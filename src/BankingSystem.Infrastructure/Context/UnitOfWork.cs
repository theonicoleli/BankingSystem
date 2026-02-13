using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using BankingSystem.SharedKernel.Data;
using BankingSystem.SharedKernel.Domain;

namespace BankingSystem.Infrastructure.Context;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task<bool> CommitAsync()
    {
        foreach (var entry in _context.ChangeTracker.Entries()
                     .Where(entry =>
                         entry.State == EntityState.Added ||
                         entry.State == EntityState.Modified ||
                         entry.State == EntityState.Deleted))
        {
            if (entry.Entity is Entity entity)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.MarkCreated();
                        break;

                    case EntityState.Modified:
                        entity.MarkUpdated();
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entity.MarkRemoved();
                        break;
                }
            }
        }

        try 
        {
            var result = await _context.SaveChangesAsync();

            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await ReleaseTransaction();
            }

            return result > 0;
        }
        catch
        {
            await RollbackAsync(); 
            throw;
        }
    }
    
    private async Task ReleaseTransaction()
    {
        if (_transaction != null)
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await ReleaseTransaction();
        }
    }
}
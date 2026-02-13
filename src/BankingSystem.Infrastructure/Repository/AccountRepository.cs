using Microsoft.EntityFrameworkCore;
using BankingSystem.Domain;
using BankingSystem.Domain.Interfaces.Repository;
using BankingSystem.Infrastructure.Context;

namespace BankingSystem.Infrastructure.Repository;

public class AccountRepository : IAccountRepository
{
    private readonly AppDbContext _context;

    public AccountRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Account?> GetById(string id)
    {
        return await _context.Accounts
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task Add(Account account)
    {
        await _context.Accounts.AddAsync(account);
    }

    public async Task Reset()
    {
        var allAccounts = await _context.Accounts.ToListAsync();
        _context.Accounts.RemoveRange(allAccounts);
        await _context.SaveChangesAsync();
    }
}
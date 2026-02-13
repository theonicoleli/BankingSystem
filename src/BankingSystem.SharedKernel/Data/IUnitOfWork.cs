namespace BankingSystem.SharedKernel.Data;

public interface IUnitOfWork
{
    Task BeginTransactionAsync();
    Task<bool> CommitAsync();
    Task RollbackAsync();
}
namespace BankingSystem.Domain.Interfaces.Repository;

public interface IAccountRepository
{
    Task<Account?> GetById(string id);
    Task Add(Account account);
    Task Reset();
}
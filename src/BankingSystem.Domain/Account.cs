using BankingSystem.SharedKernel.Domain;
using BankingSystem.SharedKernel.Utils;

namespace BankingSystem.Domain;

public class Account : Entity, IAggregateRoot
{
    private readonly object _balanceLock = new();
    
    public bool IsNew { get; private set; }
    
    private Account() { }

    public Account(string id, decimal initialBalance = 0)
    {
        Id = id;
        Balance = initialBalance;
        
        Validate();
    }

    public decimal Balance { get; private set; }
    
    public static Account CreateNew(string id)
    {
        return new Account(id) { IsNew = true };
    }

    public void Deposit(decimal amount)
    {
        AssertValidation.ValidateIfFalse(amount > 0, "Amount must be positive.");
        Balance += amount;
        
        Validate();
    }

    public void Withdraw(decimal amount)
    {
        AssertValidation.ValidateIfFalse(amount > 0, "Amount must be positive.");
        AssertValidation.ValidateIfFalse(Balance >= amount, "Insufficient funds.");
        Balance -= amount;
        
        Validate();
    }

    public void TransferTo(Account destinationAccount, decimal amount)
    {
        if (destinationAccount is null)
            throw new ArgumentNullException(nameof(destinationAccount), "Destination account cannot be null.");
        
        Withdraw(amount);
        destinationAccount.Deposit(amount);
    }

    protected sealed override void Validate()
    {
        AssertValidation.ValidateIfNullOrEmpty(Id, "Account ID cannot be null or empty.");
        AssertValidation.ValidateIfFalse(Balance >= 0, "Account balance cannot be negative.");
    }
}
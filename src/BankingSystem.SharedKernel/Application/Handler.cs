using BankingSystem.SharedKernel.Application.Results;

namespace BankingSystem.SharedKernel.Application;

public abstract class Handler<TResponse>
{
    protected readonly Result<TResponse> Result = new();

    protected bool IsValid => Result.IsValid;

    protected void AddError(string message)
    {
        Result.AddErrorGlobal(message);
    }

    protected void AddError(string property, string message)
    {
        Result.AddError(property, message);
    }

    protected Result<TResponse> Fail(Dictionary<string, List<string>> propertyErrors)
    {
        Result.AddErrors(propertyErrors);
        return Result;
    }

    protected Result<TResponse> Fail(string message)
    {
        return Fail(string.Empty, message);
    }

    protected Result<TResponse> Fail(string property, string message)
    {
        return Result<TResponse>.Failure(property, message);
    }

    protected Result<TResponse> Success(TResponse data)
    {
        return Result<TResponse>.Success(data);
    }
}
namespace BankingSystem.SharedKernel.Application.Results;

public class QueryResult<T> : Result<T>
{
    public QueryResult() { }

    public QueryResult(T? data)
    {
        if (data != null)
            Data = data;
    }

    public QueryResult(string property, string error)
    {
        AddError(property, error);
    }

    public new static QueryResult<T> Success(T data)
    {
        return new QueryResult<T>(data);
    }

    public new static QueryResult<T> Failure(string error)
    {
        return new QueryResult<T>(string.Empty, error);
    }
}
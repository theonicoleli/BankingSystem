namespace BankingSystem.SharedKernel.Application.Results;

public class Result<T>
{
    private readonly Dictionary<string, List<string>> _errors = new();

    public bool IsValid => !_errors.Any();
    public bool IsSuccess => IsValid; 
    public T? Data { get; internal set; }

    public IReadOnlyDictionary<string, List<string>> Errors => _errors;

    public static Result<T> Success(T data)
    {
        return new Result<T> { Data = data };
    }

    public static Result<T> Failure(string message)
    {
        var result = new Result<T>();
        result.AddErrorGlobal(message);
        return result;
    }

    public static Result<T> Failure(string property, string message)
    {
        var result = new Result<T>();
        result.AddError(property, message);
        return result;
    }

    public void AddErrorGlobal(string message)
    {
        AddError(string.Empty, message);
    }

    public void AddError(string property, string message)
    {
        if (!_errors.ContainsKey(property))
            _errors[property] = new List<string>();

        _errors[property].Add(message);
    }

    public void AddErrors(Dictionary<string, List<string>> errors)
    {
        foreach (var error in errors)
        {
            if (!_errors.ContainsKey(error.Key))
                _errors[error.Key] = new List<string>();

            _errors[error.Key].AddRange(error.Value);
        }
    }

    public override string ToString()
    {
        if (IsValid)
            return $"Success: {typeof(T).Name}";

        var errorList = _errors
            .SelectMany(e => e.Value.Select(v => $"{(string.IsNullOrEmpty(e.Key) ? "" : $"{e.Key}: ")}{v}"))
            .ToList();

        return $"Errors: {string.Join("; ", errorList)}";
    }
}
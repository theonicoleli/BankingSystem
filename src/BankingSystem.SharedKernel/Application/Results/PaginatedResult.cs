namespace BankingSystem.SharedKernel.Application.Results;

public class PaginatedResult<T> : Result<IEnumerable<T>>
{
    private PaginatedResult(
        IEnumerable<T>? items,
        int totalCount,
        int pageNumber,
        int pageSize)
    {
        if (items != null)
            Data = items;

        TotalCount = totalCount;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
    }

    private PaginatedResult(string error)
    {
        AddErrorGlobal(error);
        TotalCount = 0;
        PageNumber = 0;
        PageSize = 0;
        TotalPages = 0;
    }

    public int PageNumber { get; }
    public int PageSize { get; private set; }
    public int TotalCount { get; private set; }
    public int TotalPages { get; }
    public bool HasPrevious => PageNumber > 1;
    public bool HasNext => PageNumber < TotalPages;

    public static PaginatedResult<T> Success(
        IEnumerable<T>? items,
        int totalCount,
        int pageNumber,
        int pageSize)
    {
        return new PaginatedResult<T>(items, totalCount, pageNumber, pageSize);
    }

    public new static PaginatedResult<T> Failure(string error)
    {
        return new PaginatedResult<T>(error);
    }
}
namespace EagleRockService.Features.Common;

public class PaginatedResponse<T>
{
    public PaginatedResponse(IEnumerable<T> results, int recordCount, int lastPage, int currentPage)
    {
        Pagination = new Pagination { RecordCount = recordCount, LastPage = lastPage, CurrentPage = currentPage };
        Results = results;
    }

    public Pagination Pagination { get; }

    public IEnumerable<T> Results { get; }
}
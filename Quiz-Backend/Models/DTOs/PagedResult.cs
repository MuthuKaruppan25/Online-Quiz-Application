public class PagedResult<T>
{
    public int TotalCount { get; set; }
    public IEnumerable<T> Data { get; set; } = [];

    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public int TotalPages => PageSize > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 0;
}

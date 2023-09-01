namespace SchoolProject.Core.Wrappers;

public class PaginationResult<T>
{
    #region properties



    public List<T> Data { get; set; } = new();
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageCount { get; set; }
    public int PageSize { get; set; }
    public object Meta { get; set; }
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;
    public List<string> Messages { get; set; } = new();
    public bool Successed { get; set; }

    #endregion
    #region simpleMethods
    public PaginationResult(bool successed, List<T> data = default, List<string> messages = null, int count = 0, int page = 1,
        int pageSize = 10)
    {
        Data = data;
        CurrentPage = page;
        Successed = successed;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
    }

    public static PaginationResult<T> Success(List<T> data, int count, int page, int pageSize)
    {
        return new(true, data, null, count, pageSize, page);
    }
    #endregion

}


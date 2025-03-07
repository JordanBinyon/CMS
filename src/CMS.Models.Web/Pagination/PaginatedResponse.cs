namespace CMS.Models.Web.Pagination;

public class PaginatedResponse<T>
{
    public List<T> Data { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
}
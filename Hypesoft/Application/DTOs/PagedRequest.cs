// PagedResponse.cs - Resposta paginada simples
namespace Hypesoft.Application.DTOs
{
    public class PagedResponse<T>
    {
        public List<T> Data { get; set; } = new();
        public int Page { get; set; }
        public int PageSize { get; set; }
        public long TotalItems { get; set; }
        public int TotalPages { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }

        public PagedResponse(List<T> data, int page, int pageSize, long totalItems)
        {
            Data = data;
            Page = page;
            PageSize = pageSize;
            TotalItems = totalItems;
            TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            HasNextPage = page < TotalPages;
            HasPreviousPage = page > 1;
        }
    }
}
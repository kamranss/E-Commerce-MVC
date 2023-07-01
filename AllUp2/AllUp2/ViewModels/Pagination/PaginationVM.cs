namespace AllUp2.ViewModels.Pagination
{
    public class PaginationVM<T>
    {
        public List<T> Items { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public PaginationVM(List<T> items, int currentPage, int pageCount)
        {
            Items = items;
            CurrentPage = currentPage;
            PageCount = pageCount;
        }
    }
}

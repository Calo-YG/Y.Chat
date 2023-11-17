namespace Y.Chat.Application.Base
{
    public class PageDto<T>
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public List<T> Result { get; set; }

        public int TotalCount { get; set; }
    }
}

namespace WebApplication.Infrastructure.Paging
{
    public class PagingQueryString
    {
        private int? _page;
        public int? Page
        {
            get { return _page; }
            set
            {
                if (value <= 0)
                {
                    _page = 1;
                }
                else
                {
                    _page = value;
                }
            }
        }

        private int? _pageSize;
        public int? PageSize
        {
            get { return _pageSize; }
            set
            {
                if (value == null)
                {
                    _pageSize = AppSettings.DEFAULT_PAGE_SIZE;
                }
                else
                {
                    _pageSize = (int)value;
                }
            }
        }
    }
}

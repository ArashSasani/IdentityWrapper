using System;

namespace WebApplication.Infrastructure.Paging
{
    public class PagingSetup
    {
        //inputs
        private int _pageNumber;
        private int _pageSize;

        public PagingSetup(int pageNumber, int pageSize)
        {
            _pageNumber = pageNumber;
            _pageSize = pageSize;
        }

        public PagingSetup(int pageNumber)
        {
            _pageNumber = pageNumber;
            _pageSize = AppSettings.DEFAULT_PAGE_SIZE;
        }

        public PagingSetup(int? pageNumber, int? pageSize)
        {
            _pageNumber = pageNumber ?? 1;
            _pageSize = pageSize ?? AppSettings.DEFAULT_PAGE_SIZE;
        }

        #region Props
        //paging variables
        public int Offset
        {
            get
            {
                return _pageNumber == 1 ? 0
                        : ((_pageNumber - 1) * _pageSize);
            }
        }
        public int Next
        {
            get
            {
                return _pageSize;
            }
        }
        #endregion

        public PagingControls GetPagingControls(int totalNumberOfRecords
            , PagingStrategy pagingStrategy)
        {
            var controls = new PagingControls
            {
                PagesCount = GetPagesCount(totalNumberOfRecords)
            };

            controls = GetPrevNext(controls, pagingStrategy);

            return controls;
        }

        private PagingControls GetPrevNext(PagingControls controls
            , PagingStrategy pagingStrategy)
        {
            if (_pageNumber == 1)  //first page state
            {
                if (_pageNumber == controls.PagesCount)
                    controls.NextPage = null;
                else
                    controls.NextPage = _pageNumber + 1;
                controls.PrevPage = null;
            }
            else if (_pageNumber == controls.PagesCount) //last page state
            {
                controls.NextPage = null;
                controls.PrevPage = _pageNumber - 1;
            }
            else if (_pageNumber > controls.PagesCount)
            {
                if (pagingStrategy == PagingStrategy.ResetPagingToFirst)
                {
                    _pageNumber = 1;
                    return GetPrevNext(new PagingControls
                    {
                        PagesCount = controls.PagesCount
                    }, PagingStrategy.ReturnNull);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                controls.NextPage = _pageNumber + 1;
                controls.PrevPage = _pageNumber - 1;
            }
            return controls;
        }

        private int GetPagesCount(int total)
        {
            int pagesCount = AppSettings.DEFAULT_PAGES_COUNT;
            if (_pageSize < total)
            {
                if (total % _pageSize == 0)
                    pagesCount = total / _pageSize;
                else
                {
                    pagesCount = (int)Math.Floor
                    ((float)total / _pageSize) + 1;
                }
            }
            return pagesCount;
        }

    }

    public class PagingControls
    {
        public int? NextPage { get; set; }
        public int? PrevPage { get; set; }
        public int PagesCount { get; set; }
    }

    /// <summary>
    ///if the page number requested is larger than the pages count,
    /// you can either reset the pager to the first page and 
    /// return the first page list or return null for example for showing not found page.
    /// </summary>
    public enum PagingStrategy
    {
        ResetPagingToFirst = 1,
        ReturnNull = 2
    }
}

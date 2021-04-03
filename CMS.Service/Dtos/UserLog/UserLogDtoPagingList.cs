using System.Collections.Generic;
using WebApplication.Infrastructure.Interfaces;

namespace CMS.Service.Dtos.UserLog
{
    public class UserLogDtoPagingList : IPaging<UserLogDto>
    {
        public List<UserLogDto> PagingList { get; set; }
            = new List<UserLogDto>();
        public int? NextPage { get; set; }
        public int? PrevPage { get; set; }
        public int PagesCount { get; set; }
    }
}

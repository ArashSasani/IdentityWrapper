using System;

namespace CMS.Service.Dtos.UserLog
{
    public class UserLogDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string IP { get; set; }
        public string UserAgent { get; set; }
        public string Date { get; set; }
        public string Operation { get; set; }
        public long RowNumber { get; set; }
    }
}

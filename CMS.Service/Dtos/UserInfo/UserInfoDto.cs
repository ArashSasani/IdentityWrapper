using System;

namespace CMS.Service.Dtos.UserInfo
{
    public class UserInfoDto
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return Name + " " + LastName; } }
        public string NationalCode { get; set; }
        public string BirthDate { get; set; }
        public string Details { get; set; }
        public string Image { get; set; }
        public byte[] ImageBytes { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMS.Service.Dtos.UserInfo
{
    public class UpdateUserInfoDto
    {
        [Required(ErrorMessage = "*")]
        public string UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public string BirthDate { get; set; }
        public string Details { get; set; }
        public string Image { get; set; }
        public byte[] ImageBytes { get; set; }
    }
}
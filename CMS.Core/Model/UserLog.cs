using CMS.Core.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace CMS.Core.Model
{
    public class UserLog : IEntity
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public string IP { get; set; }
        public string UserAgent { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public string Operation { get; set; }

        public User User { get; set; }
    }
}

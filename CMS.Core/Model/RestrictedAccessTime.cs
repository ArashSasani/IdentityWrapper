using CMS.Core.Interfaces;
using System;

namespace CMS.Core.Model
{
    public class RestrictedAccessTime : IEntity
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
    }
}

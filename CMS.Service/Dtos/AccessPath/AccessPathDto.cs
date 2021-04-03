using System;

namespace CMS.Service.Dtos.AccessPath
{
    public class AccessPathDto
    {
        public Guid ParentId { get; set; }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public int Priority { get; set; }
    }
}

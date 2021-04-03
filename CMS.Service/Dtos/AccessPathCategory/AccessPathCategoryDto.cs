using CMS.Service.Dtos.AccessPath;
using System;
using System.Collections.Generic;

namespace CMS.Service.Dtos.AccessPathCategory
{
    public class AccessPathCategoryDto
    {
        public Guid ParentId { get; set; }
        public string ParentTitle { get; set; }
        public List<AccessPathDto> AccessPaths { get; set; }
            = new List<AccessPathDto>();
    }
}

using CMS.Service.Dtos.AccessPath;
using System;
using System.Collections.Generic;

namespace CMS.Service.Dtos.RoleAccessPath
{
    public class RoleAccessPathsDto
    {
        public string RoleId { get; set; }
        public string Rolename { get; set; }
        public List<AccessPathDto> AccessPaths { get; set; } 
            = new List<AccessPathDto>();
    }
}
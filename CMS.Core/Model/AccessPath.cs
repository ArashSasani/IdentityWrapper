using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Core.Model
{
    public class AccessPath
    {
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public int Priority { get; set; }

        [ForeignKey("ParentId")]
        public virtual AccessPathCategory AccessPathCategory { get; set; }
        public virtual ICollection<RoleAccessPath> RoleAccesses { get; set; }
    }
}

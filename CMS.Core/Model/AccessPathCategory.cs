using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMS.Core.Model
{
    public class AccessPathCategory
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }

        public virtual ICollection<AccessPath> AccessPaths { get; set; }
    }
}

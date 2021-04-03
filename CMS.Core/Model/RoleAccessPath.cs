using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Core.Model
{
    public class RoleAccessPath
    {
        [Key]
        [Column(Order = 0)]
        public string RoleId { get; set; }
        [Key]
        [Column(Order = 1)]
        public Guid AccessPathId { get; set; }

        [ForeignKey("RoleId")]
        public virtual IdentityRole IdentityRole { get; set; }
        public virtual AccessPath AccessPath { get; set; }
    }
}

using CMS.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace CMS.Core.Model
{
    public class RestrictedIP : IEntity
    {
        public int Id { get; set; }
        [Required]
        public string IP { get; set; }
    }
}

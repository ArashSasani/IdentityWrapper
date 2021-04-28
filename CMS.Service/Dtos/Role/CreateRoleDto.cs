using System.ComponentModel.DataAnnotations;

namespace CMS.Service.Dtos.Role
{
    public class CreateRoleDto
    {
        [Required(ErrorMessage ="*")]
        public string Name { get; set; }
    }
}
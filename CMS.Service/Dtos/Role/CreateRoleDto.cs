using System.ComponentModel.DataAnnotations;

namespace CMS.Service.Dtos.Role
{
    public class CreateRoleDto
    {
        [Required(ErrorMessage ="لطفا نام نقش را وارد کنید")]
        public string Name { get; set; }
    }
}
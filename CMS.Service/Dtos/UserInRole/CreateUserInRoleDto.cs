using System.ComponentModel.DataAnnotations;

namespace CMS.Service.Dtos.UserInRole
{
    public class CreateUserInRoleDto
    {
        [Required(ErrorMessage = "لطفا کد کاربر را وارد کنید")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "لطفا کد نقش را وارد کنید")]
        public string RoleId { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace CMS.Service.Dtos.UserInRole
{
    public class CreateUserInRoleDto
    {
        [Required(ErrorMessage = "*")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "*")]
        public string RoleId { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace CMS.Service.Dtos.User
{
    public class RegisterUserDto
    {
        public string UserId { get; set; }
        [Required(ErrorMessage = "*")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "confirm pass does not match with the pass")]
        public string ConfirmPassword { get; set; }
        public bool IsPersonnel { get; set; }

        public RegisterUserInfoDto UserInfo { get; set; }
    }

    public class RegisterUserInfoDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
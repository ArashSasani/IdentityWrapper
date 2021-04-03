using System.ComponentModel.DataAnnotations;

namespace CMS.Service.Dtos.User
{
    public class RegisterUserDto
    {
        public string UserId { get; set; }
        [Required(ErrorMessage = "لطفا نام کاربری را وارد کنید")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "لطفا کلمه عبور را وارد کنید")]
        [StringLength(100, ErrorMessage = "{0} باید حداقل {2} کاراکتر باشد.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "لطفا تکرار کلمه عبور را وارد کنید")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "کلمه عبور و تکرار کلمه عبور متفاوت می باشند.")]
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
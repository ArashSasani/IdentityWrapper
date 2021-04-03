using System.ComponentModel.DataAnnotations;

namespace CMS.Service.Dtos.RestrictedIP
{
    public class CreateRestrictedIPDto
    {
        [Required(ErrorMessage = "لطفا آی پی را وارد کنید")]
        [RegularExpression(@"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$", ErrorMessage = "آی پی اشتباه وارد شده است")]
        public string IP { get; set; }
    }
}

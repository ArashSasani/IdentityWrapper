using System.ComponentModel.DataAnnotations;

namespace CMS.Service.Dtos.RestrictedIP
{
    public class CreateRestrictedIPDto
    {
        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$", ErrorMessage = "IP is incorrect")]
        public string IP { get; set; }
    }
}

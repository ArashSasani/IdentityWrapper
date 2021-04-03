using System.ComponentModel.DataAnnotations;

namespace CMS.Service.Dtos.RestrictedIP
{
    public class UpdateRestrictedDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "عدد وارد شده باید بین {1} و {2} باشد")]
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا آی پی را وارد کنید")]
        [RegularExpression(@"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$", ErrorMessage = "آی پی اشتباه وارد شده است")]
        public string IP { get; set; }
    }
}

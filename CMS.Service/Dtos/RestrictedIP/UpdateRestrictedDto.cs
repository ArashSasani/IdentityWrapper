using System.ComponentModel.DataAnnotations;

namespace CMS.Service.Dtos.RestrictedIP
{
    public class UpdateRestrictedDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "number should be between {1} and {2}")]
        public int Id { get; set; }
        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$", ErrorMessage = "IP is incorrect")]
        public string IP { get; set; }
    }
}

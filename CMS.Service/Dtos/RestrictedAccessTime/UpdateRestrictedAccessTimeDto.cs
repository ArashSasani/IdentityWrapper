using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;

namespace CMS.Service.Dtos.RestrictedAccessTime
{
    public class UpdateRestrictedAccessTimeDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "عدد وارد شده باید بین {1} و {2} باشد")]
        public int Id { get; set; }
        public string Date { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan FromTime { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan ToTime { get; set; }
    }

    public class UpdateRestrictedAccessTimeDtoValidator : AbstractValidator<UpdateRestrictedAccessTimeDto>
    {
        public UpdateRestrictedAccessTimeDtoValidator()
        {
            RuleFor(x => x.ToTime).GreaterThan(x => x.FromTime)
                .WithMessage("زمان پایان نباید از زمان شروع کمتر و یا مساوی آن باشد");
        }
    }
}

using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;

namespace CMS.Service.Dtos.RestrictedAccessTime
{
    public class CreateRestrictedAccessTimeDto
    {
        public string Date { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan FromTime { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan ToTime { get; set; }
    }

    public class CreateRestrictedAccessTimeDtoValidator : AbstractValidator<CreateRestrictedAccessTimeDto>
    {
        public CreateRestrictedAccessTimeDtoValidator()
        {
            RuleFor(x => x.ToTime).GreaterThan(x => x.FromTime)
                .WithMessage("زمان پایان نباید از زمان شروع کمتر و یا مساوی آن باشد");
        }
    }
}

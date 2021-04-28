using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;

namespace CMS.Service.Dtos.RestrictedAccessTime
{
    public class CreateRestrictedAccessTimeDto
    {
        public DateTime? Date { get; set; }
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
                .WithMessage("end time cannot be less or equal than from time");
        }
    }
}

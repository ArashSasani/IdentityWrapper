using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;

namespace CMS.Service.Dtos.RestrictedAccessTime
{
    public class UpdateRestrictedAccessTimeDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "number should be between {1} and {2}")]
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
                .WithMessage("end time cannot be less or equal than from time");
        }
    }
}

using FluentValidation;
using TourAPI.Models.Domains;

namespace TourAPI.Validation
{
    public class BaseValidator : AbstractValidator<BaseEntity>
    {
        public BaseValidator()
        {
            RuleFor(baseE => baseE.CreateTimes).NotEmpty();
            RuleFor(baseE => baseE.LastUpdateTimes).NotEmpty();
        }
    }
}

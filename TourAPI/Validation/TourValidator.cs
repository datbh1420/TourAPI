using FluentValidation;
using TourAPI.Models.Domains;

namespace TourAPI.Validation
{
    public class TourValidator : AbstractValidator<Tour>
    {
        public TourValidator()
        {
            RuleFor(x => x.Price).NotEmpty().NotNull().GreaterThanOrEqualTo(0);
            RuleFor(x => x.Address).NotNull().NotEmpty();
            RuleFor(x => x.Description).NotNull().MinimumLength(20);
            RuleFor(x => x.DurationDays).NotEmpty().NotNull();
            RuleFor(x => x.Category).SetValidator(new CategoryValidator()).When(x => x.Category is not null);
            RuleFor(x => x.Location).SetValidator(new LocationValidator()).When(x => x.Location is not null);
        }
    }
}

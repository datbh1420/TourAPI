using FluentValidation;
using TourAPI.Models.Domains;

namespace TourAPI.Validation
{
    public class LocationValidator : AbstractValidator<Location>
    {
        public LocationValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
            RuleFor(x => x.Country).NotNull().NotEmpty();
            RuleFor(x => x.Continent).IsInEnum();
            RuleFor(x => x.Description).Length(20, 1000).When(x => x.Description is not null);
        }
    }
}

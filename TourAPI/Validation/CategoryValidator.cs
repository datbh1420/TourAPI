using FluentValidation;
using TourAPI.Models.Domains;

namespace TourAPI.Validation
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Description).Length(20, 1000).When(x => x.Description is not null);
        }
    }
}

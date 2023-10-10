using FluentValidation;
using TourAPI.Models.Domains;

namespace TourAPI.Validation
{
    public class ReviewValidator : AbstractValidator<Review>
    {
        public ReviewValidator()
        {
            RuleFor(x => x.Rating).NotNull().NotEmpty().InclusiveBetween(0, 10)
                .WithMessage("Rating must be Greater Than {From} and Less Than {To}");
            RuleFor(x => x.Comment).NotNull().NotEmpty().MinimumLength(20).MaximumLength(300);
        }
    }
}

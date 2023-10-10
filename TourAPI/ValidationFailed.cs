using FluentValidation.Results;

namespace TourAPI
{
    public record ValidationFailed(IEnumerable<ValidationFailure> Errors)
    {
        public ValidationFailed(ValidationFailure error) : this(new[] { error })
        {
        }
    }
}

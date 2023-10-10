using FluentValidation;
using User.Manage.API.Models;

namespace TourAPI.Validation
{
    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator()
        {
            RuleForEach(x => x.To).Must(x => x.Address.Contains("@"));
        }
    }
}

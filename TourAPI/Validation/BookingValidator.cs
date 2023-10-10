using FluentValidation;
using TourAPI.Models.Domains;

namespace TourAPI.Validation
{
    public class BookingValidator : AbstractValidator<Booking>
    {
        public BookingValidator()
        {
            RuleFor(booking => booking.CheckInDate).NotEmpty().LessThan(booking => booking.CheckOutDate);
            RuleFor(booking => booking.CheckOutDate).NotEmpty().GreaterThan(booking => booking.CheckInDate);
            RuleFor(booking => booking.TotalPrice).GreaterThanOrEqualTo(0);
            RuleFor(booking => booking.Tour).SetValidator(new TourValidator());
        }
    }
}

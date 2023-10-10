using Microsoft.AspNetCore.Identity;
using TourAPI.Models.Response;

namespace TourAPI.Models.Domains
{
    public class Booking : BaseEntity
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; } = 0;
        public string UserId { get; set; } = null!;
        public string TourId { get; set; } = null!;
        public IdentityUser User { get; set; } = null!;
        public Tour Tour { get; set; } = null!;

        public static explicit operator BookingResponse(Booking booking)
        {
            return new BookingResponse
            {
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate,
                TotalPrice = booking.TotalPrice,
                Tour = (TourResponse)booking.Tour,
                User = new UserResponse
                {
                    Name = booking.User.UserName,
                    Email = booking.User.Email,
                    PhoneNumber = booking.User.PhoneNumber
                },
                CreateTimes = booking.CreateTimes,
                LastUpdateTimes = booking.LastUpdateTimes,
            };
        }
    }
}

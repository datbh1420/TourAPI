using TourAPI.Models.Domains;

namespace TourAPI.Models.Request
{
    public class BookingRequest
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; } = 0;
        public string UserId { get; set; } = null!;
        public string TourId { get; set; } = null!;

        public static explicit operator Booking(BookingRequest request)
        {
            return new Booking
            {
                CheckInDate = request.CheckInDate,
                CheckOutDate = request.CheckOutDate,
                TotalPrice = request.TotalPrice,
                UserId = request.UserId,
                TourId = request.TourId,
            };
        }
    }
}

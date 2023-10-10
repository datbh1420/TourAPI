namespace TourAPI.Models.Response
{
    public class BookingResponse
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; } = 0;
        public UserResponse User { get; set; } = null!;
        public TourResponse Tour { get; set; } = null!;
        public DateTime CreateTimes { get; set; }
        public DateTime LastUpdateTimes { get; set; }
    }
}

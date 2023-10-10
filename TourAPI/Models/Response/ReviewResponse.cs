namespace TourAPI.Models.Response
{
    public class ReviewResponse
    {
        public float Rating { get; set; }
        public string Comment { get; set; } = null!;
        public UserResponse User { get; set; } = null!;
        public TourResponse Tour { get; set; } = null!;
        public DateTime CreateTimes { get; set; }
        public DateTime LastUpdateTimes { get; set; }
    }
}

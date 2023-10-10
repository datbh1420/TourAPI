using TourAPI.Models.Domains;

namespace TourAPI.Models.Request
{
    public class ReviewRequest
    {
        public float Rating { get; set; }
        public string Comment { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string TourId { get; set; } = null!;

        public static explicit operator Review(ReviewRequest request)
        {
            return new Review
            {
                Rating = request.Rating,
                Comment = request.Comment,
                UserId = request.UserId,
                TourId = request.TourId,
            };
        }
    }
}

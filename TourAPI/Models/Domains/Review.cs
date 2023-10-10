using Microsoft.AspNetCore.Identity;
using TourAPI.Models.Response;

namespace TourAPI.Models.Domains
{
    public class Review : BaseEntity
    {
        public float Rating { get; set; }
        public string Comment { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string TourId { get; set; } = null!;
        public IdentityUser User { get; set; } = null!;
        public Tour Tour { get; set; } = null!;

        public static explicit operator ReviewResponse(Review review)
        {
            return new ReviewResponse
            {
                Comment = review.Comment,
                Rating = review.Rating,
                User = new UserResponse
                {
                    Name = review.User.UserName,
                    Email = review.User.Email,
                    PhoneNumber = review.User.PhoneNumber
                },
                Tour = (TourResponse)review.Tour,
                CreateTimes = review.CreateTimes,
                LastUpdateTimes = review.LastUpdateTimes,
            };
        }
    }
}

using TourAPI.Models.Domains;

namespace TourAPI.Models.Request
{
    public class TourRequest
    {
        public string Name { get; set; } = null!;
        public Double Rating { get; set; }
        public string Address { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double Price { get; set; }
        public string DurationDays { get; set; } = null!;
        public string? CategoryId { get; set; }
        public string? LocationId { get; set; }

        public static explicit operator Tour(TourRequest request)
        {
            return new Tour
            {
                Name = request.Name,
                Rating = request.Rating,
                Address = request.Address,
                Description = request.Description,
                Price = request.Price,
                DurationDays = request.DurationDays,
                CategoryId = request.CategoryId,
                LocationId = request.LocationId
            };
        }
    }
}

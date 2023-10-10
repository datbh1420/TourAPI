using TourAPI.Models.Dto;
using TourAPI.Models.Response;

namespace TourAPI.Models.Domains
{
    public class Tour : BaseEntity
    {
        public string Name { get; set; } = null!;
        public Double Rating { get; set; }
        public string Address { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double Price { get; set; }
        public string DurationDays { get; set; } = null!;
        public string? CategoryId { get; set; }
        public string? LocationId { get; set; }
        public Category? Category { get; set; }
        public Location? Location { get; set; }

        public static explicit operator TourResponse(Tour tour)
        {
            return new TourResponse
            {
                Name = tour.Name,
                Rating = tour.Rating,
                Address = tour.Address,
                Description = tour.Description,
                Price = tour.Price,
                DurationDays = tour.DurationDays,
                Category = (tour.Category is not null) ? (CategoryResponse)tour.Category : null,
                Location = (tour.Location is not null) ? (LocationResponse)tour.Location : null,
                CreateTimes = tour.CreateTimes,
                LastUpdateTimes = tour.LastUpdateTimes,
            };
        }
    }
}

using TourAPI.Models.Domains;
using TourAPI.Models.Dto;

namespace TourAPI.Models.Response
{
    public class TourResponse
    {
        public string Name { get; set; } = null!;
        public CategoryResponse? Category { get; set; }
        public LocationResponse? Location { get; set; }
        public Double Rating { get; set; }
        public string Address { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double Price { get; set; }
        public string DurationDays { get; set; } = null!;
        public DateTime CreateTimes { get; set; }
        public DateTime LastUpdateTimes { get; set; }
        public Image Image { get; set; }
    }
}

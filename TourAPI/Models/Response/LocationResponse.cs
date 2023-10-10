using TourAPI.Enums;

namespace TourAPI.Models.Response
{
    public class LocationResponse
    {
        public Continents Continent { get; set; }
        public string Country { get; set; } = null!;
        public string? Description { get; set; }
    }
}

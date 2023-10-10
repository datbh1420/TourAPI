using TourAPI.Enums;
using TourAPI.Models.Domains;

namespace TourAPI.Models.Request
{
    public class LocationRequest
    {
        public Continents Continent { get; set; }
        public string Country { get; set; } = null!;
        public string? Description { get; set; }

        public static implicit operator Location(LocationRequest request)
        {
            return new Location
            {
                Continent = request.Continent,
                Country = request.Country,
                Description = request.Description,
            };
        }
    }
}

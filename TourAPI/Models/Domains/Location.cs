using System.ComponentModel.DataAnnotations;
using TourAPI.Enums;
using TourAPI.Models.Response;

namespace TourAPI.Models.Domains
{
    public class Location
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public Continents Continent { get; set; }
        public string Country { get; set; } = null!;
        public string? Description { get; set; }

        public static explicit operator LocationResponse(Location location)
        {
            return new LocationResponse
            {
                Continent = location.Continent,
                Country = location.Country,
                Description = location.Description,
            };
        }
    }


}

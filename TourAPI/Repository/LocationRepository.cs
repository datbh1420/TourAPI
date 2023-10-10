using TourAPI.Data;
using TourAPI.Models.Domains;

namespace TourAPI.Repository
{
    public class LocationRepository : BaseRepository<Location>
    {
        private readonly TourDBContext context;
        public LocationRepository(TourDBContext context) : base(context)
        {
            this.context = context;
        }

        public List<Tour> GetTourByLocationName(string locationName)
        {
            var listTour = (from tour in context.Tours
                            join location in context.Locations on tour.LocationId equals location.Id
                            where location.Country == locationName
                            select tour
                            ).ToList();
            return listTour;
        }
    }
}

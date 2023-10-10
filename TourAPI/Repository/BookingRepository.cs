using TourAPI.Data;
using TourAPI.Models.Domains;

namespace TourAPI.Repository
{
    public class BookingRepository : BaseRepository<Booking>
    {
        public BookingRepository(TourDBContext context) : base(context)
        {
        }
    }
}

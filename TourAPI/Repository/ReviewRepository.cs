using Microsoft.EntityFrameworkCore;
using TourAPI.Data;
using TourAPI.Models.Domains;

namespace TourAPI.Repository
{
    public class ReviewRepository : BaseRepository<Review>
    {
        private readonly TourDBContext context;

        public ReviewRepository(TourDBContext context) : base(context)
        {
            this.context = context;
        }
        public new async Task<Review?> GetByIdAsync(string Id)
        {
            var entity = await context.Reviews.Include(x => x.Tour).Include(x => x.User).FirstOrDefaultAsync(x => x.Id == Id);
            return entity;
        }
        public async Task<List<Review>> GetReviewByTourId(string tourId)
        {
            var list = await context.Reviews.Where(r => r.TourId == tourId).Include(x => x.Tour).Include(x => x.User).ToListAsync();
            return list;
        }
    }
}

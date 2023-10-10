using TourAPI.Data;
using TourAPI.Models.Domains;

namespace TourAPI.Repository
{
    public class CategoryRepository : BaseRepository<Category>
    {
        private readonly TourDBContext context;
        public CategoryRepository(TourDBContext context) : base(context)
        {
            this.context = context;
        }
    }
}

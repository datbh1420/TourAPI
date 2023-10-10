using Microsoft.EntityFrameworkCore;
using TourAPI.Data;
using TourAPI.Models.Domains;
using TourAPI.Models.Request;

namespace TourAPI.Repository
{
    public class TourRepository : BaseRepository<Tour>
    {
        public readonly TourDBContext context;

        public TourRepository(TourDBContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<Tour>> GetAllByQuery(Querying query)
        {
            var list = context.Tours.AsQueryable();

            if (string.IsNullOrWhiteSpace(query.FilterOn) == false && string.IsNullOrWhiteSpace(query.FilterQuery) == false)
            {
                if (query.FilterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    list = list.Where(x => x.Name.Contains(query.FilterQuery));
                }
            }

            if (string.IsNullOrWhiteSpace(query.SortBy) == false)
            {
                if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase) == false)
                {
                    list = query.IsAscending.Value ? list.OrderBy(x => x.Name) : list.OrderByDescending(x => x.Name);
                }
                else if (query.SortBy.Equals("Price", StringComparison.OrdinalIgnoreCase) == false)
                {
                    list = query.IsAscending.Value ? list.OrderBy(x => x.Price) : list.OrderByDescending(x => x.Price);
                }
                else if (query.SortBy.Equals("Rating", StringComparison.OrdinalIgnoreCase) == false)
                {
                    list = query.IsAscending.Value ? list.OrderBy(x => x.Rating) : list.OrderByDescending(x => x.Rating);
                }
            }

            var skip = (int)(query.PageNumber - 1) * query.PageSize;

            return await list.Skip(skip).Take(query.PageSize).ToListAsync();
        }

        public async Task<List<Tour>> GetTourByCategory(string categoryId)
        {
            try
            {
                var list = await context.Tours.Where(t => t.CategoryId == categoryId).ToListAsync();
                return list;
            }
            catch (Exception)
            {
                throw new Exception("Error in GetTourByCateGory");
            }
        }

        public new async Task<List<Tour>> GetAllAsync()
        {
            try
            {
                var list = await context.Tours.Include(x => x.Category).Include(x => x.Location).ToListAsync();
                return list;
            }
            catch (Exception)
            {
                throw new Exception("Error in GetAllAsync");
            }
        }
    }
}
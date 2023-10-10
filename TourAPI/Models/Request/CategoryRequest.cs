using TourAPI.Models.Domains;

namespace TourAPI.Models.Request
{
    public class CategoryRequest
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public static explicit operator Category(CategoryRequest request)
        {
            return new Category
            {
                Name = request.Name,
                Description = request.Description,
            };
        }
    }
}

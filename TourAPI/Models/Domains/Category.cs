using TourAPI.Models.Dto;

namespace TourAPI.Models.Domains
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public static explicit operator CategoryResponse(Category category)
        {
            return new CategoryResponse
            {
                Name = category.Name,
                Description = category.Description,
                CreateTimes = category.CreateTimes,
                LastUpdateTimes = category.LastUpdateTimes,
            };
        }
    }
}

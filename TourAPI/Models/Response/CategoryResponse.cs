namespace TourAPI.Models.Dto
{
    public class CategoryResponse
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreateTimes { get; set; }
        public DateTime LastUpdateTimes { get; set; }
    }
}

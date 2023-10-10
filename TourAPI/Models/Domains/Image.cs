using System.ComponentModel.DataAnnotations.Schema;

namespace TourAPI.Models.Domains
{
    public class Image
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [NotMapped]
        public IFormFile File { get; set; } = null!;
        public string? FileName { get; set; }
        public string? FileDescription { get; set; }
        public string FileExtension { get; set; } = null!;
        public long FileSizeInBytes { get; set; }
        public string FilePath { get; set; } = null!;
        public string TourId { get; set; } = null!;
        public Tour Tour { get; set; } = null!;
    }
}
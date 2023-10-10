using TourAPI.Models.Domains;

namespace TourAPI.Models.Request
{
    public class ImageRequest
    {
        public IFormFile File { get; set; } = null!;
        public string? FileName { get; set; }
        public string? FileDescription { get; set; }

        public static explicit operator Image(ImageRequest request)
        {
            return new Image
            {
                File = request.File,
                FileName = request.FileName,
                FileDescription = request.FileDescription,
                FileExtension = Path.GetExtension(request.File.FileName),
                FileSizeInBytes = request.File.Length
            };
        }
    }
}

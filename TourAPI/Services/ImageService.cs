using OneOf;
using OneOf.Types;
using TourAPI.Models.Domains;
using TourAPI.Repository;

namespace TourAPI.Services
{
    public interface IImageService
    {
        Task<OneOf<NotFound, Image>> Upload(Image image);
        Task<OneOf<NotFound, Image>> Update(Image image);
    }
    public class ImageService : IImageService
    {
        private readonly ImageRepository repo;

        public ImageService(ImageRepository repo)
        {
            this.repo = repo;
        }
        public async Task<OneOf<NotFound, Image>> Upload(Image image)
        {
            await repo.Upload(image);
            if (image is null)
                return new NotFound();
            return image;
        }

        public async Task<OneOf<NotFound, Image>> Update(Image image)
        {
            await repo.Update(image);
            if (image is null)
                return new NotFound();
            return image;
        }
    }
}

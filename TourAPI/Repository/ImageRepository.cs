using Microsoft.EntityFrameworkCore;
using TourAPI.Data;
using TourAPI.Models.Domains;

namespace TourAPI.Repository
{
    public class ImageRepository
    {
        private readonly TourDBContext context;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, TourDBContext context)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<Image?> Upload(Image image)
        {
            // Create FilePath
            var filePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images",
                $"{image.FileName}{image.FileExtension}");

            //CopyToFile
            using var stream = new FileStream(filePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            image.FilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}" +
                $"://{httpContextAccessor.HttpContext.Request.Host}" +
                $"{httpContextAccessor.HttpContext.Request.PathBase}" +
                $"/Images/{image.FileName}{image.FileExtension}";
            await context.Images.AddAsync(image);
            await context.SaveChangesAsync();

            return image;
        }

        public async Task<Image?> Update(Image image)
        {
            var imageExist = await context.Images.FirstOrDefaultAsync(i => i.TourId == image.TourId);
            imageExist.FileExtension = image.FileExtension;
            imageExist.FileDescription = image.FileDescription;
            imageExist.FileName = image.FileName;
            imageExist.FileSizeInBytes = image.FileSizeInBytes;
            imageExist.File = image.File;

            var filePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images",
                $"{image.FileName}{image.FileExtension}");

            using var stream = new FileStream(filePath, FileMode.Create);
            await imageExist.File.CopyToAsync(stream);

            imageExist.FilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}" +
                $"://{httpContextAccessor.HttpContext.Request.Host}" +
                $"{httpContextAccessor.HttpContext.Request.PathBase}" +
                $"/Images/{image.FileName}{image.FileExtension}";
            context.Images.Update(imageExist);
            await context.SaveChangesAsync();

            return imageExist;
        }

        public async Task<bool?> Delete(string Id)
        {
            var imageExist = await context.Images.FirstOrDefaultAsync(i => i.TourId == Id);
            context.Images.Remove(imageExist);
            await context.SaveChangesAsync();
            return true;
        }
    }
}

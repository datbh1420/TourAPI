using FluentValidation;
using TourAPI.Models.Domains;

namespace TourAPI.Validation
{
    public class ImageValidator : AbstractValidator<Image>
    {
        public ImageValidator()
        {
            RuleFor(x => x.File.Length).ExclusiveBetween(0, 26_214_400)
                .WithMessage("File Size should be greater than 0 and less than 25MB");
            RuleFor(x => x.File).Must(ValidationExtension)
                .WithMessage("Unsupported file extension");
            RuleFor(x => x.Tour).SetValidator(new TourValidator());
        }

        private bool ValidationExtension(IFormFile file)
        {
            var allowExtensions = new string[] { ".png", ".jpeg", ".jpg" };

            if (allowExtensions.Contains(Path.GetExtension(file.FileName)))
                return true;
            return false;
        }
    }
}

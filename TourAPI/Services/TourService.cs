using FluentValidation;
using OneOf;
using OneOf.Types;
using TourAPI.Models.Domains;
using TourAPI.Models.Request;
using TourAPI.Repository;

namespace TourAPI.Services
{
    public interface ITourService
    {
        Task<List<Tour>> GetAllAsync();
        Task<List<Tour>> GetByQuery(Querying query);
        Task<OneOf<Tour, NotFound>> GetByIdAsync(string Id);
        Task<List<Tour>> GetTourByCategory(string categoryId);
        Task<bool> CreateAsync(Tour entity, Image image);
        Task<bool> UpdateAsync(string Id, Tour entity, Image image);
        Task<bool> DeleteAsync(string Id);
    }

    public class TourService : ITourService
    {
        private readonly TourRepository TourRepo;
        private readonly ImageRepository imageRepo;
        private readonly IValidator<Tour> tourValidator;
        private readonly IValidator<Image> imageValidator;
        public TourService(TourRepository TourRepo, ImageRepository imageRepo
            , IValidator<Tour> tourValidator, IValidator<Image> imageValidator)
        {
            this.TourRepo = TourRepo;
            this.imageRepo = imageRepo;
            this.tourValidator = tourValidator;
            this.imageValidator = imageValidator;
        }
        public async Task<bool> CreateAsync(Tour tour, Image image)
        {
            var imageValidationResult = imageValidator.Validate(image);
            var tourValidationResult = tourValidator.Validate(tour);
            if (!imageValidationResult.IsValid)
            {
                throw new ValidationException(imageValidationResult.Errors);
            }
            if (!tourValidationResult.IsValid)
            {
                throw new ValidationException(tourValidationResult.Errors);
            }


            var existEntity = await TourRepo.GetByIdAsync(tour.Id);
            if (existEntity is not null)
            {
                return false;
            }
            image.TourId = tour.Id;
            await TourRepo.CreateAsync(tour);
            await imageRepo.Upload(image);
            return true;
        }

        public async Task<bool> DeleteAsync(string Id)
        {
            var result = await TourRepo.DeleteAsync(Id);
            imageRepo.Delete(Id);
            return result;
        }
        public async Task<List<Tour>> GetAllAsync()
        {
            var list = await TourRepo.GetAllAsync();
            return list;
        }
        public async Task<OneOf<Tour, NotFound>> GetByIdAsync(string Id)
        {
            var entity = await TourRepo.GetByIdAsync(Id);
            if (entity == null)
                return new NotFound();
            return entity;
        }
        public async Task<List<Tour>> GetByQuery(Querying query)
        {
            var list = await TourRepo.GetAllByQuery(query);
            return list;
        }

        public async Task<List<Tour>> GetTourByCategory(string categoryId)
        {
            var list = await TourRepo.GetTourByCategory(categoryId);
            return list;
        }
        public async Task<bool> UpdateAsync(string Id, Tour entity, Image image)
        {
            var validationResult = tourValidator.Validate(entity);
            var validationImageResult = imageValidator.Validate(image);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            if (!validationImageResult.IsValid)
            {
                throw new ValidationException(validationImageResult.Errors);
            }

            var existEntity = await TourRepo.GetByIdAsync(Id);
            if (existEntity is not null)
            {
                entity.Id = Id;
                image.TourId = Id;
                entity.CreateTimes = existEntity.CreateTimes;
                await TourRepo.UpdateAsync(entity);
                await imageRepo.Update(image);
                return true;
            }
            return false;
        }
    }
}


using FluentValidation;
using OneOf;
using OneOf.Types;
using TourAPI.Models.Domains;
using TourAPI.Repository;

namespace TourAPI.Services
{

    public interface IReviewService
    {
        Task<List<Review>> GetReviewByTourId(string tourId);
        Task<List<Review>> GetReviewByTour(string TourId);
        Task<OneOf<Review, NotFound>> GetByIdAsync(string Id);
        Task<bool> CreateAsync(Review entity);
        Task<bool> UpdateAsync(string Id, Review entity);
        Task<bool> DeleteAsync(string Id);
    }

    public class ReviewService : IReviewService
    {
        private readonly ReviewRepository repo;
        private readonly IValidator<Review> validator;
        public ReviewService(ReviewRepository repo, IValidator<Review> validator)
        {
            this.repo = repo;
            this.validator = validator;
        }
        public async Task<bool> CreateAsync(Review entity)
        {
            var validationResult = validator.Validate(entity);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var existEntity = await repo.GetByIdAsync(entity.Id);
            if (existEntity is not null)
            {
                return false;
            }
            await repo.CreateAsync(entity);
            return true;
        }
        public async Task<bool> DeleteAsync(string Id)
        {
            var result = await repo.DeleteAsync(Id);
            return result;
        }
        public async Task<OneOf<Review, NotFound>> GetByIdAsync(string Id)
        {
            var entity = await repo.GetByIdAsync(Id);
            if (entity == null)
                return new NotFound();
            return entity;
        }
        public async Task<List<Review>> GetReviewByTour(string TourId)
        {
            var list = await repo.GetReviewByTourId(TourId);
            return list;
        }
        public async Task<bool> UpdateAsync(string Id, Review entity)
        {
            var validationResult = validator.Validate(entity);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var existEntity = await repo.GetByIdAsync(Id);
            if (existEntity is not null)
            {
                entity.Id = Id;
                entity.CreateTimes = existEntity.CreateTimes;
                await repo.UpdateAsync(entity);
                return true;
            }
            return false;
        }
        public async Task<List<Review>> GetReviewByTourId(string tourId)
        {
            var list = await repo.GetReviewByTourId(tourId);
            return list;
        }
    }

}

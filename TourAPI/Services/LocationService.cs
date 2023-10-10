using FluentValidation;
using OneOf;
using OneOf.Types;
using TourAPI.Models.Domains;
using TourAPI.Repository;

namespace TourAPI.Services
{
    public interface ILocationService
    {
        Task<List<Location>> GetAllAsync();
        Task<OneOf<Location, NotFound>> GetByIdAsync(string Id);
        Task<bool> CreateAsync(Location entity);
        Task<bool> UpdateAsync(string Id, Location entity);
        Task<bool> DeleteAsync(string Id);
        List<Tour> GetTourByLocationName(string locationName);
    }

    public class LocationService : ILocationService
    {
        private readonly LocationRepository repo;
        private readonly IValidator<Location> validator;
        public LocationService(LocationRepository repo, IValidator<Location> validator)
        {
            this.repo = repo;
            this.validator = validator;
        }
        public async Task<bool> CreateAsync(Location entity)
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
        public async Task<List<Location>> GetAllAsync()
        {
            var list = await repo.GetAllAsync();
            return list;
        }
        public async Task<OneOf<Location, NotFound>> GetByIdAsync(string Id)
        {
            var entity = await repo.GetByIdAsync(Id);
            if (entity is null)
                return new NotFound();
            return entity;
        }

        public List<Tour> GetTourByLocationName(string locationName)
        {
            var list = repo.GetTourByLocationName(locationName);
            return list;
        }

        public async Task<bool> UpdateAsync(string Id, Location entity)
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
                await repo.UpdateAsync(entity);
                return true;
            }
            return false;
        }
    }
}

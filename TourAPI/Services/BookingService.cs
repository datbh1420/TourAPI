using FluentValidation;
using OneOf;
using OneOf.Types;
using TourAPI.Models.Domains;
using TourAPI.Repository;

namespace TourAPI.Services
{
    public interface IBookingService
    {
        Task<List<Booking>> GetAllAsync();
        Task<OneOf<Booking, NotFound>> GetByIdAsync(string Id);
        Task<bool> CreateAsync(Booking entity);
        Task<bool> UpdateAsync(string Id, Booking entity);
        Task<bool> DeleteAsync(string Id);
    }

    public class BookingService : IBookingService
    {
        private readonly BookingRepository repo;
        private readonly IValidator<Booking> validator;
        public BookingService(BookingRepository repo, IValidator<Booking> validator)
        {
            this.repo = repo;
            this.validator = validator;
        }
        public async Task<bool> CreateAsync(Booking entity)
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
        public async Task<List<Booking>> GetAllAsync()
        {
            var list = await repo.GetAllAsync();
            return list;
        }
        public async Task<OneOf<Booking, NotFound>> GetByIdAsync(string Id)
        {
            var entity = await repo.GetByIdAsync(Id);
            if (entity == null)
                return new NotFound();
            return entity;
        }
        public async Task<bool> UpdateAsync(string Id, Booking entity)
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
    }
}

using FluentValidation;
using OneOf;
using OneOf.Types;
using TourAPI.Models.Domains;
using TourAPI.Repository;

namespace TourAPI.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();
        Task<OneOf<Category, NotFound>> GetByIdAsync(string Id);
        Task<bool> CreateAsync(Category entity);
        Task<bool> UpdateAsync(string Id, Category entity);
        Task<bool> DeleteAsync(string Id);
    }

    public class CategoryService : ICategoryService
    {
        private readonly CategoryRepository repo;
        private readonly IValidator<Category> validator;
        public CategoryService(CategoryRepository repo, IValidator<Category> validator)
        {
            this.repo = repo;
            this.validator = validator;
        }
        public async Task<bool> CreateAsync(Category entity)
        {
            var validationResult = validator.Validate(entity);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }


            var existEntity = await repo.GetByIdAsync(entity.Id);
            if (existEntity is null)
            {
                await repo.CreateAsync(entity);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(string Id)
        {
            var result = await repo.DeleteAsync(Id);
            return result;
        }
        public async Task<List<Category>> GetAllAsync()
        {
            var list = await repo.GetAllAsync();
            return list;
        }
        public async Task<OneOf<Category, NotFound>> GetByIdAsync(string Id)
        {
            var entity = await repo.GetByIdAsync(Id);
            if (entity is null)
                return new NotFound();
            return entity;
        }
        public async Task<bool> UpdateAsync(string Id, Category entity)
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

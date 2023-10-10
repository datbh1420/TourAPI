using Microsoft.AspNetCore.Mvc;
using TourAPI.Models.Domains;
using TourAPI.Models.Dto;
using TourAPI.Models.Request;
using TourAPI.Services;

namespace TourAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService service;

        public CategoryController(ICategoryService service)
        {
            this.service = service;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            var list = await service.GetAllAsync();
            List<CategoryResponse> listResponse = list.Select(categories => (CategoryResponse)categories).ToList();
            return Ok(listResponse);
        }

        [HttpGet]
        [Route("{Id}:string")]
        public async Task<ActionResult> GetByIdAsync([FromRoute] string Id)
        {
            var result = await service.GetByIdAsync(Id);
            return result.Match<ActionResult>(
                entity => Ok((CategoryResponse)entity),
                _ => NotFound());
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromForm] CategoryRequest request)
        {
            var entity = (Category)request;
            var result = await service.CreateAsync(entity);
            if (result is true)
            {
                var response = (CategoryResponse)entity;
                return Ok(response);
            }
            return BadRequest("Category is already exist!");

        }

        [HttpPut()]
        [Route("{Id}:string")]
        public async Task<ActionResult> UpdateAsync([FromRoute] string Id, [FromForm] CategoryRequest request)
        {
            var entity = (Category)request;
            var result = await service.UpdateAsync(Id, entity);
            if (result)
            {
                return Ok((CategoryResponse)entity);
            }
            return BadRequest("Id was not exist");
        }

        [HttpDelete]
        [Route("{Id}:string")]
        public async Task<ActionResult> DeleteAsync([FromRoute] string Id)
        {
            var result = await service.DeleteAsync(Id);
            if (result)
            {
                var entity = await service.GetByIdAsync(Id);
                return Ok("Delete Successfully");
            }
            return BadRequest("Id was not exist");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using TourAPI.Models.Domains;
using TourAPI.Models.Request;
using TourAPI.Models.Response;
using TourAPI.Services;

namespace TourAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourController : ControllerBase
    {
        private readonly ITourService service;

        public TourController(ITourService service)
        {
            this.service = service;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAllAsync()
        {
            var list = await service.GetAllAsync();
            List<TourResponse> listResponse = list.Select(tours => (TourResponse)tours).ToList();
            return Ok(listResponse);
        }

        [HttpGet("GetByQuery")]
        public async Task<ActionResult> GetByQuery(Querying query)
        {
            var list = await service.GetByQuery(query);
            List<TourResponse> listResponse = list.Select(categories => (TourResponse)categories).ToList();
            return Ok(listResponse);
        }

        [HttpGet]
        [Route("{categoryId}:string")]
        public async Task<ActionResult> GetTourByCategory([FromRoute] string categoryId)
        {
            var list = await service.GetTourByCategory(categoryId);
            List<TourResponse> listResponse = list.Select(categories => (TourResponse)categories).ToList();
            return Ok(listResponse);
        }

        [HttpGet]
        [Route("{Id}:string")]
        public async Task<ActionResult> GetByIdAsync([FromRoute] string Id)
        {
            var result = await service.GetByIdAsync(Id);
            return result.Match<ActionResult>(
                entity => Ok((TourResponse)entity),
                _ => NotFound());
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromForm] TourRequest tourRequest, [FromForm] ImageRequest fileRequset)
        {
            var entityTour = (Tour)tourRequest;
            var entityImage = (Image)fileRequset;
            var result = await service.CreateAsync(entityTour, entityImage);
            if (result)
            {
                var response = (TourResponse)entityTour;
                response.Image = entityImage;
                return Ok(response);
            }
            return BadRequest("Tour is already exist!");

        }

        [HttpPut()]
        [Route("{Id}:string")]
        public async Task<ActionResult> Update([FromRoute] string Id, [FromForm] TourRequest tourRequest, [FromForm] ImageRequest imageRequest)
        {
            var entity = (Tour)tourRequest;
            var image = (Image)imageRequest;
            var result = await service.UpdateAsync(Id, entity, image);
            if (result)
            {
                return Ok((TourResponse)entity);
            }
            return BadRequest("Id was not exist");
        }

        [HttpDelete]
        [Route("{Id}:string")]
        public async Task<ActionResult> Delete([FromRoute] string Id)
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

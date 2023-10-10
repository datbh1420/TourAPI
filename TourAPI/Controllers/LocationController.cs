using Microsoft.AspNetCore.Mvc;
using TourAPI.Models.Domains;
using TourAPI.Models.Request;
using TourAPI.Models.Response;
using TourAPI.Services;

namespace TourAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService service;

        public LocationController(ILocationService service)
        {
            this.service = service;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            var list = await service.GetAllAsync();
            List<LocationResponse> listResponse = list.Select(categories => (LocationResponse)categories).ToList();
            return Ok(listResponse);
        }

        [HttpGet]
        [Route("{Id}:string")]
        public async Task<ActionResult> GetByIdAsync([FromRoute] string Id)
        {
            var result = await service.GetByIdAsync(Id);
            return result.Match<ActionResult>(
                entity => Ok((LocationResponse)entity),
                _ => NotFound());
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromForm] LocationRequest request)
        {
            var entity = (Location)request;
            var result = await service.CreateAsync(entity);
            if (result is true)
            {
                var response = (LocationResponse)entity;
                return Ok(response);
            }
            return BadRequest("Location is already exist!");

        }

        [HttpPut()]
        [Route("{Id}:string")]
        public async Task<ActionResult> UpdateAsync([FromRoute] string Id, [FromForm] LocationRequest request)
        {
            var entity = (Location)request;
            var result = await service.UpdateAsync(Id, entity);
            if (result)
            {
                return Ok((LocationResponse)entity);
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

        [HttpGet("GetTourByLocationName")]
        public ActionResult GetTourByLocationName(string locationName)
        {
            var list = service.GetTourByLocationName(locationName);
            List<TourResponse> listResponse = list.Select(tours => (TourResponse)tours).ToList();
            return Ok(listResponse);
        }
    }
}

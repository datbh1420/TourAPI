using Microsoft.AspNetCore.Mvc;
using TourAPI.Models.Domains;
using TourAPI.Models.Request;
using TourAPI.Models.Response;
using TourAPI.Services;

namespace TourAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService service;

        public ReviewController(IReviewService service)
        {
            this.service = service;
        }
        [HttpGet]
        public async Task<ActionResult> GetReviewByTourId(string tourId)
        {
            var list = await service.GetReviewByTourId(tourId);
            List<ReviewResponse> listResponse = list.Select(reviews => (ReviewResponse)reviews).ToList();
            return Ok(listResponse);
        }
        [HttpGet]
        [Route("{Id}:string")]
        public async Task<ActionResult> GetByIdAsync([FromRoute] string Id)
        {
            var result = await service.GetByIdAsync(Id);
            return result.Match<ActionResult>(
                entity => Ok((ReviewResponse)entity),
                _ => NotFound());
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromForm] ReviewRequest request)
        {
            var entity = (Review)request;
            var result = await service.CreateAsync(entity);
            if (result)
            {
                var response = (ReviewResponse)entity;
                return Ok(response);
            }
            return BadRequest("Review is already exist!");

        }

        [HttpPut()]
        [Route("{Id}:string")]
        public async Task<ActionResult> Update([FromRoute] string Id, [FromForm] ReviewRequest request)
        {
            var entity = (Review)request;
            var result = await service.UpdateAsync(Id, entity);
            if (result)
            {
                return Ok((ReviewResponse)entity);
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

using Microsoft.AspNetCore.Mvc;
using Axidel.WebApi.ApiServices.Tags;
using Axidel.WebApi.Models.Tags;
using Axidel.WebApi.Models.Commons;
using Axidel.Service.Configurations;

namespace Axidel.WebApi.Controllers
{
    public class TagsController(ITagApiService tagApiService) : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] TagCreateModel createModel)
        {
            if (createModel == null)
                return BadRequest(new Response
                {
                    StatusCode = 400,
                    Message = "Invalid tag data."
                });

            var result = await tagApiService.CreateAsync(createModel);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Id }, new Response
            {
                StatusCode = 201,
                Message = "Tag created successfully.",
                Data = result
            });
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var result = await tagApiService.DeleteAsync(id);

            return result ?
                NoContent() :
                NotFound(new Response
                {
                    StatusCode = 404,
                    Message = "Tag not found."
                });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await tagApiService.GetAllAsync();

            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Tags retrieved successfully.",
                Data = result
            });
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var result = await tagApiService.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound(new Response
                {
                    StatusCode = 404,
                    Message = "Tag not found."
                });
            }

            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Tag retrieved successfully.",
                Data = result
            });
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Axidel.WebApi.Models.Items;
using Axidel.WebApi.Models.Commons;
using Axidel.Service.Configurations;

namespace Axidel.WebApi.Controllers
{
    public class ItemsController(IItemApiService itemApiService) : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ItemCreateModel createModel)
        {
            if (createModel == null)
                return BadRequest(new Response
                {
                    StatusCode = 400,
                    Message = "Invalid item data."
                });

            var result = await itemApiService.CreateAsync(createModel);

            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Item created successfully.",
                Data = result
            });
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> PutAsync(long id, [FromBody] ItemUpdateModel updateModel)
        {
            if (updateModel == null)
                return BadRequest(new Response
                {
                    StatusCode = 400,
                    Message = "Invalid item data."
                });

            var result = await itemApiService.UpdateAsync(id, updateModel);

            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Item updated successfully.",
                Data = result
            });
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var result = await itemApiService.DeleteAsync(id);

            return Ok(new Response
            {
                StatusCode = result ? 200 : 404,
                Message = result ? "Item deleted successfully." : "Item not found.",
                Data = result
            });
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetAsync(long id)
        {
            var result = await itemApiService.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound(new Response
                {
                    StatusCode = 404,
                    Message = "Item not found."
                });
            }

            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Item retrieved successfully.",
                Data = result
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params, [FromQuery] Filter filter, [FromQuery] string search = null)
        {
            var result = await itemApiService.GetAllAsync(@params, filter, search);

            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Items retrieved successfully.",
                Data = result
            });
        }
    }
}
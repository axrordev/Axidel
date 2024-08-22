using Microsoft.AspNetCore.Mvc;
using Axidel.WebApi.ApiServices.CustomFieldValues;
using Axidel.WebApi.Models.CustomFieldValues;
using Axidel.WebApi.Models.Commons;

namespace Axidel.WebApi.Controllers
{
    public class CustomFieldValuesController(ICustomFieldValueApiService customFieldValueApiService) : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CustomFieldValueCreateModel createModel)
        {
            if (createModel == null)
                return BadRequest(new Response
                {
                    StatusCode = 400,
                    Message = "Invalid custom field value data."
                });

            var result = await customFieldValueApiService.CreateAsync(createModel);

            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Custom field value created successfully.",
                Data = result
            });
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> PutAsync(long id, [FromBody] CustomFieldValueUpdateModel updateModel)
        {
            if (updateModel == null)
                return BadRequest(new Response
                {
                    StatusCode = 400,
                    Message = "Invalid custom field value data."
                });

            var result = await customFieldValueApiService.UpdateAsync(id, updateModel);

            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Custom field value updated successfully.",
                Data = result
            });
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var result = await customFieldValueApiService.DeleteAsync(id);

            return Ok(new Response
            {
                StatusCode = result ? 200 : 404,
                Message = result ? "Custom field value deleted successfully." : "Custom field value not found.",
                Data = result
            });
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetAsync(long id)
        {
            var result = await customFieldValueApiService.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound(new Response
                {
                    StatusCode = 404,
                    Message = "Custom field value not found."
                });
            }

            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Custom field value retrieved successfully.",
                Data = result
            });
        }

        [HttpGet("item/{itemId:long}")]
        public async Task<IActionResult> GetAllByItemIdAsync(long itemId)
        {
            var result = await customFieldValueApiService.GetAllByItemIdAsync(itemId);

            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Custom field values retrieved successfully.",
                Data = result
            });
        }
    }
}

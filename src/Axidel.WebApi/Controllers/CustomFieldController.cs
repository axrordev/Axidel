using Axidel.WebApi.ApiServices.CustomFields;
using Axidel.WebApi.Models.Commons;
using Axidel.WebApi.Models.CustomFields;
using Microsoft.AspNetCore.Mvc;

namespace Axidel.WebApi.Controllers
{
    public class CustomFieldsController(ICustomFieldApiService customFieldApiService) : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CustomFieldCreateModel createModel)
        {
            if (createModel == null)
                return BadRequest(new Response
                {
                    StatusCode = 400,
                    Message = "Invalid custom field data."
                });

            var result = await customFieldApiService.CreateAsync(createModel);

            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Custom field created successfully.",
                Data = result
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await customFieldApiService.GetAllAsync();

            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Custom fields retrieved successfully.",
                Data = result
            });
        }
    }
}

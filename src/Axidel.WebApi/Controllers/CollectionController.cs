using Axidel.Service.Configurations;
using Axidel.WebApi.ApiServices.Collections;
using Axidel.WebApi.Models.Collections;
using Axidel.WebApi.Models.Commons;
using Microsoft.AspNetCore.Mvc;

namespace Axidel.WebApi.Controllers
{
    public class CollectionController(ICollectionApiService collectionApiService) : BaseController
    {

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CollectionCreateModel createModel)
        {
            if (createModel == null)
                return BadRequest("Invalid collection data.");

            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Collection created successfully.",
                Data = await collectionApiService.CreateAsync(createModel)
            });
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> PutAsync(long id, [FromBody] CollectionUpdateModel updateModel)
        {
            if (updateModel == null)
                return BadRequest("Invalid collection data.");

            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Collection updated successfully.",
                Data = await collectionApiService.UpdateAsync(id, updateModel)
            });
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Collection deleted successfully.",
                Data = await collectionApiService.DeleteAsync(id)
            });
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetAsync(long id)
        {
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Collection retrieved successfully.",
                Data = await collectionApiService.GetByIdAsync(id)
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync(
            [FromQuery] PaginationParams @params,
            [FromQuery] Filter filter)
        {
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Collection retrieved successfully.",
                Data = await collectionApiService.GetAllAsync(@params, filter)
            });
        }      
    }
}

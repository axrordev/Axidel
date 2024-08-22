using Axidel.WebApi.ApiServices.CollectionImages;
using Axidel.WebApi.Models.CollectionImages;
using Axidel.WebApi.Models.Commons;
using Microsoft.AspNetCore.Mvc;

namespace Axidel.WebApi.Controllers
{
    public class AdImageController(ICollectionImageApiService collectionImageApiService) : BaseController
    {
        [HttpPost]
        public async ValueTask<IActionResult> PostAsync(CollectionImageCreateModel createModel, IFormFile formFile)
        {
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await collectionImageApiService.CreateAsync(createModel.CollectionId, formFile)
            });
        }

        [HttpDelete("{id:long}")]
        public async ValueTask<IActionResult> DeleteAsync(long id)
        {
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await collectionImageApiService.DeleteAsync(id)
            });
        }

        [HttpGet("{id:long}")]
        public async ValueTask<IActionResult> GetAsync(long id)
        {
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await collectionImageApiService.GetByIdAsync(id)
            });
        }
    }
}

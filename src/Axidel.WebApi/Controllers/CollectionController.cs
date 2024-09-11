using Axidel.Service.Configurations;
using Axidel.Service.Services.Assets;
using Axidel.WebApi.ApiServices.Assets;
using Axidel.WebApi.ApiServices.Collections;
using Axidel.WebApi.Models.Collections;
using Axidel.WebApi.Models.Commons;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;

namespace Axidel.WebApi.Controllers
{
    public class CollectionController(ICollectionApiService collectionApiService, IAssetApiService assetApiService) : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm] CollectionCreateModel createModel, IFormFile file, string fileType = "Images")
        {
            if (createModel == null)
                return BadRequest("Invalid collection data.");

            // Faylni yuklash faqat fayl mavjud bo'lganda amalga oshiriladi
            if (file != null)
            {
                var uploadedAsset = await assetApiService.UploadAsync(file, fileType);
                createModel.ImageId = uploadedAsset.Id;
            }

            var collection = await collectionApiService.CreateAsync(createModel);
        
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Collection created successfully.",
                Data = collection 
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

        [HttpDelete("delete-collection/{id:long}")]
        public async Task<IActionResult> DeleteCollectionAsync(long id)
        {
            await collectionApiService.DeleteAsync(id);

            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Collection deleted successfully.",
                Data = null
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


        [HttpGet("get-image/{id:long}")]
        public async Task<IActionResult> GetImageAsync(long id)
        {
            var image = await assetApiService.GetByIdAsync(id);

            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Image retrieved successfully.",
                Data = image
            });
        }

    }
}
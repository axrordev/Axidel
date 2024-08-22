using Microsoft.AspNetCore.Mvc;
using Axidel.WebApi.Models.Itemtags;
using Axidel.WebApi.Models.Commons;


namespace Axidel.WebApi.Controllers
{
    public class ItemTagsController(IItemTagApiService itemTagApiService) : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> AddTagToItemAsync([FromBody] ItemTagCreateModel createModel)
        {
            if (createModel == null)
                return BadRequest(new Response
                {
                    StatusCode = 400,
                    Message = "Invalid item-tag data."
                });

            var result = await itemTagApiService.AddTagToItemAsync(createModel);

            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Tag added to item successfully.",
                Data = result
            });
        }

        [HttpDelete("{itemId:long}/{tagId:long}")]
        public async Task<IActionResult> RemoveTagFromItemAsync(long itemId, long tagId)
        {
            var result = await itemTagApiService.RemoveTagFromItemAsync(itemId, tagId);

            return result ?
                NoContent() :
                NotFound(new Response
                {
                    StatusCode = 404,
                    Message = "Item or tag not found."
                });
        }

        [HttpGet("{itemId:long}/tags")]
        public async Task<IActionResult> GetTagsByItemIdAsync(long itemId)
        {
            var result = await itemTagApiService.GetTagsByItemIdAsync(itemId);

            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Tags retrieved successfully.",
                Data = result
            });
        }
    }
}

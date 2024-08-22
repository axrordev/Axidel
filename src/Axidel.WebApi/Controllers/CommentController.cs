using Microsoft.AspNetCore.Mvc;
using Axidel.WebApi.ApiServices.Comments;
using Axidel.WebApi.Models.Comments;
using Axidel.WebApi.Models.Commons;
using Axidel.Service.Configurations;

namespace Axidel.WebApi.Controllers
{
    public class CommentsController(ICommentApiService commentApiService) : BaseController
    {

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CommentCreateModel createModel)
        {
            if (createModel == null)
                return BadRequest(new Response
                {
                    StatusCode = 400,
                    Message = "Invalid comment data."
                });

            var result = await commentApiService.CreateAsync(createModel);

            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Comment created successfully.",
                Data = result
            });
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> PutAsync(long id, [FromBody] CommentUpdateModel updateModel)
        {
            if (updateModel == null)
                return BadRequest(new Response
                {
                    StatusCode = 400,
                    Message = "Invalid comment data."
                });

            var result = await commentApiService.UpdateAsync(id, updateModel);

            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Comment updated successfully.",
                Data = result
            });
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var result = await commentApiService.DeleteAsync(id);

            return Ok(new Response
            {
                StatusCode = result ? 200 : 404,
                Message = result ? "Comment deleted successfully." : "Comment not found.",
                Data = result
            });
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetAsync(long id)
        {
            var result = await commentApiService.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound(new Response
                {
                    StatusCode = 404,
                    Message = "Comment not found."
                });
            }

            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Comment retrieved successfully.",
                Data = result
            });
        }

        [HttpGet("item/{itemId:long}")]
        public async Task<IActionResult> GetAllByItemAsync(long itemId, [FromQuery] PaginationParams @params, [FromQuery] Filter filter)
        {
            var result = await commentApiService.GetAllByItemAsync(itemId, @params, filter);

            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Comments retrieved successfully.",
                Data = result
            });
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Axidel.WebApi.ApiServices.Likes;
using Axidel.WebApi.Models.Likes;
using Axidel.WebApi.Models.Commons;
using Axidel.Service.Configurations;

namespace Axidel.WebApi.Controllers
{
    public class LikesController(ILikeApiService likeApiService) : BaseController
    {

        [HttpPost("toggle/{itemId:long}/{userId:long}")]
        public async Task<IActionResult> ToggleLikeAsync(long itemId, long userId)
        {
            var result = await likeApiService.ToggleLikeAsync(itemId, userId);

            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Like toggled successfully.",
                Data = result
            });
        }

        [HttpGet("{itemId:long}/count")]
        public async Task<IActionResult> GetLikesCountAsync(long itemId)
        {
            var count = await likeApiService.GetLikesCountAsync(itemId);

            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Likes count retrieved successfully.",
                Data = count
            });
        }
    }
}

using AutoMapper;
using Axidel.Service.Services.Likes;
using Axidel.WebApi.Models.Likes;

namespace Axidel.WebApi.ApiServices.Likes
{
    public class LikeApiService(ILikeService likeService, IMapper mapper) : ILikeApiService
    {
        public async ValueTask<LikeViewModel> ToggleLikeAsync(long itemId, long userId)
        {
            var like = await likeService.ToggleLikeAsync(itemId, userId);
            return mapper.Map<LikeViewModel>(like);
        }

        public async ValueTask<int> GetLikesCountAsync(long itemId)
        {
            return await likeService.GetLikesCountAsync(itemId);
        }
    }
}

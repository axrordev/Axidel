using Axidel.Domain.Entities.Items;

namespace Axidel.Service.Services.Likes;

public interface ILikeService
{
    ValueTask<Like> ToggleLikeAsync(long itemId, long userId);
    ValueTask<int> GetLikesCountAsync(long itemId);
}

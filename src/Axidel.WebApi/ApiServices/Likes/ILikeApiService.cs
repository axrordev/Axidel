using Axidel.WebApi.Models.Likes;

public interface ILikeApiService
{
    ValueTask<LikeViewModel> ToggleLikeAsync(long itemId, long userId);
    ValueTask<int> GetLikesCountAsync(long itemId);
}
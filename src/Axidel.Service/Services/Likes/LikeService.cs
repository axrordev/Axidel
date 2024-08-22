using Axidel.Data.UnitOfWorks;
using Axidel.Domain.Entities.Items;
using Axidel.Service.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Axidel.Service.Services.Likes;

public class LikeService(IUnitOfWork unitOfWork) : ILikeService
{
    public async ValueTask<Like> ToggleLikeAsync(long itemId, long userId)
    {
        var item = await unitOfWork.ItemRepository.SelectAsync(i => i.Id == itemId)
            ?? throw new NotFoundException($"Item with ID={itemId} not found.");

        var existingLike = await unitOfWork.LikeRepository
            .SelectAsync(l => l.ItemId == itemId && l.UserId == userId);

        if (existingLike is not null)
        {
            // User has already liked the item, so we remove the like
            await unitOfWork.LikeRepository.DeleteAsync(existingLike);
            await unitOfWork.SaveAsync();
            return null; // Returning null to indicate that the like was removed
        }
        else
        {
            // User has not liked the item, so we add a like
            var like = new Like
            {
                ItemId = itemId,
                UserId = userId
            };

            await unitOfWork.LikeRepository.InsertAsync(like);
            await unitOfWork.SaveAsync();
            return like;
        }
    }

    public async ValueTask<int> GetLikesCountAsync(long itemId)
    {
        var item = await unitOfWork.ItemRepository.SelectAsync(i => i.Id == itemId)
            ?? throw new NotFoundException($"Item with ID={itemId} not found.");

        var likeCount = await unitOfWork.LikeRepository.Select()
            .CountAsync(l => l.ItemId == itemId);

        return likeCount;
    }
}
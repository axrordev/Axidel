using Axidel.Data.UnitOfWorks;
using Axidel.Domain.Entities.Items;
using Axidel.Service.Configurations;
using Axidel.Service.Exceptions;
using Axidel.Service.Extensions;
using Axidel.Service.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Axidel.Service.Services.Comments;

public class CommentService(IUnitOfWork unitOfWork) : ICommentService
{
    public async ValueTask<Comment> CreateAsync(Comment comment)
    {
        var existingItem = await unitOfWork.ItemRepository.SelectAsync(i => i.Id == comment.ItemId);
        if (existingItem == null)
            throw new NotFoundException($"Item not found with ID {comment.ItemId}");

        var existingUser = await unitOfWork.UserRepository.SelectAsync(u => u.Id == comment.UserId);
        if (existingUser == null)
            throw new NotFoundException($"User not found with ID {comment.UserId}");

        var createdComment = await unitOfWork.CommentRepository.InsertAsync(comment);
        await unitOfWork.SaveAsync();
        return createdComment;
    }

    public async ValueTask<Comment> UpdateAsync(long id, Comment comment)
    {
        var existingComment = await unitOfWork.CommentRepository.SelectAsync(c => c.Id == id)
            ?? throw new NotFoundException($"Comment not found with ID {id}");

        existingComment.Text = comment.Text;
        existingComment.UpdatedById = HttpContextHelper.GetUserId;

        var updatedComment = await unitOfWork.CommentRepository.UpdateAsync(existingComment);
        await unitOfWork.SaveAsync();
        return updatedComment;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existingComment = await unitOfWork.CommentRepository.SelectAsync(c => c.Id == id)
            ?? throw new NotFoundException($"Comment not found with ID {id}");

        existingComment.DeletedById = HttpContextHelper.GetUserId;
        await unitOfWork.CommentRepository.DeleteAsync(existingComment);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<Comment> GetByIdAsync(long id)
    {
        return await unitOfWork.CommentRepository
            .SelectAsync(expression: c => c.Id == id, includes: [ "Item", "User" ])
            ?? throw new NotFoundException($"Comment not found with ID {id}");
    }

    public async ValueTask<IEnumerable<Comment>> GetAllByItemAsync(long itemId, PaginationParams @params, Filter filter)
    {
        var comments = unitOfWork.CommentRepository.Select(c => c.ItemId == itemId, includes: [ "Item", "User" ]).OrderBy(filter);
        var pagedComments = comments.ToPaginateAsQueryable(@params);
        return await pagedComments.ToListAsync();
    }
}
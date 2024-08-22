using AutoMapper;
using Axidel.Domain.Entities.Items;
using Axidel.Service.Configurations;
using Axidel.Service.Services.Comments;
using Axidel.WebApi.Models.Comments;

namespace Axidel.WebApi.ApiServices.Comments;

public class CommentApiService(ICommentService commentService, IMapper mapper) : ICommentApiService
{
    public async ValueTask<CommentViewModel> CreateAsync(CommentCreateModel createModel)
    {
        var createdComment = await commentService.CreateAsync(mapper.Map<Comment>(createModel));
        return mapper.Map<CommentViewModel>(createdComment);
    }

    public async ValueTask<CommentViewModel> UpdateAsync(long id, CommentUpdateModel updateModel)
    {
        var updatedComment = await commentService.UpdateAsync(id, mapper.Map<Comment>(updateModel));
        return mapper.Map<CommentViewModel>(updatedComment);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        return await commentService.DeleteAsync(id);
    }

    public async ValueTask<CommentViewModel> GetByIdAsync(long id)
    {
        var comment = await commentService.GetByIdAsync(id);
        return mapper.Map<CommentViewModel>(comment);
    }

    public async ValueTask<IEnumerable<CommentViewModel>> GetAllByItemAsync(long itemId, PaginationParams @params, Filter filter)
    {
        var comments = await commentService.GetAllByItemAsync(itemId, @params, filter);
        return mapper.Map<IEnumerable<CommentViewModel>>(comments);
    }
}

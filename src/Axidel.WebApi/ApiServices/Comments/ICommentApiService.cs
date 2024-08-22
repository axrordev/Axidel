using Axidel.Service.Configurations;
using Axidel.WebApi.Models.Comments;

namespace Axidel.WebApi.ApiServices.Comments;

public interface ICommentApiService
{
    ValueTask<CommentViewModel> CreateAsync(CommentCreateModel createModel);
    ValueTask<CommentViewModel> UpdateAsync(long id, CommentUpdateModel updateModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<CommentViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<CommentViewModel>> GetAllByItemAsync(long itemId, PaginationParams @params, Filter filter);
}
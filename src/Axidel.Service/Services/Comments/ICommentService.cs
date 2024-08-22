using Axidel.Domain.Entities.Items;
using Axidel.Service.Configurations;
using Microsoft.AspNetCore.Http;
using System.Xml.Linq;

namespace Axidel.Service.Services.Comments;

public interface ICommentService 
{
    ValueTask<Comment> CreateAsync(Comment comment);
    ValueTask<Comment> UpdateAsync(long id, Comment comment);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<Comment> GetByIdAsync(long id);
    ValueTask<IEnumerable<Comment>> GetAllByItemAsync(long itemId, PaginationParams @params, Filter filter);
}
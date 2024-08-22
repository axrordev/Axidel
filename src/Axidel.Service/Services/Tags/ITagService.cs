using Axidel.Domain.Entities.Tags;

namespace Axidel.Service.Services.Tags;

public interface ITagService
{
    ValueTask<Tag> CreateAsync(Tag tag);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<IEnumerable<Tag>> GetAllAsync();
    ValueTask<Tag> GetByIdAsync(long id);
}
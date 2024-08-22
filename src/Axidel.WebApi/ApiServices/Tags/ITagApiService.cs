using Axidel.WebApi.Models.Tags;

public interface ITagApiService
{
    ValueTask<TagViewModel> CreateAsync(TagCreateModel createModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<IEnumerable<TagViewModel>> GetAllAsync();
    ValueTask<TagViewModel> GetByIdAsync(long id);
}

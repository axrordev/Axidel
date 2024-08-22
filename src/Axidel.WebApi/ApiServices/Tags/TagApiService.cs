using AutoMapper;
using Axidel.Domain.Entities.Tags;
using Axidel.Service.Services.Tags;
using Axidel.WebApi.ApiServices.Items;
using Axidel.WebApi.Models.Tags;

namespace Axidel.WebApi.ApiServices.Tags
{
    public class TagApiService(ITagService tagService, IMapper mapper) : ITagApiService
    {
        public async ValueTask<TagViewModel> CreateAsync(TagCreateModel createModel)
        {
            var tag = await tagService.CreateAsync(mapper.Map<Tag>(createModel));
            return mapper.Map<TagViewModel>(tag);
        }

        public async ValueTask<bool> DeleteAsync(long id)
        {
            return await tagService.DeleteAsync(id);
        }

        public async ValueTask<IEnumerable<TagViewModel>> GetAllAsync()
        {
            var tags = await tagService.GetAllAsync();
            return mapper.Map<IEnumerable<TagViewModel>>(tags);
        }

        public async ValueTask<TagViewModel> GetByIdAsync(long id)
        {
            var tag = await tagService.GetByIdAsync(id);
            return mapper.Map<TagViewModel>(tag);
        }
    }
}
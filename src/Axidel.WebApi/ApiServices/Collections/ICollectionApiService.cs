using Axidel.Service.Configurations;
using Axidel.WebApi.Models.Collections;

namespace Axidel.WebApi.ApiServices.Collections;

public interface ICollectionApiService
{
    ValueTask<CollectionViewModel> CreateAsync(CollectionCreateModel createModel);
    ValueTask<CollectionViewModel> UpdateAsync(long id, CollectionUpdateModel updateModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<CollectionViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<CollectionViewModel>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}

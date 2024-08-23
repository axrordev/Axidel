using Axidel.Domain.Entities.Collections;
using Axidel.Domain.Entities.Items;

namespace Axidel.Service.Services.SearchServices;

public interface ISearchService
{
    ValueTask<List<Item>> SearchItemsAsync(string searchTerm);
    ValueTask<List<Collection>> SearchCollectionsAsync(string searchTerm);
}

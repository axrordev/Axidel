using Axidel.Data.UnitOfWorks;
using Axidel.Domain.Entities.Collections;
using Axidel.Domain.Entities.Items;
using Microsoft.EntityFrameworkCore;

namespace Axidel.Service.Services.SearchServices;

public class SearchService(IUnitOfWork unitOfWork) : ISearchService
{

    public async ValueTask<List<Item>> SearchItemsAsync(string searchTerm)
    {
        var items = await unitOfWork.ItemRepository
            .Select() 
            .Include(i => i.Comments) 
            .Where(i => i.Comments.Any(cm => cm.Text.Contains(searchTerm)))
            .ToListAsync();

        return items;
    }

    public async ValueTask<List<Collection>> SearchCollectionsAsync(string searchTerm)
    {
        var collections = await unitOfWork.CollectionRepository
            .Select() 
            .Include(c => c.Items)
                .ThenInclude(i => i.Comments) 
            .Where(c => c.Items.Any(i => i.Comments.Any(cm => cm.Text.Contains(searchTerm))))
            .ToListAsync();

        return collections;
    }
}

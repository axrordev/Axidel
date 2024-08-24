using Axidel.Data.DbContexts;
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
            .Select(includes: new[] { "Comments" })
            .Where(i => i.Name.ToLower().Contains(searchTerm.ToLower()) ||
                        i.Comments.Any(cm => cm.Text.ToLower().Contains(searchTerm.ToLower())))
            .ToListAsync();

        return items;
    }

    public async ValueTask<List<Collection>> SearchCollectionsAsync(string searchTerm)
    {
        var collections = await unitOfWork.CollectionRepository
            .Select(includes: new[] { "Items", "Items.Comments" })
            .Where(c => c.Name.ToLower().Contains(searchTerm.ToLower()) ||
                        c.Description.ToLower().Contains(searchTerm.ToLower()) ||
                        c.Items.Any(i => i.Name.ToLower().Contains(searchTerm.ToLower()) ||
                                         i.Comments.Any(cm => cm.Text.ToLower().Contains(searchTerm.ToLower()))))
            .ToListAsync();

        return collections;
    }

}


using Axidel.Service.Services.SearchServices;
using Axidel.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Axidel.WebApi.Controllers;
public class SearchController : ControllerBase
{
    private readonly ISearchService searchService;

    public SearchController(ISearchService searchService)
    {
        this.searchService = searchService;
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search(string query)
    {
        var items = await searchService.SearchItemsAsync(query);
        var collections = await searchService.SearchCollectionsAsync(query);

        var searchResults = new List<SearchResultViewModel>();

        searchResults.AddRange(items.Select(item => new SearchResultViewModel
        {
            ResultType = "Item",
            Name = item.Name,
            Url = Url.Action("Details", "Items", new { id = item.Id })
        }));

        searchResults.AddRange(collections.Select(collection => new SearchResultViewModel
        {
            ResultType = "Collection",
            Name = collection.Name,
            Url = Url.Action("Details", "Collections", new { id = collection.Id })
        }));

        return Ok(searchResults);
    }
}

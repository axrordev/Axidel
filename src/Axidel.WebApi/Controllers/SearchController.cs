using Axidel.Service.Services.SearchServices;
using Microsoft.AspNetCore.Mvc;

namespace Axidel.WebApi.Controllers;

public class SearchController(ISearchService searchService) : BaseController
{

    [HttpGet("items")]
    public async ValueTask<IActionResult> SearchItems([FromQuery] string searchTerm)
    {
        var items = await searchService.SearchItemsAsync(searchTerm);
        return Ok(items);
    }

    [HttpGet("collections")]
    public async ValueTask<IActionResult> SearchCollections([FromQuery] string searchTerm)
    {
        var collections = await searchService.SearchCollectionsAsync(searchTerm);
        return Ok(collections);
    }
}
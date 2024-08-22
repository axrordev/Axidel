using Axidel.WebApi.Models.Itemtags;

namespace Axidel.WebApi.Models.Tags;

public class TagViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public ICollection<ItemTagViewModel> ItemTags { get; set; } = new List<ItemTagViewModel>();
}


using Axidel.WebApi.Models.Items;
using Axidel.WebApi.Models.Tags;

namespace Axidel.WebApi.Models.Itemtags;

public class ItemTagViewModel
{
    public long Id { get; set; }
    public ItemViewModel Item { get; set; }
    public TagViewModel Tag { get; set; }
}

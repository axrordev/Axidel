using Axidel.Domain.Entities.Items;
using Axidel.Domain.Entities.Tags;

namespace Axidel.WebApi.Models.Itemtags;

public class ItemTagCreateModel
{
    public long ItemId { get; set; }
    public long TagId { get; set; }
}

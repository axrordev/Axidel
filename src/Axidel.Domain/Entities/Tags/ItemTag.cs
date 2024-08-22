using Axidel.Domain.Commons;
using Axidel.Domain.Entities.Items;

namespace Axidel.Domain.Entities.Tags;

public class ItemTag : Auditable
{
    public long ItemId { get; set; }
    public Item Item { get; set; }
    public long TagId { get; set; }
    public Tag Tag { get; set; }
}
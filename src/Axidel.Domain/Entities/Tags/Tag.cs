using Axidel.Domain.Commons;


namespace Axidel.Domain.Entities.Tags;

public class Tag : Auditable
{
    public string Name { get; set; } 
    public ICollection<ItemTag> ItemTags { get; set; } = new List<ItemTag>();
}
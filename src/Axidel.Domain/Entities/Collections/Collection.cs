using Axidel.Domain.Commons;
using Axidel.Domain.Entities.Commons;
using Axidel.Domain.Entities.Items;
using Axidel.Domain.Entities.Users;

namespace Axidel.Domain.Entities.Collections;

public class Collection : Auditable
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }//“Books”, “Signs”, “Silverware”,   
    public long? ImageId { get; set; }
    public Asset Image { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
    public ICollection<Item> Items { get; set; } = new List<Item>();
    public ICollection<CustomField> CustomFields { get; set; } = new List<CustomField>();
}
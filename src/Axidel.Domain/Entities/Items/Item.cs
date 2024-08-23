using Axidel.Domain.Commons;
using Axidel.Domain.Entities.Collections;
using Axidel.Domain.Entities.Tags;
using Axidel.Domain.Entities.Users;
using System.Text.Json.Serialization;


namespace Axidel.Domain.Entities.Items;

public class Item : Auditable
{
    public string Name { get; set; }
    public long? CollectionId { get; set; }
    public Collection Collection { get; set; }
    public long? UserId { get; set; }
    public User User { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public ICollection<Like> Likes { get; set; }
    public ICollection<ItemTag> ItemTags { get; set; }
    public ICollection<CustomFieldValue> CustomFieldValues { get; set; } = new List<CustomFieldValue>();
}
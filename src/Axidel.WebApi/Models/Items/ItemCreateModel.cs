using Axidel.Domain.Entities.Tags;
using Axidel.Domain.Entities.Users;

namespace Axidel.WebApi.Models.Items;

public class ItemCreateModel
{
    public string Name { get; set; }
    public long? CollectionId { get; set; }
    public long? UserId { get; set; }
}

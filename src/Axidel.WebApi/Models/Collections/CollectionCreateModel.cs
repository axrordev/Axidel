using Axidel.Domain.Entities.Commons;
using Axidel.Domain.Entities.Users;

namespace Axidel.WebApi.Models.Collections;

public class CollectionCreateModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public long? ImageId { get; set; }
    public long UserId { get; set; }
}

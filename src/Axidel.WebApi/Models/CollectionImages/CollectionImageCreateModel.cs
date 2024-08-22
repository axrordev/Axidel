
using Axidel.Domain.Entities.Commons;

namespace Axidel.WebApi.Models.CollectionImages;

public class CollectionImageCreateModel
{
    public long CollectionId { get; set; }
    public long ImageId { get; set; }
}

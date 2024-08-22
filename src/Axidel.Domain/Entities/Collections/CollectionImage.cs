using Axidel.Domain.Commons;
using Axidel.Domain.Entities.Commons;

namespace Axidel.Domain.Entities.Collections
{
    public class CollectionImage : Auditable
    {
        public long CollectionId { get; set; }
        public Collection Collection { get; set; }
        public long ImageId { get; set; }
        public Asset Image { get; set; }
    }
}

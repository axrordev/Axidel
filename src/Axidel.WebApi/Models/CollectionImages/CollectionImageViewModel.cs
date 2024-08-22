using Axidel.WebApi.Models.Assets;
using Axidel.WebApi.Models.Collections;

namespace Axidel.WebApi.Models.CollectionImages;

public class CollectionImageViewModel
{
    public long Id { get; set; }
    public CollectionViewModel Collection { get; set; }
    public AssetViewModel Image { get; set; }
}

using Axidel.Domain.Entities.Collections;
using Axidel.Domain.Entities.Items;
using Axidel.WebApi.Models.Assets;
using Axidel.WebApi.Models.CustomFields;
using Axidel.WebApi.Models.Items;
using Axidel.WebApi.Models.Users;

namespace Axidel.WebApi.Models.Collections;

public class CollectionViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; } 
    public AssetViewModel Image { get; set; }
    public UserViewModel User { get; set; }
    public ICollection<ItemViewModel> Items { get; set; } = new List<ItemViewModel>();
    public ICollection<CustomFieldViewModel> CustomFields { get; set; } = new List<CustomFieldViewModel>();
}
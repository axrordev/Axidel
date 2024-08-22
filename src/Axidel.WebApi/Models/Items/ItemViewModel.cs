using Axidel.Domain.Entities.Items;
using Axidel.WebApi.Models.Collections;
using Axidel.WebApi.Models.Comments;
using Axidel.WebApi.Models.CustomFieldValues;
using Axidel.WebApi.Models.Itemtags;
using Axidel.WebApi.Models.Likes;
using Axidel.WebApi.Models.Users;

namespace Axidel.WebApi.Models.Items;

public class ItemViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public CollectionViewModel Collection { get; set; }
    public UserViewModel User { get; set; }
    public ICollection<CommentViewModel> Comments { get; set; }
    public ICollection<LikeViewModel> Likes { get; set; }
    public ICollection<ItemTagViewModel> ItemTags { get; set; }
    public ICollection<CustomFieldValueViewModel> CustomFieldValues { get; set; } = new List<CustomFieldValueViewModel>();
}
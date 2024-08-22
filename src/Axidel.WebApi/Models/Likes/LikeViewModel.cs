using Axidel.WebApi.Models.Items;
using Axidel.WebApi.Models.Users;

namespace Axidel.WebApi.Models.Likes;

public class LikeViewModel
{
    public long Id { get; set; }
    public UserViewModel User { get; set; }
    public ItemViewModel Item { get; set; }
}

using Axidel.Domain.Entities.Items;
using Axidel.Domain.Entities.Users;

namespace Axidel.WebApi.Models.Likes;
public class LikeCreateModel
{
    public long UserId { get; set; }
    public long ItemId { get; set; }
}

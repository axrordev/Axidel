using Axidel.Domain.Entities.Items;
using Axidel.Domain.Entities.Users;

namespace Axidel.WebApi.Models.Comments;

public class CommentCreateModel
{
    public long ItemId { get; set; }
    public long UserId { get; set; }
    public string Text { get; set; }
}

using Axidel.WebApi.Models.Items;
using Axidel.WebApi.Models.Users;

namespace Axidel.WebApi.Models.Comments;

public class CommentViewModel
{
    public long Id { get; set; }
    public ItemViewModel Item { get; set; }
    public UserViewModel User { get; set; }
    public string Text { get; set; }
}

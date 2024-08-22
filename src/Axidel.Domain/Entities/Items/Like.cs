using Axidel.Domain.Commons;
using Axidel.Domain.Entities.Users;

namespace Axidel.Domain.Entities.Items;

public class Like : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public long ItemId { get; set; }
    public Item Item { get; set; }
}
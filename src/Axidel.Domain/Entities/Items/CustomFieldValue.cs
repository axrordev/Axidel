using Axidel.Domain.Commons;
using Axidel.Domain.Entities.Collections;

namespace Axidel.Domain.Entities.Items;

public class CustomFieldValue : Auditable
{
    public string Value { get; set; }
    public long CustomFieldId { get; set; }
    public CustomField CustomField { get; set; }
    public long ItemId { get; set; }
    public Item Item { get; set; }
}
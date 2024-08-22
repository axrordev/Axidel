using Axidel.Domain.Entities.Collections;
using Axidel.Domain.Entities.Items;
using Axidel.WebApi.Models.CustomFields;
using Axidel.WebApi.Models.Items;

namespace Axidel.WebApi.Models.CustomFieldValues;

public class CustomFieldValueCreateModel
{
    public string Value { get; set; }
    public long CustomFieldId { get; set; }
    public long ItemId { get; set; }
}
public class CustomFieldValueUpdateModel
{
    public string Value { get; set; }
}
public class CustomFieldValueViewModel
{
    public long CustomFieldId { get; set; }
    public string Value { get; set; }
    public CustomFieldViewModel CustomField { get; set; }
    public ItemViewModel Item { get; set; }
}

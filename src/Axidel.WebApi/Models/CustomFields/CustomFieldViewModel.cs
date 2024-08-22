using Axidel.Domain.Enums;
using Axidel.WebApi.Models.Collections;

namespace Axidel.WebApi.Models.CustomFields;

public class CustomFieldViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public CustomFieldType FieldType { get; set; }
    public CollectionViewModel Collection { get; set; }
}

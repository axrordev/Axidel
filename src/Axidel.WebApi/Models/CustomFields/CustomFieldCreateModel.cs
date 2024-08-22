using Axidel.Domain.Enums;

namespace Axidel.WebApi.Models.CustomFields;

public class CustomFieldCreateModel
{
    public string Name { get; set; }
    public CustomFieldType FieldType { get; set; }
    public long? CollectionId { get; set; }
}

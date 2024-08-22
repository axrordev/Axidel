using Axidel.Domain.Commons;
using Axidel.Domain.Enums;

namespace Axidel.Domain.Entities.Collections;

public class CustomField : Auditable
{
    public string Name { get; set; }
    public CustomFieldType FieldType { get; set; }
    public long? CollectionId { get; set; }  
    public Collection Collection { get; set; }
}
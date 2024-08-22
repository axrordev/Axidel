using Axidel.Domain.Entities.Collections;

namespace Axidel.Service.Services.CustomFields;

public interface ICustomFieldService
{
    ValueTask<CustomField> CreateAsync(CustomField customField);
    ValueTask<IEnumerable<CustomField>> GetAllAsync();
}
using Axidel.Domain.Entities.Items;

namespace Axidel.Service.Services.CustomFieldValues;

public interface ICustomFieldValueService
{
    ValueTask<CustomFieldValue> CreateAsync(CustomFieldValue customFieldValue);
    ValueTask<CustomFieldValue> UpdateAsync(long id, CustomFieldValue customFieldValue);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<CustomFieldValue> GetByIdAsync(long id);
    ValueTask<IEnumerable<CustomFieldValue>> GetAllByItemIdAsync(long itemId);
}
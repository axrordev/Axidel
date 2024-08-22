using Axidel.WebApi.Models.CustomFieldValues;

public interface ICustomFieldValueApiService
{
    ValueTask<CustomFieldValueViewModel> CreateAsync(CustomFieldValueCreateModel createModel);
    ValueTask<CustomFieldValueViewModel> UpdateAsync(long id, CustomFieldValueUpdateModel updateModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<CustomFieldValueViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<CustomFieldValueViewModel>> GetAllByItemIdAsync(long itemId);
}

using Axidel.WebApi.Models.CustomFields;

namespace Axidel.WebApi.ApiServices.CustomFields;

public interface ICustomFieldApiService
{
    ValueTask<CustomFieldViewModel> CreateAsync(CustomFieldCreateModel createModel);
    ValueTask<IEnumerable<CustomFieldViewModel>> GetAllAsync();
}
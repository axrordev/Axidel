using AutoMapper;
using Axidel.Domain.Entities.Collections;
using Axidel.Service.Services.CustomFields;
using Axidel.WebApi.Models.CustomFields;

namespace Axidel.WebApi.ApiServices.CustomFields;

public class CustomFieldApiService(ICustomFieldService customFieldService, IMapper mapper) : ICustomFieldApiService
{
    public async ValueTask<CustomFieldViewModel> CreateAsync(CustomFieldCreateModel createModel)
    {
        var createdCustomField = await customFieldService.CreateAsync(mapper.Map<CustomField>(createModel));
        return mapper.Map<CustomFieldViewModel>(createdCustomField);
    }

    public async ValueTask<IEnumerable<CustomFieldViewModel>> GetAllAsync()
    {
        var result = await customFieldService.GetAllAsync();
        return mapper.Map<IEnumerable<CustomFieldViewModel>>(result);
    }
}

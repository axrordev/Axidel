using AutoMapper;
using Axidel.Domain.Entities.Items;
using Axidel.Service.Services.CustomFieldValues;
using Axidel.WebApi.Models.CustomFieldValues;

namespace Axidel.WebApi.ApiServices.CustomFieldValues
{
    public class CustomFieldValueApiService(ICustomFieldValueService customFieldValueService, IMapper mapper) : ICustomFieldValueApiService
    {
        public async ValueTask<CustomFieldValueViewModel> CreateAsync(CustomFieldValueCreateModel createModel)
        {
            var createdCustomFieldValue = await customFieldValueService.CreateAsync(mapper.Map<CustomFieldValue>(createModel));
            return mapper.Map<CustomFieldValueViewModel>(createdCustomFieldValue);
        }

        public async ValueTask<CustomFieldValueViewModel> UpdateAsync(long id, CustomFieldValueUpdateModel updateModel)
        {
            var updatedCustomFieldValue = await customFieldValueService.UpdateAsync(id, mapper.Map<CustomFieldValue>(updateModel));
            return mapper.Map<CustomFieldValueViewModel>(updatedCustomFieldValue);
        }

        public async ValueTask<bool> DeleteAsync(long id)
        {
            return await customFieldValueService.DeleteAsync(id);
        }

        public async ValueTask<CustomFieldValueViewModel> GetByIdAsync(long id)
        {
            var customFieldValue = await customFieldValueService.GetByIdAsync(id);
            return mapper.Map<CustomFieldValueViewModel>(customFieldValue);
        }

        public async ValueTask<IEnumerable<CustomFieldValueViewModel>> GetAllByItemIdAsync(long itemId)
        {
            var customFieldValues = await customFieldValueService.GetAllByItemIdAsync(itemId);
            return mapper.Map<IEnumerable<CustomFieldValueViewModel>>(customFieldValues);
        }
    }
}
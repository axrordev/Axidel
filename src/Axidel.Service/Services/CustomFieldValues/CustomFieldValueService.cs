using Axidel.Data.UnitOfWorks;
using Axidel.Domain.Entities.Items;
using Axidel.Service.Exceptions;
using Axidel.Service.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Axidel.Service.Services.CustomFieldValues;

public class CustomFieldValueService(IUnitOfWork unitOfWork) : ICustomFieldValueService
{
    public async ValueTask<CustomFieldValue> CreateAsync(CustomFieldValue customFieldValue)
    {
        // Ensure the related CustomField and Item exist
        var customField = await unitOfWork.CustomFieldRepository.SelectAsync(cf => cf.Id == customFieldValue.CustomFieldId)
                            ?? throw new NotFoundException($"CustomField not found with ID={customFieldValue.CustomFieldId}");

        var item = await unitOfWork.ItemRepository.SelectAsync(i => i.Id == customFieldValue.ItemId)
                    ?? throw new NotFoundException($"Item not found with ID={customFieldValue.ItemId}");

        customFieldValue.CreatedById = HttpContextHelper.GetUserId;
        var createdCustomFieldValue = await unitOfWork.CustomFieldValueRepository.InsertAsync(customFieldValue);
        await unitOfWork.SaveAsync();
        return createdCustomFieldValue;
    }

    public async ValueTask<CustomFieldValue> UpdateAsync(long id, CustomFieldValue customFieldValue)
    {
        var existCustomFieldValue = await unitOfWork.CustomFieldValueRepository.SelectAsync(cf => cf.Id == id)
             ?? throw new NotFoundException($"CustomFieldValue not found with ID={id}");

        existCustomFieldValue.Value = customFieldValue.Value;

        var updatedCustomFieldValue = await unitOfWork.CustomFieldValueRepository.UpdateAsync(existCustomFieldValue);
        await unitOfWork.SaveAsync();
        return updatedCustomFieldValue;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existCustomFieldValue = await unitOfWork.CustomFieldValueRepository.SelectAsync(cf => cf.Id == id)
             ?? throw new NotFoundException($"CustomFieldValue not found with ID={id}");

        await unitOfWork.CustomFieldValueRepository.DeleteAsync(existCustomFieldValue);
        await unitOfWork.SaveAsync();
        return true;
    }

    public async ValueTask<CustomFieldValue> GetByIdAsync(long id)
    {
        return await unitOfWork.CustomFieldValueRepository
            .SelectAsync(cf => cf.Id == id, includes: ["CustomField", "Item"])
            ?? throw new NotFoundException($"CustomFieldValue not found with ID={id}");
    }

    public async ValueTask<IEnumerable<CustomFieldValue>> GetAllByItemIdAsync(long itemId)
    {
        return await unitOfWork.CustomFieldValueRepository
            .Select(cf => cf.ItemId == itemId, includes: ["CustomField"])
            .ToListAsync();
    }
}

using Axidel.Data.UnitOfWorks;
using Axidel.Domain.Entities.Collections;
using Axidel.Service.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Axidel.Service.Services.CustomFields;

public class CustomFieldService(IUnitOfWork unitOfWork) : ICustomFieldService
{
    public async ValueTask<CustomField> CreateAsync(CustomField customField)
    {
        customField.CreatedById = HttpContextHelper.GetUserId;
        var createdCustomField = await unitOfWork.CustomFieldRepository.InsertAsync(customField);
        await unitOfWork.SaveAsync();
        return createdCustomField;
    }

    public async ValueTask<IEnumerable<CustomField>> GetAllAsync()
    {
        return await unitOfWork.CustomFieldRepository.Select().ToListAsync();
    }
}
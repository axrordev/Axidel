using Axidel.Data.UnitOfWorks;
using Axidel.Domain.Entities.Tags;
using Axidel.Service.Exceptions;
using Axidel.Service.Helpers;
using Microsoft.EntityFrameworkCore;
namespace Axidel.Service.Services.Tags;

public class TagService(IUnitOfWork unitOfWork) : ITagService
{
    public async ValueTask<Tag> CreateAsync(Tag tag)
    {
        var existingTag = await unitOfWork.TagRepository.SelectAsync(t => t.Name.ToLower() == tag.Name.ToLower());
        if (existingTag != null)
            throw new AlreadyExistException($"Tag already exists with name = {tag.Name}");

        tag.CreatedById = HttpContextHelper.GetUserId;
        var createdTag = await unitOfWork.TagRepository.InsertAsync(tag);
        await unitOfWork.SaveAsync();
        return createdTag;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existingTag = await unitOfWork.TagRepository.SelectAsync(t => t.Id == id)
            ?? throw new NotFoundException($"Tag not found with ID = {id}");

        await unitOfWork.TagRepository.DeleteAsync(existingTag);
        await unitOfWork.SaveAsync();
        return true;
    }

    public async ValueTask<IEnumerable<Tag>> GetAllAsync()
    {
        return await unitOfWork.TagRepository.Select().ToListAsync();
    }

    public async ValueTask<Tag> GetByIdAsync(long id)
    {
        var tag = await unitOfWork.TagRepository.SelectAsync(t => t.Id == id)
            ?? throw new NotFoundException($"Tag not found with ID = {id}");

        return tag;
    }
}
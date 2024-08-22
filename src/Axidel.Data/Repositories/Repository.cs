using Axidel.Data.DbContexts;
using Axidel.Domain.Commons;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Axidel.Data.Repositories;

public class Repository<TEntity>(AppDbContext context) : IRepository<TEntity> where TEntity : Auditable
{
    public async ValueTask<TEntity> InsertAsync(TEntity entity)
    {
        return (await context.Set<TEntity>().AddAsync(entity)).Entity;
    }

    public async ValueTask<TEntity> UpdateAsync(TEntity entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        var updatedEntity = context.Set<TEntity>().Update(entity);
        return await Task.FromResult(updatedEntity.Entity);
    }

    public async ValueTask DeleteAsync(TEntity entity)
    {
        entity.IsDeleted = true;
        entity.DeletedAt = DateTime.UtcNow;
        context.Set<TEntity>().Update(entity);
        await Task.CompletedTask;
    }

    public async ValueTask<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression, string[] includes = null)
    {
        var query = context.Set<TEntity>().Where(expression);

        if (includes is not null)
            foreach (var include in includes)
                query = query.Include(include);

        return await query.FirstOrDefaultAsync();
    }

    public IQueryable<TEntity> Select(
        Expression<Func<TEntity, bool>> expression = null,
        string[] includes = null,
        bool isTracking = true)
    {
        var query = expression is not null ?
            context.Set<TEntity>().Where(expression) :
            context.Set<TEntity>();

        if (includes is not null)
            foreach (var include in includes)
                query = query.Include(include);

        if (!isTracking)
            query.AsNoTracking();

        return query;
    }
}

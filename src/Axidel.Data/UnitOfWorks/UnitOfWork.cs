using Axidel.Data.DbContexts;
using Axidel.Data.Repositories;
using Axidel.Domain.Entities.Collections;
using Axidel.Domain.Entities.Commons;
using Axidel.Domain.Entities.Items;
using Axidel.Domain.Entities.Tags;
using Axidel.Domain.Entities.Users;

namespace Axidel.Data.UnitOfWorks;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private readonly AppDbContext context = context;
    public IRepository<Comment> CommentRepository { get; } = new Repository<Comment>(context);
    public IRepository<User> UserRepository { get; } = new Repository<User>(context);
    public IRepository<Asset> AssetRepository { get; } = new Repository<Asset>(context);
    public IRepository<Collection> CollectionRepository { get; } = new Repository<Collection>(context);
    public IRepository<CustomField> CustomFieldRepository { get; } = new Repository<CustomField>(context);
    public IRepository<CustomFieldValue> CustomFieldValueRepository { get; } = new Repository<CustomFieldValue>(context);
    public IRepository<Item> ItemRepository { get; } = new Repository<Item>(context);
    public IRepository<Like> LikeRepository { get; } = new Repository<Like>(context);
    public IRepository<ItemTag> ItemTagRepository { get; } = new Repository<ItemTag>(context);
    public IRepository<Tag> TagRepository { get; } = new Repository<Tag>(context);
    public IRepository<Permission> PermissionRepository { get; } = new Repository<Permission>(context);
    public IRepository<UserRole> UserRoleRepository { get; } = new Repository<UserRole>(context);
    public IRepository<UserRolePermission> UserRolePermissionRepository { get; } = new Repository<UserRolePermission>(context);


    public async ValueTask BeginTransactionAsync()
    {
        await context.Database.BeginTransactionAsync();
    }

    public async ValueTask CommitTransactionAsync()
    {
        await context.Database.CommitTransactionAsync();
    }

    public async ValueTask Rollback()
    {
        await context.Database.RollbackTransactionAsync();
    }

    public async ValueTask<bool> SaveAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
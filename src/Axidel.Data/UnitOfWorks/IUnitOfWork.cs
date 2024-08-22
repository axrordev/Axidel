using Axidel.Data.Repositories;
using Axidel.Domain.Entities.Collections;
using Axidel.Domain.Entities.Commons;
using Axidel.Domain.Entities.Items;
using Axidel.Domain.Entities.Tags;
using Axidel.Domain.Entities.Users;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace Axidel.Data.UnitOfWorks;

public interface IUnitOfWork : IDisposable
{
    IRepository<Collection> CollectionRepository { get; }
    IRepository<CollectionImage> CollectionImageRepository { get; }
    IRepository<CustomField> CustomFieldRepository { get; }
    IRepository<Asset> AssetRepository { get; }
    IRepository<Comment> CommentRepository { get; }
    IRepository<CustomFieldValue> CustomFieldValueRepository { get; }
    IRepository<Item> ItemRepository { get; }
    IRepository<Like> LikeRepository { get; }
    IRepository<ItemTag> ItemTagRepository { get; }
    IRepository<Tag> TagRepository { get; }
    IRepository<User> UserRepository { get; }
    IRepository<UserRole> UserRoleRepository { get; }
    IRepository<UserRolePermission> UserRolePermissionRepository { get; }
    IRepository<Permission> PermissionRepository { get; }

    ValueTask<bool> SaveAsync();
    ValueTask BeginTransactionAsync();
    ValueTask CommitTransactionAsync();
    ValueTask Rollback();
}

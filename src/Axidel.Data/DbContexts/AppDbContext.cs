using Axidel.Domain.Entities.Collections;
using Axidel.Domain.Entities.Commons;
using Axidel.Domain.Entities.Items;
using Axidel.Domain.Entities.Tags;
using Axidel.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Axidel.Data.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Collection> Collections { get; set; }
    public DbSet<CollectionImage> CollectionImages { get; set; }
    public DbSet<CustomField> CustomFields { get; set; }
    public DbSet<CustomFieldValue> CustomFieldValues { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<ItemTag> ItemTags { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<UserRolePermission> UserRolePermissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Collection>()
           .HasOne(collection => collection.Image)
           .WithMany()
           .HasForeignKey(collection => collection.ImageId);

        modelBuilder.Entity<Collection>()
            .HasOne(collection => collection.User)
            .WithMany(user => user.Collections)
            .HasForeignKey(collection => collection.UserId);

        modelBuilder.Entity<Collection>()
            .HasIndex(c => new { c.Id, c.Name, c.Description, c.Category });

        modelBuilder.Entity<CollectionImage>()
            .HasOne(ci => ci.Image)
            .WithMany()
            .HasForeignKey(ci => ci.ImageId);

        modelBuilder.Entity<CollectionImage>()
            .HasOne(ci => ci.Collection)
            .WithMany()
            .HasForeignKey(ci => ci.CollectionId);

        modelBuilder.Entity<CustomField>()
            .HasOne(customField => customField.Collection)
            .WithMany(collection => collection.CustomFields)
            .HasForeignKey(customField => customField.CollectionId);


        modelBuilder.Entity<CustomFieldValue>()
           .HasOne(cfv => cfv.CustomField)
           .WithMany()
           .HasForeignKey(cfv => cfv.CustomFieldId);

        modelBuilder.Entity<CustomFieldValue>()
          .HasOne(cfv => cfv.Item)
          .WithMany()
          .HasForeignKey(cfv => cfv.ItemId);


        modelBuilder.Entity<Comment>()
            .HasOne(comment => comment.Item)
            .WithMany(item => item.Comments)
            .HasForeignKey(Comment => Comment.ItemId);

        modelBuilder.Entity<Comment>()
           .HasOne(comment => comment.User)
           .WithMany()
           .HasForeignKey(Comment => Comment.UserId);

        modelBuilder.Entity<Comment>()
            .HasIndex(c => new { c.Id, c.Text });


        modelBuilder.Entity<Item>()
            .HasOne(item => item.Collection)
            .WithMany(collection => collection.Items)
            .HasForeignKey(item => item.CollectionId);

        modelBuilder.Entity<Item>()
           .HasOne(item => item.User)
           .WithMany()
           .HasForeignKey(item => item.UserId);

        modelBuilder.Entity<Item>()
            .HasIndex(i => new { i.Id, i.Name});


        modelBuilder.Entity<Like>()
            .HasOne(like => like.Item)
            .WithMany(item => item.Likes)
            .HasForeignKey(like => like.ItemId);

        modelBuilder.Entity<Like>()
            .HasOne(like => like.User)
            .WithMany()
            .HasForeignKey(like => like.UserId);


        modelBuilder.Entity<ItemTag>()
            .HasOne(itemTag => itemTag.Tag)
            .WithMany(tag => tag.ItemTags)
            .HasForeignKey(itemTag => itemTag.TagId);

        modelBuilder.Entity<ItemTag>()
           .HasOne(itemTag => itemTag.Item)
           .WithMany()
           .HasForeignKey(itemTag => itemTag.ItemId);

        
        modelBuilder.Entity<User>()
            .HasOne(user => user.Role)
            .WithMany()
            .HasForeignKey(user => user.RoleId);

        modelBuilder.Entity<UserRolePermission>()
            .HasOne(userRolePermission => userRolePermission.UserRole)
            .WithMany()
            .HasForeignKey(userRolePermission => userRolePermission.UserRoleId);

        modelBuilder.Entity<UserRolePermission>()
           .HasOne(permission => permission.UserRole)
           .WithMany()
           .HasForeignKey(permission => permission.UserRoleId);

        modelBuilder.Entity<Collection>().HasQueryFilter(entity => !entity.IsDeleted);
        modelBuilder.Entity<CollectionImage>().HasQueryFilter(entity => !entity.IsDeleted);
        modelBuilder.Entity<CustomField>().HasQueryFilter(entity => !entity.IsDeleted);
        modelBuilder.Entity<CustomFieldValue>().HasQueryFilter(entity => !entity.IsDeleted);
        modelBuilder.Entity<Asset>().HasQueryFilter(entity => !entity.IsDeleted);
        modelBuilder.Entity<Comment>().HasQueryFilter(entity => !entity.IsDeleted);
        modelBuilder.Entity<Item>().HasQueryFilter(entity => !entity.IsDeleted);
        modelBuilder.Entity<Like>().HasQueryFilter(entity => !entity.IsDeleted);
        modelBuilder.Entity<ItemTag>().HasQueryFilter(entity => !entity.IsDeleted);
        modelBuilder.Entity<Tag>().HasQueryFilter(entity => !entity.IsDeleted);
        modelBuilder.Entity<Tag>().HasQueryFilter(entity => !entity.IsDeleted);
        modelBuilder.Entity<Permission>().HasQueryFilter(entity => !entity.IsDeleted);
        modelBuilder.Entity<User>().HasQueryFilter(entity => !entity.IsDeleted);
        modelBuilder.Entity<UserRole>().HasQueryFilter(entity => !entity.IsDeleted);
        modelBuilder.Entity<UserRolePermission>().HasQueryFilter(entity => !entity.IsDeleted);
    }
}

using AutoMapper;
using Axidel.Domain.Entities.Collections;
using Axidel.Domain.Entities.Commons;
using Axidel.Domain.Entities.Items;
using Axidel.Domain.Entities.Tags;
using Axidel.Domain.Entities.Users;
using Axidel.WebApi.Models.Assets;
using Axidel.WebApi.Models.CollectionImages;
using Axidel.WebApi.Models.Collections;
using Axidel.WebApi.Models.Comments;
using Axidel.WebApi.Models.CustomFields;
using Axidel.WebApi.Models.CustomFieldValues;
using Axidel.WebApi.Models.Items;
using Axidel.WebApi.Models.Itemtags;
using Axidel.WebApi.Models.Likes;
using Axidel.WebApi.Models.Permissions;
using Axidel.WebApi.Models.Tags;
using Axidel.WebApi.Models.UserRolePermissions;
using Axidel.WebApi.Models.UserRoles;
using Axidel.WebApi.Models.Users;

namespace Axidel.WebApi.MapperConfigurations;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User and related entities
        CreateMap<UserRegisterModel, User>();
        CreateMap<UserUpdateModel, User>();
        CreateMap<User, UserViewModel>();
        CreateMap<User, LoginViewModel>();

        CreateMap<UserRole, UserRoleCreateModel>().ReverseMap();
        CreateMap<UserRole, UserRoleUpdateModel>().ReverseMap();
        CreateMap<UserRole, UserRoleViewModel>().ReverseMap();

        CreateMap<PermissionCreateModel, Permission>();
        CreateMap<PermissionUpdateModel, Permission>();
        CreateMap<Permission, PermissionViewModel>();

        CreateMap<UserRolePermissionCreateModel, UserRolePermission>();
        CreateMap<UserRolePermissionUpdateModel, UserRolePermission>();
        CreateMap<UserRolePermission, UserRolePermissionViewModel>();

        // Collection and related entities
        CreateMap<CollectionCreateModel, Collection>();
        CreateMap<CollectionUpdateModel, Collection>();
        CreateMap<Collection, CollectionViewModel>();

        // CollectionImage and related entities
        CreateMap<CollectionImageCreateModel, Collection>();

        // Comment and related entities
        CreateMap<CommentCreateModel, Comment>();
        CreateMap<CommentUpdateModel, Comment>();
        CreateMap<Comment, CommentViewModel>();

        // CustomField and related entities
        CreateMap<CustomFieldCreateModel, CustomField>();
        CreateMap<CustomField, CustomFieldViewModel>();

        // CustomFieldValue and related entities
        CreateMap<CustomFieldValueCreateModel, CustomFieldValue>();
        CreateMap<CustomFieldValueUpdateModel, CustomFieldValue>();
        CreateMap<CustomFieldValue, CustomFieldValueViewModel>();

        // Item and related entities
        CreateMap<ItemCreateModel, Item>();
        CreateMap<ItemUpdateModel, Item>();
        CreateMap<Item, ItemViewModel>();

        // Like and related entities
        CreateMap<LikeCreateModel, Like>();
        CreateMap<Like, LikeViewModel>();

        // ItemTag and related entities
        CreateMap<ItemTagCreateModel, ItemTag>();
        CreateMap<ItemTag, ItemTagViewModel>();

        // Tag and related entities
        CreateMap<TagCreateModel, Tag>();
        CreateMap<TagUpdateModel, Tag>();
        CreateMap<Tag, TagViewModel>();

        CreateMap<Asset, AssetViewModel>().ReverseMap();
    }
}
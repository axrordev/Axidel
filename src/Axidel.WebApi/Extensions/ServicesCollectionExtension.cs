using Axidel.Data.UnitOfWorks;
using Axidel.Service.Helpers;
using Axidel.Service.Services.Accounts;
using Axidel.Service.Services.Assets;
using Axidel.Service.Services.Collections;
using Axidel.Service.Services.Comments;
using Axidel.Service.Services.CustomFields;
using Axidel.Service.Services.CustomFieldValues;
using Axidel.Service.Services.Items;
using Axidel.Service.Services.ItemTags;
using Axidel.Service.Services.Likes;
using Axidel.Service.Services.Permissions;
using Axidel.Service.Services.SearchServices;
using Axidel.Service.Services.Tags;
using Axidel.Service.Services.UserRolePermissions;
using Axidel.Service.Services.UserRoles;
using Axidel.Service.Services.Users;
using Axidel.WebApi.ApiServices.Accounts;
using Axidel.WebApi.ApiServices.Assets;
using Axidel.WebApi.ApiServices.Collections;
using Axidel.WebApi.ApiServices.Comments;
using Axidel.WebApi.ApiServices.CustomFields;
using Axidel.WebApi.ApiServices.CustomFieldValues;
using Axidel.WebApi.ApiServices.Items;
using Axidel.WebApi.ApiServices.ItemTags;
using Axidel.WebApi.ApiServices.Likes;
using Axidel.WebApi.ApiServices.Permissions;
using Axidel.WebApi.ApiServices.Tags;
using Axidel.WebApi.ApiServices.UserRolePermissions;
using Axidel.WebApi.ApiServices.UserRoles;
using Axidel.WebApi.ApiServices.Users;
using Axidel.WebApi.Helpers;
using Axidel.WebApi.Middlewares;
using Axidel.WebApi.Validators.Accounts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Axidel.WebApi.Extensions;

public static class ServicesCollectionExtension
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IAssetService, AssetService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICollectionService, CollectionService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<ICustomFieldService, CustomFieldService>();
        services.AddScoped<ICustomFieldValueService, CustomFieldValueService>();
        services.AddScoped<IItemService, ItemService>();
        services.AddScoped<IItemTagService, ItemTagService>();
        services.AddScoped<ILikeService, LikeService>();
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<IUserRoleService, UserRoleService>();
        services.AddScoped<IUserRolePermissionService, UserRolePermissionService>();
        services.AddScoped<ISearchService, SearchService>();

    }

    public static void AddApiServices(this IServiceCollection services)
    {
        services.AddScoped<IAssetApiService, AssetApiService>();
        services.AddScoped<IAccountApiService, AccountApiService>();
        services.AddScoped<IUserApiService, UserApiService>();
        services.AddScoped<ICollectionApiService, CollectionApiService>();
        services.AddScoped<ICommentApiService, CommentApiService>();
        services.AddScoped<ICustomFieldApiService, CustomFieldApiService>();
        services.AddScoped<ICustomFieldValueApiService, CustomFieldValueApiService>();
        services.AddScoped<IItemApiService, ItemApiService>();
        services.AddScoped<IItemTagApiService, ItemTagApiService>();
        services.AddScoped<ILikeApiService, LikeApiService>();
        services.AddScoped<ITagApiService, TagApiService>();
        services.AddScoped<IPermissionApiService, PermissionApiService>();
        services.AddScoped<IUserRoleApiService, UserRoleApiService>();
        services.AddScoped<IUserRolePermissionApiService, UserRolePermissionApiService>();
    }

    public static void AddExceptions(this IServiceCollection services)
    {
        FilePathHelper.WwwrootPath = Path.GetFullPath("wwwroot");

        services.AddExceptionHandler<NotFoundExceptionMiddleware>();
        services.AddExceptionHandler<ForbiddenExceptionMiddleware>();
        services.AddExceptionHandler<AlreadyExistExceptionMiddleware>();
        services.AddExceptionHandler<InternalServerExceptionMiddleware>();
        services.AddExceptionHandler<ArgumentIsNotValidExceptionMiddleware>();
    }

    public static void AddInjectHelper(this WebApplication app)
    {
        var scope = app.Services.CreateScope();
        ServiceHelper.UserRoleService = scope.ServiceProvider.GetRequiredService<IUserRoleService>();
        ServiceHelper.UserRolePermissionService = scope.ServiceProvider.GetRequiredService<IUserRolePermissionService>();
    }

    public static void AddPathInitializer(this WebApplication app)
    {
        HttpContextHelper.ContextAccessor = app.Services.GetRequiredService<IHttpContextAccessor>();
        EnvironmentHelper.JwtKey = app.Configuration.GetSection("Jwt:Key").Value;
        EnvironmentHelper.TokenLifeTimeInHour = app.Configuration.GetSection("Jwt:LifeTime").Value;
        EnvironmentHelper.SmtpHost = app.Configuration.GetSection("Email:SmtpHost").Value;
        EnvironmentHelper.SmtpPort = app.Configuration.GetSection("Email:SmtpPort").Value;
        EnvironmentHelper.EmailAddress = app.Configuration.GetSection("Email:EmailAddress").Value;
        EnvironmentHelper.EmailPassword = app.Configuration.GetSection("Email:EmailPassword").Value;
        EnvironmentHelper.SuperAdminLogin = app.Configuration.GetSection("SuperAdmin:Login").Value;
        EnvironmentHelper.SuperAdminPassword = app.Configuration.GetSection("SuperAdmin:Password").Value;
    }

    public static void AddValidators(this IServiceCollection services)
    {
        services.AddTransient<AccountLoginValidator>();
        services.AddTransient<AccountCreateValidator>();
        services.AddTransient<AccountVerifyValidator>();
        services.AddTransient<AccountSendCodeValidator>();
        services.AddTransient<AccountRegisterModelValidator>();
        services.AddTransient<AccountResetPasswordValidator>();
    }

    public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(setup =>
        {
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

            setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });
        });
    }
}

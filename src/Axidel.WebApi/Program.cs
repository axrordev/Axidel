using Axidel.Data.DbContexts;
using Axidel.WebApi.Extensions;
using Axidel.WebApi.MapperConfigurations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options
    => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddMemoryCache();

builder.Services.AddApiServices();

builder.Services.AddServices();

builder.Services.AddValidators();

builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureSwagger();

builder.Services.AddExceptions();

builder.Services.AddJwt(builder.Configuration);

builder.Services.AddProblemDetails();

builder.Services.AddHttpContextAccessor();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
            .WithOrigins("https://axidel.netlify.app") // Add your frontend URL here
            .AllowAnyHeader()
            .AllowAnyMethod());
});


var app = builder.Build();

app.UseCors("AllowSpecificOrigin");

app.AddInjectHelper();

app.AddPathInitializer();

app.UseSwagger();

app.UseSwaggerUI();

app.UseExceptionHandler();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
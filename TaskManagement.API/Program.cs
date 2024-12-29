using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TaskManagement.API.Helper;
using TaskManagement.Domain.Repository.Contract;
using TaskManagement.Repository;
using TaskManagement.Repository.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();



builder.Services.AddDbContext<TaskManagementDbContext>(options=>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});



builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<TaskManagementDbContext>()
                .AddDefaultTokenProviders();


builder.Services.AddScoped<UserManager<IdentityUser>>();

builder.Services.AddScoped(typeof(IAuthToken), typeof(AuthToken));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped(typeof(ITaskRepository), typeof(TaskRepository));

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:ValidIssure"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:AuthKey"] ?? string.Empty))
    };

});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var _DbContext = services.GetRequiredService<TaskManagementDbContext>();
var _identityRoleManger = services.GetRequiredService<RoleManager<IdentityRole>>();
var LoggerFactory = services.GetRequiredService<ILoggerFactory>();

try
{
    await RoleIdentityDbContext.SeedRoleAsync(_identityRoleManger);
    await _DbContext.Database.MigrateAsync(); //Update DataBase


}
catch (Exception ex)
{
    var logger = LoggerFactory.CreateLogger<Program>();
    logger.LogError(ex, "an error occured during migration");
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

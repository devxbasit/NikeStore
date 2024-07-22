using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NikeStore.Services.AuthApi.Data;
using NikeStore.Services.AuthApi.Models;
using NikeStore.Services.AuthApi.RabbitMqProducer;
using NikeStore.Services.AuthApi.Services;
using NikeStore.Services.AuthApi.Services.IService;
using NikeStore.Services.EmailApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            new string[] { }
        }
    });
});
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options => { options.UseSqlServer(builder.Configuration.GetConnectionString("AuthApiDbConnectionString")); });
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.Password.RequiredLength = 3;
        options.Password.RequiredUniqueChars = 0;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireDigit = false;
        options.Password.RequireNonAlphanumeric = false;
    }).AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IJwtTokenGeneratorService, JwtTokenGeneratorService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRabbitMqAuthMessageProducer, RabbitMqAuthMessageProducer>();


builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));
builder.Services.Configure<RabbitMQConnectionOptions>(builder.Configuration.GetSection("RabbitMQSetting:RabbitMQConnectionOptions"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}
app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapControllers();
ApplyPendingMigrations();
app.Run();

void ApplyPendingMigrations()
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        if (db.Database.GetPendingMigrations().Count() > 0)
        {
            Console.WriteLine("--> Applying pending migrations...");
            db.Database.Migrate();
        }
    }
}

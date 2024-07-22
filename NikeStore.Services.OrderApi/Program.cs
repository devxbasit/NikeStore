using Microsoft.AspNetCore.Authentication.JwtBearer;
using NikeStore.Services.OrderAPI.RabbmitMQSender;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NikeStore.Services.CouponApi.Data;
using NikeStore.Services.OrderApi.Extensions;
using NikeStore.Services.OrderApi.Models;
using NikeStore.Services.OrderApi.Services;
using NikeStore.Services.OrderApi.Services.IService;
using NikeStore.Services.OrderApi.Utility;
using NikeStore.Services.ProductApi.Utility;

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
builder.Services.AddDbContext<AppDbContext>(option => { option.UseSqlServer(builder.Configuration.GetConnectionString("OrderApiDbConnectionString")); });

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));
builder.Services.Configure<RabbitMQConnectionOptions>(builder.Configuration.GetSection("RabbitMQSetting:RabbitMQConnectionOptions"));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IRabbitMqOrderMessageProducer, RabbitMqOrderMessageProducer>();

builder.Services.AddControllers();
builder.AddAppAuthentication();

builder.Services.AddAuthorization();


builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<BackendApiAuthenticationHttpClientHandler>();
builder.Services.AddHttpClient("ShoppingCartClient",
        u => { u.BaseAddress = new Uri(builder.Configuration["ServiceUrls:ShoppingCartApi"]); })
    .AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>();



var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{

}

app.UseSwagger();
app.UseSwaggerUI();
Stripe.StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

ApplyPendingMigrations();
app.Run();


void ApplyPendingMigrations()
{
    using (var scope = app.Services.CreateScope())
    {
        var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (_db.Database.GetPendingMigrations().Count() > 0)
        {
            _db.Database.Migrate();
        }
    }
}

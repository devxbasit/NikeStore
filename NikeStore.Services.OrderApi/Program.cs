using NikeStore.Services.OrderAPI.RabbmitMQSender;
using Microsoft.EntityFrameworkCore;
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
builder.Services.AddSwaggerGen();

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
    app.UseSwagger();
    app.UseSwaggerUI();
}

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

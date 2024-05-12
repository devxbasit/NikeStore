using Microsoft.EntityFrameworkCore;
using NikeStore.Services.ShoppingCartApi.Data;
using NikeStore.Services.ShoppingCartApi.Extensions;
using NikeStore.Services.ShoppingCartApi.Models;
using NikeStore.Services.ShoppingCartApi.RabbitMqProducer;
using NikeStore.Services.ShoppingCartApi.Service;
using NikeStore.Services.ShoppingCartApi.Service.IService;
using NikeStore.Services.ShoppingCartApi.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("ShoppingCartApiDbConnectionString"));
});

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICouponService, CouponService>();
builder.Services.AddScoped<IRabbitMqCartMessageProducer, RabbitMqCartMessageProducer>();
builder.Services.AddScoped<BackendApiAuthenticationHttpClientHandler>();
builder.Services.Configure<RabbitMQConnectionOptions>(builder.Configuration.GetSection("RabbitMQSetting:RabbitMQConnectionOptions"));

builder.Services.AddHttpClient("Product",
        u => { u.BaseAddress = new Uri(builder.Configuration["ServiceUrls:ProductAPI"]); })
    .AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>();

builder.Services
    .AddHttpClient("Coupon", u => { u.BaseAddress = new Uri(builder.Configuration["ServiceUrls:CouponAPI"]); })
    .AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.AddAppAuthentication();
builder.Services.AddAuthorization();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();


ApplyPendingMigrations();

app.Run();


void ApplyPendingMigrations()
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (db.Database.GetPendingMigrations().Count() > 0)
        {
            db.Database.Migrate();
        }
    }
}
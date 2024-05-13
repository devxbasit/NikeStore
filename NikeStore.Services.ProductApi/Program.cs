using Microsoft.EntityFrameworkCore;
using NikeStore.Services.ProductApi.Data;
using NikeStore.Services.ProductApi.Extensions;
using NikeStore.Services.ProductApi.Services;
using NikeStore.Services.ProductApi.Services.IService;
using NikeStore.Services.ProductApi.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("ProductApiDbConnection"));
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


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

//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
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
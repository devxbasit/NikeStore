using Microsoft.EntityFrameworkCore;
using NikeStore.Services.CouponApi.Data;
using NikeStore.Services.OrderApi.Models;
using NikeStore.Services.RewardsApi.Extensions;
using NikeStore.Services.RewardsApi.RabbitMqConsumer;
using NikeStore.Services.RewardsApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));
builder.Services.Configure<RabbitMQConnectionOptions>(builder.Configuration.GetSection("RabbitMQSetting:RabbitMQConnectionOptions"));


var dbConnectionString = builder.Configuration.GetConnectionString("RewardsApiDbConnectionString");
builder.Services.AddDbContext<AppDbContext>(option => { option.UseSqlServer(dbConnectionString); });

var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();
optionBuilder.UseSqlServer(dbConnectionString);
builder.Services.AddSingleton<IRewardService>(new RewardService(optionBuilder.Options));


builder.Services.AddHostedService<RabbitMqRewardMessageConsumer>();


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
//app.UseHsts();

app.UseAuthorization();
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

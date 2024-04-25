using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NikeStore.Services.EmailApi.Data;
using NikeStore.Services.EmailApi.Extensions;
using NikeStore.Services.EmailApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// adding all the 3 consumers here
builder.Services.AddRabbitMqConsumers();


string connectionString = builder.Configuration.GetConnectionString("EmailApiConnectionString");

builder.Services.AddDbContext<AppDbContext>(options => { options.UseSqlServer(connectionString); });

var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
optionsBuilder.UseSqlServer(connectionString);
builder.Services.AddSingleton(new EmailService(optionsBuilder.Options));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

ApplyPendingMigrations();
app.Run();


void ApplyPendingMigrations()
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (db.Database.GetPendingMigrations().Count() > 0)
    {
        db.Database.Migrate();
    }
}
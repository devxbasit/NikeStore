using Hangfire;
using Hangfire.Dashboard;
using HangfireBasicAuthenticationFilter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NikeStore.Services.EmailApi.Data;
using NikeStore.Services.EmailApi.Extensions;
using NikeStore.Services.EmailApi.Models;
using NikeStore.Services.EmailApi.Services;
using NikeStore.Services.EmailApi.Services.IService;

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

string connectionString = builder.Configuration.GetConnectionString("EmailApiDbConnectionString");


builder.Services.AddDbContext<AppDbContext>(options => { options.UseSqlServer(connectionString); });

var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
optionsBuilder.UseSqlServer(connectionString);
builder.Services.AddSingleton<IDbLogService>(new DbLogService(optionsBuilder.Options));


builder.Services.Configure<RabbitMQConnectionOptions>(builder.Configuration.GetSection("RabbitMQSetting:RabbitMQConnectionOptions"));
builder.Services.Configure<MailKitConnectionOptions>(builder.Configuration.GetSection("MailKitConnectionOptions"));
builder.Services.AddSingleton<ISmtpMailService, SmtpMailService>();

// adding all the 3 consumers as hosted service
builder.Services.AddRabbitMqConsumersAsHostedService();


// Add Hangfire services.
builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("EmailApiHangfireDbConnectionString")));

// Add Hangfire Server
builder.Services.AddHangfireServer();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

ApplyPendingMigrations();

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new [] { new MyAuthorizationFilter() }
});


// app.UseHangfireDashboard("/hangfire", new DashboardOptions()
// {
//     Authorization = new[]
//     {
//         new HangfireCustomBasicAuthenticationFilter
//         {
//             User = builder.Configuration.GetValue<string>("HangFireOptions:BasicAuthenticationFilterValues:User:UserName"),
//             Pass = builder.Configuration.GetValue<string>("HangFireOptions:BasicAuthenticationFilterValues:User:Password"),
//         }
//     }
// });



app.UseHangfireServer();

app.DoAction();
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





public class MyAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        return true;
    }
}

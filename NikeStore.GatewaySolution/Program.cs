
using Microsoft.Extensions.Hosting.Internal;
using NikeStore.GatewaySolution.Extensions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddAppAuthetication();

IServiceProvider serviceProvider = builder.Services.BuildServiceProvider();
IHostingEnvironment env = serviceProvider.GetService<IHostingEnvironment>();


builder.Configuration
    .AddJsonFile("./ocelot.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"./ocelot.{env.EnvironmentName}.json", optional: false, reloadOnChange: true);


builder.Services.AddOcelot(builder.Configuration);



var app = builder.Build();

// Configure the HTTP request pipeline.

await app.UseOcelot();

app.UseAuthentication();
app.UseAuthorization();

app.Run();

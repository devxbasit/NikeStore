
using NikeStore.GatewaySolution.Extensions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddAppAuthetication();
builder.Configuration.AddJsonFile("./ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);



var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseOcelot().GetAwaiter().GetResult();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
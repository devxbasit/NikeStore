using Microsoft.AspNetCore.Authentication.Cookies;
using NikeStore.Web.Service;
using NikeStore.Web.Service.IService;
using NikeStore.Web.Utility;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

builder.Services.AddScoped<ITokenProviderService, TokenProviderService>();
builder.Services.AddScoped<IBaseService, BaseService>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICouponService, CouponService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        // Todo - Fix Later
        options.ExpireTimeSpan = TimeSpan.FromDays(365);
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/AccessDenied";
    });


SD.AuthApiBase = builder.Configuration["ServiceUrls:AuthApi"];
SD.CouponApiBase = builder.Configuration["ServiceUrls:CouponApi"];
SD.ProducApiBase = builder.Configuration["ServiceUrls:ProductApi"];
SD.ShoppingCartApiBase = builder.Configuration["ServiceUrls:ShoppingCartApi"];
SD.OrderApiBase = builder.Configuration["ServiceUrls:OrderApi"];


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

using EcommerceShop.WebApp.Interfaces;
using EcommerceShop.WebApp.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<ICategoryApiService, CategoryApiService>();
builder.Services.AddTransient<IProductApiService, ProductApiService>();
builder.Services.AddTransient<ILanguageApiService, LanguageApiService>();
builder.Services.AddTransient<ICartService, CartService>();
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient("myclient", client =>{
    client.BaseAddress = new Uri("https://localhost:7196");
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
builder.Services.AddSession(options =>{
    options.Cookie.Name = "MyProjectSessionCookie";
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

//app.UseCookiePolicy();

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

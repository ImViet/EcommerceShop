using EcommerceShop.WebApp.Interfaces;
using EcommerceShop.WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<ICategoryApiService, CategoryApiService>();
builder.Services.AddTransient<IProductApiService, ProductApiService>();
builder.Services.AddTransient<ILanguageApiService, LanguageApiService>();

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient("myclient", client =>{
    client.BaseAddress = new Uri("https://localhost:7196");
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
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
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

using System.Reflection;
using EcommerceShop.AdminApp.Interfaces;
using EcommerceShop.AdminApp.Services;
using EcommerceShop.Contracts.Dtos.AuthDtos;
using EcommerceShop.Contracts.Validators.AuthDtos;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

//
builder.Services.AddTransient<IAuthApiService, AuthApiService>();
builder.Services.AddTransient<IUserApiService, UserApiService>();
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddControllersWithViews().AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<UserRegisterDtoValidator>();
    });;
builder.Services.AddHttpClient("myclient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7196");
});
builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => {
    options.LoginPath = "/Auth/Login";
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
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

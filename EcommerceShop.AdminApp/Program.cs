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
builder.Services.AddTransient<IRoleApiService, RoleApiService>();
builder.Services.AddTransient<ILanguageApiService, LanguageApiService>();
builder.Services.AddTransient<IProductApiService, ProductApiService>();
builder.Services.AddTransient<ICategoryApiService, CategoryApiService>();
builder.Services.AddTransient<IOrderApiService, OrderApiService>();
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddControllersWithViews().AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<UserRegisterDtoValidator>();
    });
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
builder.Services.AddAuthorization(options => {
    options.AddPolicy("RoleAdmin", policy => policy.RequireClaim("Role", "admin"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.Use(async(context, next) => {
        await next();
        if(context.Response.StatusCode == 404)
        {
            context.Request.Path = "/Error/NotFound";
            await next();
        }
    });
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

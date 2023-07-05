using EcommerceShop.BackendAPI.Extensions;
using EcommerceShop.Business;
using EcommerceShop.Contracts.Dtos.AuthDtos;
using EcommerceShop.Contracts.Validators.AuthDtos;
using EcommerceShop.Data;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthenticateRegister();
builder.Services.AddDataLayer(builder.Configuration);
builder.Services.AddBusinessLayer();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Swagger bearer token
builder.Services.AddSwaggerRegister();
builder.Services.AddAuthorization(options =>{
    options.AddPolicy("RoleAdmin", policy => policy.RequireClaim("Role", "admin"));
});
builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<UserRegisterDtoValidator>();
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EcommerceShop v1"));
}

app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

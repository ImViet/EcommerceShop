using EcommerceShop.Business;
using EcommerceShop.Business.Interfaces;
using EcommerceShop.Business.Services;
using EcommerceShop.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthenticateRegister();
builder.Services.AddControllers();
builder.Services.AddDataLayer(builder.Configuration);
builder.Services.AddBusinessLayer();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

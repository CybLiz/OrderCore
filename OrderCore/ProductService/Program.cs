using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.DTO;
using ProductService.Models;
using ProductService.Repository;
using ProductService.Services;


var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Repository et service
builder.Services.AddScoped<IRepository<Product>, ProductRepository>();
builder.Services.AddScoped<IService<ProductReceiveDto, ProductSendDto>, ProductServiceImpl>();



// DbContext
string connectionString = builder.Configuration.GetConnectionString("default");
builder.Services.AddDbContext<AppDbContext>(option =>
    option.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


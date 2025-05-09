using OrderManagementSystem.Data.
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// AutoMapper servisini ekle
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

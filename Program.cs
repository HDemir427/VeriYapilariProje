using OrderManagementSystem.Data;
using AutoMapper;
using OrderManagementSystem.Mapping;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

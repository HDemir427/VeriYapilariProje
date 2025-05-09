using System.Reflection;
using OrderManagementSystem.Mapping;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);



var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

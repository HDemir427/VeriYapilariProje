using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data.Context;
using OrderManagementSystem.Data.Entity;
using OrderManagementSystem.Data.Seeder;
using OrderManagementSystem.Mapping;
using OrderManagementSystem.Services;
using OrderManagementSystem.Utilities;
using OrderManagementSystem.Data.Context;  

var builder = WebApplication.CreateBuilder(args);

// 🔹 Veritabanı bağlantısı
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});



// 🔹 AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddScoped(typeof(CustomHashTable<,>));
builder.Services.AddScoped(typeof(CustomAvlTree<>));
builder.Services.AddScoped(typeof(CustomQueue<>));
builder.Services.AddScoped(typeof(CustomLinkedList<>));


builder.Services.AddScoped<CustomHashTable<int, Product>>();
builder.Services.AddScoped<CustomAvlTree<Category>>();
builder.Services.AddScoped<CustomQueue<Order>>();
builder.Services.AddScoped<CustomLinkedList<UserHistory>>();


builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserHistoryService, UserHistoryService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IAuthService, AuthService>(); 
builder.Services.AddScoped<ICategoryService, CategoryService>(); 
builder.Services.AddScoped<IOrderService, OrderService>();



// 🔹 Controller desteği
builder.Services.AddControllers();

// 🔹 CORS (Frontend erişimi için)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();
app.UseCors("AllowAll");

DataSeeder.SeedDatabase(app.Services);
// 🔹 Middleware sırası
app.UseCors("AllowAll");
app.UseRouting();
app.UseAuthorization(); // (JWT vs. varsa burada ayarlanır)

app.MapControllers();

app.Run();

using OrderManagementSystem.Data.Entity;
using OrderManagementSystem.Data.Context;
using Microsoft.Extensions.DependencyInjection;

namespace OrderManagementSystem.Data.Seeder
{
    public static class DataSeeder
    {
        public static void SeedDatabase(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<OrderManagementSystem.Data.Context.Context>();


            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category { Name = "Fotoğraf Makinesi" },
                    new Category { Name = "Telefon & Tablet" },
                    new Category { Name = "Bilgisayar & Laptop" },
                    new Category { Name = "Aksesuar" },
                    new Category { Name = "Tablet" },
                    new Category { Name = "Monitör" },
                    new Category { Name = "Akıllı Saat & Bileklik" },
                    new Category { Name = "Oyun Konsolu" }
                };

                context.Categories.AddRange(categories);
                context.SaveChanges(); 
            }



            if (!context.Products.Any())
                {
                    var products = new List<Product>
                {
                    new Product { Name = "iPhone 15 128 GB", CategoryId = 1, Description = "deneme", Price = 54999m, StockQuantity = 25, Image = "https://cdn.akakce.com/z/apple/iphone-15.jpg" },
                    new Product { Name = "TCL 32S5400AF Full HD 32'' TV", CategoryId = 1, Description = "deneme", Price = 6849m, StockQuantity = 30, Image = "https://cdn.akakce.com/z/tcl/32s5400af-full-hd-32-82-ekran-android-smart-led-tv.jpg" },
                    new Product { Name = "Lenovo V14 G2 Intel Core i5", CategoryId = 2, Description = "deneme", Price = 14999m, StockQuantity = 20, Image = "https://cdn.akakce.com/z/lenovo/v14-g2-itl-82kd000ntx-intel-core-i5-1135g7-8gb-256gb-ssd-freedos-14.jpg" },
                    new Product { Name = "HP 240 G8 Core i3 4GB 256GB SSD", CategoryId = 2, Description = "deneme", Price = 10999m, StockQuantity = 18, Image = "https://cdn.akakce.com/z/hp/240-g8-intel-core-i3-1115g4-4gb-256gb-ssd-14-freedos.jpg" },
                    new Product { Name = "Samsung Galaxy A55 5G 128 GB", CategoryId = 1, Description = "deneme", Price = 21499m, StockQuantity = 40, Image = "https://cdn.akakce.com/z/samsung/galaxy-a55-5g-128-gb.jpg" },
                    new Product { Name = "Xbox Series S 512 GB SSD", CategoryId = 8, Description = "deneme", Price = 8499m, StockQuantity = 22, Image = "https://cdn.akakce.com/z/microsoft/series-s-512-gb-ssd-oyun-konsolu.jpg" },
                    new Product { Name = "Sony WH-1000XM5 Kulak Üstü", CategoryId = 3, Description = "deneme", Price = 7999m, StockQuantity = 15, Image = "https://cdn.akakce.com/z/sony/wh-1000xm5-kablosuz-kulak-ustu-kulaklik.jpg" },
                    new Product { Name = "Reeder P13 Blue Max Kids Tablet", CategoryId = 4, Description = "deneme", Price = 4299m, StockQuantity = 28, Image = "https://cdn.akakce.com/z/reeder/p13-blue-max-kids-13-3-4-gb-64-gb-tablet.jpg" },
                    new Product { Name = "HP M27f FHD 27\" Monitör", CategoryId = 5, Description = "deneme", Price = 3399m, StockQuantity = 12, Image = "https://cdn.akakce.com/z/hp/m27f-fhd-27-75-hz-monitor.jpg" },
                    new Product { Name = "Logitech G203 Gaming Mouse", CategoryId = 3, Description = "deneme", Price = 699m, StockQuantity = 50, Image = "https://cdn.akakce.com/z/logitech/g203-lightsync-rgb-gaming-mouse.jpg" },
                    new Product { Name = "Asus TUF Gaming M5 RGB Mouse", CategoryId = 3, Description = "deneme", Price = 459m, StockQuantity = 45, Image = "https://cdn.akakce.com/z/asus/tuf-gaming-m5-rgb-kablolu-gaming-mouse.jpg" },
                    new Product { Name = "Samsung 980 PRO 1 TB NVMe SSD", CategoryId = 2, Description = "deneme", Price = 2499m, StockQuantity = 35, Image = "https://cdn.akakce.com/z/samsung/980-pro-1-tb-m-2-nvme-ssd.jpg" },
                    new Product { Name = "Kingston A2000 500 GB SSD", CategoryId = 2, Description = "deneme", Price = 899m, StockQuantity = 40, Image = "https://cdn.akakce.com/z/kingston/a2000-500-gb-m-2-nvme-ssd.jpg" },
                    new Product { Name = "Xiaomi Mi Smart Band 7", CategoryId = 6, Description = "deneme", Price = 549m, StockQuantity = 60, Image = "https://cdn.akakce.com/z/xiaomi/mi-smart-band-7-akilli-bileklik.jpg" },
                    new Product { Name = "PS5 Slim Digital 1 TB", CategoryId = 8, Description = "deneme", Price = 19999m, StockQuantity = 15, Image = "https://cdn.akakce.com/z/sony/slim-digital-edition-1-tb-ssd-oyun-konsolu.jpg" },
                    new Product { Name = "Dell Inspiron 15 Ryzen 5", CategoryId = 2, Description = "deneme", Price = 12499m, StockQuantity = 20, Image = "https://cdn.akakce.com/z/dell/inspiron-15-3525-ryzen-5-5500u-8-gb-256-gb-ssd-15-6.jpg" },
                    new Product { Name = "Garmin Forerunner 55 GPS Saat", CategoryId = 6, Description = "deneme", Price = 2799m, StockQuantity = 22, Image = "https://cdn.akakce.com/z/garmin/forerunner-55-gps-kosu-saati.jpg" },
                    new Product { Name = "Logitech MK270 Klavye Mouse Set", CategoryId = 3, Description = "deneme", Price = 499m, StockQuantity = 55, Image = "https://cdn.akakce.com/z/logitech/mk270-kablosuz-klavye-mouse-set.jpg" },
                    new Product { Name = "Canon EOS M50 Mark II Kamera", CategoryId = 7, Description = "deneme", Price = 12999m, StockQuantity = 10, Image = "https://cdn.akakce.com/z/canon/eos-m50-mark-ii-aynasiz-fotograf-makinasi.jpg" },
                    new Product { Name = "Xiaomi Redmi Note 12 Pro", CategoryId = 1, Description = "deneme", Price = 9799m, StockQuantity = 27, Image = "https://cdn.akakce.com/z/xiaomi/redmi-note-12-pro-8-gb-128-gb.jpg" }
                };

                    context.Products.AddRange(products);
                    context.SaveChanges();
                }
            }
        }
    }


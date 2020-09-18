using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class DbInitializer
    {
        //A method to initilize the database
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<Data.ApplicationDbContext>();
                //Check if there is any product exist in the database, if not, add the products below to the database
                if (!context.Products.Any())
                {
                    context.AddRange(
                        new Product { productName = "Iphone X", price = 999, productAmount = 100, productInfo = " A phone made by Apple", active = "true", image = "https://i.imgur.com/LU5OmmR.jpg" },
                        new Product { productName = "Spectre X360", price = 1999, productAmount = 100, productInfo = " A laptop made by HP", active = "true", image = "https://i.imgur.com/Ig3Ntrz.jpg" },
                        new Product { productName = "Galaxy Note 10", price = 799, productAmount = 100, productInfo = " A phone made by Samsung", active = "true", image = "https://i.imgur.com/Il6BFvp.jpg" },
                        new Product { productName = "Macbook Pro", price = 2999, productAmount = 100, productInfo = " A laptop made by Apple", active = "true", image = "https://i.imgur.com/4i5vb8s.jpg" },
                        new Product { productName = "Ipad Pro", price = 1099, productAmount = 100, productInfo = " A tablet made by Apple", active = "true", image = "https://i.imgur.com/hWKxFZS.png" },
                        new Product { productName = "Iphone 11", price = 699, productAmount = 100, productInfo = " A phone made by Apple", active = "true", image = "https://i.imgur.com/tmSM8h8.jpg" },
                        new Product { productName = "Thinkpad X1 Extreme", price = 2999, productAmount = 100, productInfo = " A laptop made by Lenovo", active = "true", image = "https://i.imgur.com/XcAkE2J.jpg" },
                        new Product { productName = "LG G7 thinQ", price = 599, productAmount = 100, productInfo = " A laptop made by LG", active = "true", image = "https://i.imgur.com/OVSX3So.jpg" },
                        new Product { productName = "Alienware M17 R2", price = 3999, productAmount = 100, productInfo = " A powerful gaming laptop made by Dell", active = "true", image = "https://i.imgur.com/zifYIXF.jpg" }
                        );
                }
                context.SaveChanges();
            }   
        }
    }
}

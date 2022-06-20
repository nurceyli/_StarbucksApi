using _StarbucksApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _StarbucksApi.Data
{
    public class DataGenerator
    {
        public async static void Initialize(IServiceProvider serviceProvider) 
        {
            using(var context=new StarbucksContext(serviceProvider.GetRequiredService<DbContextOptions<StarbucksContext>>())) 
            {
                if (context.Categories.Count()==0) 
                {
                    context.Categories.AddRange(
                        new Category() { CategoryName="Coffee Beans"},
                        new Category() { CategoryName = "Tea Box" },
                        new Category() { CategoryName = "Peripherals" },
                        new Category() { CategoryName="Soğuk Kahve"}
                        );
                };
                await context.SaveChangesAsync();

                if (context.Products.Count() == 0) 
                {
                    context.Products.AddRange(
                        new Product() { ProductName="Blonde Espresso Roast",Description= "Yoğun ve zengin aromalar ile karamelsi bir tatlılık ", AddTime=DateTime.Now.AddDays(-17),Price=180,CategoryId=1 },
                        new Product() {  ProductName = "Veranda Blend", Description = " Yumuşak kakao  ve hafif kavrulmuş fındık nüansları", AddTime = DateTime.Now.AddDays(-10), Price = 85, CategoryId = 1 },
                        new Product() {  ProductName = "Hibiscus Tea", Description = "Bu çay, hatmi çiçeği ve elmanın sulu mayhoşluğunu, turunçgil tadı içeren limon otuyla birleştirerek çekici bir baz oluşturur. ", AddTime = DateTime.Now.AddDays(-5), Price = 50, CategoryId = 2 },
                        new Product() {  ProductName = "Youthberry Tea", Description = " Ananas, mango ve acai çileği aromalarına sahip bitki çay", AddTime = DateTime.Now.AddDays(-20), Price = 70, CategoryId = 2 },
                        new Product() {  ProductName = "Starbucks Seri Desenli Termos", Description = " Starbucks®  yeşil renkli, desenli, plastik termos / 473ml / 16oz, Ürünlerin kullanım talimatları farklılık gösterebilmektedir.", AddTime = DateTime.Now.AddDays(-17), Price = 699, CategoryId = 3 },
                        new Product() {  ProductName = "Coffee Press", Description = "Kullanılacak çekirdek kahve Coffee Press ölçüsünde öğütülmelidir. ", AddTime = DateTime.Now.AddDays(-18), Price = 100, CategoryId = 3},
                        new Product() { ProductName = "Coffee Press V2", Description = "Kullanılacak çekirdek kahve Coffee Press ölçüsünde öğütülmelidir. ", AddTime = DateTime.Now.AddDays(-18), Price = 100, CategoryId = 4 }
                        );  
                };
                await context.SaveChangesAsync();

                if (context.Comments.Count() == 0) 
                {
                    context.Comments.AddRange(
                        new Comment() { UserName = "Toprak", Text = "Kahvele bizi Starbuckscı çocuk", ProductId = 1 },
                        new Comment() {  UserName = "Murat", Text = "Çok güzel", ProductId = 2 },
                        new Comment() {  UserName = "Kadir", Text = "Çok güzel", ProductId = 3 },
                        new Comment() {  UserName = "Merve", Text = "Çok güzel", ProductId = 4 },
                        new Comment() {  UserName = "Nur", Text = "Çok güzel", ProductId = 5 },
                        new Comment() {  UserName = "Emir", Text = "Çok güzel", ProductId = 6 },
                        new Comment() { UserName = "Yeliz", Text = "Çok güzel", ProductId = 7 }
                        );
                };
                await context.SaveChangesAsync();
            }
        }
    }
}

using System;
using CQRS.BooksAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CQRS.BooksAPI.Data
{
    public static class PrepDb
    {
       public static void PrePopulateDB(IApplicationBuilder app)
       {
            //Getting the instance of AppDbContext, this is a static class 
            using (var scope =  app.ApplicationServices.CreateScope())
            {
                SeedData(scope.ServiceProvider.GetService<AppDbContext>());
            }
       }

       private static void SeedData(AppDbContext context)
       {
            Console.WriteLine("--> Trying for data seeding ");
            try{
                context.Books.AddRange(
                    new Book{Name ="DARKNESS AT NOON", Author = "Arthur Koestler"},
                    new Book {Name = "THE WAY OF ALL FLESH" , Author ="Samuel Butler"}
                );
                context.SaveChanges();                

                Console.WriteLine("--> Completed data seeding");
            }catch(Exception ex)
            {
                Console.WriteLine($"--> Error in data seeding {ex.Message}");
            }
       }
    }
}

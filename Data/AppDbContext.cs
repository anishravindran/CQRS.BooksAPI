using System;
using CQRS.BooksAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CQRS.BooksAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions opt) :base(opt)
        {
            
        }

        public DbSet<Book> Books {get; set;}
        
    }
}

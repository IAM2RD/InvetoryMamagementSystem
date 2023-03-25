using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace IMSAPI.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }    

        public string? Category { get; set; }
        public string? CurrentUser { get; set; } 
    }

    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set;}


        
        public List<Item>? Items {get; set; }


    }

    public class ItemDb : DbContext
    {
        public ItemDb(DbContextOptions options) : base(options) { }
        public DbSet<Item> Items { get; set;}
        public DbSet<User> Users { get; set; }
        
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using reservation_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travel.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Command> Commands { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<ProductInCommand> ProductInCommands { get; set; }
        public virtual DbSet<UserRole> Roles { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProductInCommand>()
           .HasKey(e => new { e.CommandID, e.ProductID });

            builder.Entity<ProductInCommand>()
            .HasOne<Product>(e => e.Products)
            .WithMany(p => p.ProductInCommands);

            builder.Entity<ProductInCommand>()
            .HasOne<Command>(e => e.Commands)
            .WithMany(p => p.ProductInCommands);
            builder.Entity<Category>().HasKey(c => c.CatID);
            base.OnModelCreating(builder);


        }
    }
}

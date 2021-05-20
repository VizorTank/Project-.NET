using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project_.NET.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project_.NET.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Favourite> Favourites { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Favourite>().HasKey(c => new { c.RecipeId, c.UserId });
            modelBuilder.Entity<Favourite>().HasOne(f => f.User).WithMany(u => u.Favourites).HasForeignKey(f => f.UserId);
            modelBuilder.Entity<Favourite>().HasOne(f => f.Recipe).WithMany(r => r.Favourites).HasForeignKey(f => f.RecipeId);
        }
        
    }
}

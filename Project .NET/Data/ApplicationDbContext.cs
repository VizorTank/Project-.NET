
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project_.NET.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project_.NET.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Like> Likes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Favorite>().HasKey(c => new { c.RecipeId, c.UserId });
            modelBuilder.Entity<Favorite>().HasOne(f => f.User).WithMany(u => u.Favorites).HasForeignKey(f => f.UserId);
            modelBuilder.Entity<Favorite>().HasOne(f => f.Recipe).WithMany(r => r.Favorites).HasForeignKey(f => f.RecipeId);

            modelBuilder.Entity<Like>().HasKey(c => new { c.RecipeId, c.UserId });
            modelBuilder.Entity<Like>().HasOne(f => f.User).WithMany(u => u.Likes).HasForeignKey(f => f.UserId);
            modelBuilder.Entity<Like>().HasOne(f => f.Recipe).WithMany(r => r.Likes).HasForeignKey(f => f.RecipeId);
        }
    }
}

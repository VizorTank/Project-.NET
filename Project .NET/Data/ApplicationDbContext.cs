
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
        public DbSet<Like> Likes { get; set; }
        public DbSet<RecipeCategory> RecipeCategories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<RecipeUser> RecipeUsers { get; set; }
        public DbSet<FavouriteAutor> FavouriteAutors { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Favourite>().HasKey(c => new { c.RecipeId, c.UserId });
            modelBuilder.Entity<Favourite>().HasOne(f => f.User).WithMany(u => u.Favorites).HasForeignKey(f => f.UserId);
            modelBuilder.Entity<Favourite>().HasOne(f => f.Recipe).WithMany(r => r.Favorites).HasForeignKey(f => f.RecipeId);

            modelBuilder.Entity<Like>().HasKey(c => new { c.RecipeId, c.UserId });
            modelBuilder.Entity<Like>().HasOne(f => f.User).WithMany(u => u.Likes).HasForeignKey(f => f.UserId);
            modelBuilder.Entity<Like>().HasOne(f => f.Recipe).WithMany(r => r.Likes).HasForeignKey(f => f.RecipeId);

            modelBuilder.Entity<RecipeCategory>().HasKey(c => new { c.RecipeId, c.CategoryId });
            modelBuilder.Entity<RecipeCategory>().HasOne(f => f.Recipe).WithMany(r => r.RecipeCategories).HasForeignKey(f => f.RecipeId);
            modelBuilder.Entity<RecipeCategory>().HasOne(f => f.Category).WithMany(u => u.RecipeCategories).HasForeignKey(f => f.CategoryId);

            modelBuilder.Entity<RecipeUser>().HasKey(c => new { c.RecipeId, c.UserId });
            modelBuilder.Entity<RecipeUser>().HasOne(f => f.Recipe).WithMany(r => r.RecipeUsers).HasForeignKey(f => f.RecipeId);
            modelBuilder.Entity<RecipeUser>().HasOne(f => f.User).WithMany(u => u.RecipeUsers).HasForeignKey(f => f.UserId);

            modelBuilder.Entity<RecipeIngredient>().HasKey(c => new { c.RecipeId, c.IngredientId });
            modelBuilder.Entity<RecipeIngredient>().HasOne(f => f.Recipe).WithMany(r => r.RecipeIngredients).HasForeignKey(f => f.RecipeId);
            modelBuilder.Entity<RecipeIngredient>().HasOne(f => f.Ingredient).WithMany(u => u.RecipeIngredients).HasForeignKey(f => f.IngredientId);

            modelBuilder.Entity<FavouriteAutor>().HasKey(c => new { c.UserId, c.AutorId });
            modelBuilder.Entity<FavouriteAutor>().HasOne(f => f.User).WithMany(r => r.FavouriteAutors).HasForeignKey(f => f.UserId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<FavouriteAutor>().HasOne(f => f.Autor).WithMany(u => u.FavouritedByUsers).HasForeignKey(f => f.AutorId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Image>().HasOne(f => f.Recipe).WithMany(u => u.Images).HasForeignKey(f => f.RecipeId);
        }
    }
}

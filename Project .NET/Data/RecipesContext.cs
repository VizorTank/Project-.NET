using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project_.NET.Models;

namespace Project_.NET.Data
{
    public class RecipesContext : DbContext
    {
        public RecipesContext(DbContextOptions<RecipesContext> options) : base(options) { }
        public DbSet<Recipes> Recipes { get; set; }
        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Recipes>()
                .HasOne(p => p.User_Id)
                .WithMany(b => b.Id)
                .HasForeignKey("UserForeignKey");
        }
        */
    }
}

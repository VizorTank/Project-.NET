using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project_.NET.Models;

namespace Project_.NET.Data
{
    public class RecipesData : DbContext
    {
        public RecipesData(DbContextOptions<RecipesData> options) : base(options) { }
        public DbSet<Recipes> Recipes { get; set; }
    }
}

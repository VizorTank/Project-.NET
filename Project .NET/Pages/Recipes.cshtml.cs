using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project_.NET.Data;
using Project_.NET.Models;
namespace Project_.NET.Pages.Shared
{
    public class RecipesModel : RecipesFun
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        public RecipesModel(ApplicationDbContext cont, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment) : base(cont, userManager, "./Recipes", webHostEnvironment)
        { }
        public void  OnGet()
        {
            var RPQuerry = (from Recipes in _cont.Recipes where Recipes.Id==Id orderby Recipes.date descending select Recipes).Include(u => u.User);
            Recipes = RPQuerry.ToList();
        }

    }
}

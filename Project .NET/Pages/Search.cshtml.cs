using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Project_.NET.Data;
using Project_.NET.Models;

namespace Project_.NET.Pages
{
    public class SearchModel : RecipesFun
    {
        [BindProperty]
        public string UserName { get; set; }
        [BindProperty]
        public string CategoryName { get; set; }
        [BindProperty]
        public string IngredientName { get; set; }
        [BindProperty]
        public string RecipeName { get; set; }
        [BindProperty]
        public bool SortType { get; set; }
        [BindProperty]
        public bool SortOrder { get; set; }
        public string ToolTip { get; set; }

        public SearchModel(ApplicationDbContext cont, 
            UserManager<ApplicationUser> userManager, 
            IWebHostEnvironment webHostEnvironment) : base(cont, userManager, webHostEnvironment)
        { }
        public void OnGet()
        {
            var tmpRecipes = (from Recipe 
                       in _cont.Recipes 
                       orderby Recipe.date descending
                       select Recipe).Include(r => r.RecipeUsers).ThenInclude(u => u.User).OrderBy(u => u.date);

            if (!SortType)
                Recipes = tmpRecipes.OrderBy(u => u.date).ToList();
            else
                Recipes = tmpRecipes.OrderBy(u => u.Likes).ToList();
        }

        public void OnPostSearchAsync()
        {
            var querry = _cont.Recipes
                .Include(r => r.RecipeCategories).ThenInclude(rc => rc.Category)
                .Include(r => r.RecipeUsers).ThenInclude(u => u.User)
                .Select(x => x);
            if (UserName != null)
            {
                querry = querry.Where(u => u.RecipeUsers.Any(k => k.User.UserName == UserName));
            }
            if (RecipeName != null)
            {
                querry = querry.Where(r => r.Name.Contains(RecipeName));
            }
            if (CategoryName != null)
            {
                querry = querry.Where(r => r.RecipeCategories.Any(r => r.Category.Name == CategoryName));
            }
            if (IngredientName != null)
            {
                querry = querry.Where(r => r.RecipeIngredients.Any(r => r.Ingredient.Name == IngredientName));
            }
            if (!SortType)
            {
                if (SortOrder)
                    Recipes = querry.OrderBy(r => r.date).ToList();
                else
                    Recipes = querry.OrderByDescending(r => r.date).ToList();
            }
            else
            {
                if (SortOrder)
                    Recipes = querry.OrderBy(r => r.Votes).ToList();
                else
                    Recipes = querry.OrderByDescending(r => r.Votes).ToList();
            }
        }
    }
}

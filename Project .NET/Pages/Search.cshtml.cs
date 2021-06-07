using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project_.NET.Data;
using Project_.NET.Models;

namespace Project_.NET.Pages
{
    public class SearchModel : PageModel
    {
        private readonly ApplicationDbContext _cont;
        private readonly UserManager<ApplicationUser> _userManager;

        [BindProperty]
        public string UserName { get; set; }
        [BindProperty]
        public string CategoryName { get; set; }
        [BindProperty]
        public string RecipeName { get; set; }

        public string ToolTip { get; set; }

        public IList<Recipe> Recipes { get; set; }

        public SearchModel(ApplicationDbContext cont, UserManager<ApplicationUser> userManager)
        {
            this._cont = cont;
            this._userManager = userManager;
        }
        public void OnGet()
        {
            Recipes = (from Recipe 
                       in _cont.Recipes 
                       orderby Recipe.Votes descending 
                       select Recipe).Include(u => u.User).Take(10).ToList();
        }

        public void OnPostUserAsync()
        {
            ToolTip = "Wyszukiwanie receptury po u¿ytkowniku \"" + UserName + "\"";
            Recipes = (from Recipe 
                       in _cont.Recipes 
                       where Recipe.User.UserName == UserName
                       orderby Recipe.Votes descending 
                       select Recipe).Include(u => u.User).Take(10).ToList();
        }

        public void OnPostRecipeAsync()
        {
            ToolTip = "Wyszukiwanie receptury po nazwie \"" + RecipeName + "\"";
            Recipes = (from Recipe 
                       in _cont.Recipes
                       where Recipe.Name.Contains(RecipeName)
                       orderby Recipe.Votes descending 
                       select Recipe).Include(u => u.User).Take(10).ToList();
        }

        public void OnPostCategoryAsync()
        {
            ToolTip = "Wyszukiwanie receptury po kategorii " + CategoryName;
            Recipes = (from Recipe
                       in _cont.Recipes
                       where Recipe.RecipeCategories.Any(r => r.Category.Name == CategoryName)
                       orderby Recipe.Votes descending
                       select Recipe).Include(u => u.User).Include(r => r.RecipeCategories).ThenInclude(u => u.Category).Take(10).ToList();
        }

        public IList<Category> GetCategories()
        {
            IList<Category> categories = (from Category in _cont.Categories select Category).ToList();
            if (categories != null)
                return categories;
            return new List<Category>();
        }

        public IList<RecipeCategory> GetCategories(Recipe recipe)
        {
            if (recipe != null)
            {
                IList<RecipeCategory> categories = (from RecipeCategory in _cont.RecipeCategories where RecipeCategory.Recipe == recipe select RecipeCategory).Include(c => c.Category).ToList();
                if (categories != null)
                    return categories;
            }
            return new List<RecipeCategory>();
        }
    }
}

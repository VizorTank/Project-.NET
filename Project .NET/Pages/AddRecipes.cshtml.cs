using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Project_.NET.Data;
using Project_.NET.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace Project_.NET.Pages
{
    [Authorize]
    public class AddRecipesModel : PageModel
    {
        private readonly ApplicationDbContext _cont;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty, Required(ErrorMessage = "Pole Nazwa jest wymagane "), MaxLength(50, ErrorMessage = "Miej ni¿ 50 znaków")]
        public string AddName { get; set; }
        //[BindProperty, Required(ErrorMessage = "Pole Sk³adniki jest wymagane "), MaxLength(2500, ErrorMessage = "Miej ni¿ 2500 znaków")]
        //public string AddIngs { get; set; }

        [BindProperty, Required(ErrorMessage = "Pole Opis Przygotowania jest wymagane "), MaxLength(2500, ErrorMessage = "Miej ni¿ 2500 znaków")]
        public string AddDesc { get; set; }
        [BindProperty]
        public string AddImg { get; set; }
        [BindProperty]
        public IFormFile Foto { get; set; }
        public IList<Category> Categories { get; set; }
        [BindProperty]
        public IList<string> ChosenCategories { get; set; }
        [BindProperty]
        public IList<string> ChosenUsers { get; set; }
        [BindProperty]
        public IList<string> ChosenIng { get; set; }
        public int recipeId { get; set; }

        public void OnGet() { }
        public AddRecipesModel(ApplicationDbContext cont, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _cont = cont;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult OnPostAdd(int itemId)
        {
            recipeId = itemId;
            if (ModelState.IsValid)
            {
                Recipe recipe;
                if (GetRecipe(recipeId) != null)
                {
                    recipe = GetRecipe(recipeId);
                    recipe.Name = AddName;
                    //recipe.Ings = AddIngs;
                    recipe.Desc = AddDesc;
                    RemoveRecipeCategories(recipe);
                    RemoveRecipeIngriedient(recipe);
                    RemoveRecipeUser(recipe);
                }
                else
                {
                    if (Foto != null)
                    {
                        AddImg = "../images/" + PrecessFoto();
                    }

                    recipe = new Recipe(AddName, AddDesc, AddImg);
                    RecipeUser recipeUser = new RecipeUser(GetUser(), recipe);

                    _cont.RecipeUsers.Add(recipeUser);
                    _cont.Recipes.Add(recipe);
                }


                foreach (string item in ChosenUsers)
                 {
                    AddRecipeUser(recipe, GetUser(item));
                    
                 }
                foreach (string item in ChosenCategories)
                 {
                   AddRecipeCategory(recipe, GetCategory(item));
                 }
                foreach (string item in ChosenIng)
                {
                    AddRecipeIng(recipe, GetIngriedient(item));
                }
                _cont.SaveChanges();
                return RedirectToPage("./AllRecipes");
            }
            return Page();
        }

        public Category GetCategory(string name)
        {
            return (from Category in _cont.Categories where Category.Name == name select Category).SingleOrDefault();
        }
        public Ingriedient GetIngriedient(string name)
        {
            return (from IG in _cont.Ingredients where IG.Name == name select IG).SingleOrDefault();
        }
        public IList<Category> GetCategories()
        {
            IList<Category> categories = (from Category in _cont.Categories select Category).ToList();
            if (categories != null)
                return categories;
            return new List<Category>();
        }

        public IList<ApplicationUser> GetUsers()
        {
            IList<ApplicationUser> users = (from User in _cont.Users select User).ToList();
            if (users != null)
            {
                users.Remove(GetUser());
                return users;
            }
            return new List<ApplicationUser>();
        }
        public IList<Ingriedient> GetIngredients()
        {
            IList<Ingriedient> ings = (from IG in _cont.Ingredients select IG).ToList();
            if (ings != null)
            {
                return ings;
            }
            return new List<Ingriedient>();
        }
        public bool IsInSelectedCategory(Category category)
        {
            Recipe recipe = GetRecipe(recipeId);
            if (recipe != null)
            {
                var querry = (from RecipeCategory 
                              in _cont.RecipeCategories 
                              where RecipeCategory.Recipe == recipe 
                              && RecipeCategory.Category == category 
                              select RecipeCategory).Count();
                if (querry == 1)
                    return true;
            }
            
            return false;
        }
        public bool IsInAutors(ApplicationUser user)
        {
            Recipe recipe = GetRecipe(recipeId);
            if(recipe!=null)
            {
                var querry = (from RecipeUser
                              in _cont.RecipeUsers
                              where RecipeUser.Recipe == recipe &&
                              RecipeUser.User == user
                              select RecipeUser).Count();
                if (querry == 1)
                    return true;
            }

            return false;
        }
        public bool IsInSelectedIngriedients(Ingriedient ingriedient)
        {
            Recipe recipe = GetRecipe(recipeId);
            if (recipe != null)
            {
                var querry = (from RI
                              in _cont.RecipeIngredients
                              where RI.Recipe == recipe &&
                              RI.Ingredient == ingriedient
                              select RI).Count();
                if (querry == 1)
                    return true;
            }

            return false;

        }
        public IList<RecipeUser> GetUsers(Recipe recipe)
        {
            if(recipe!=null)
            {
                IList<RecipeUser> users = (from RecipeUser in _cont.RecipeUsers where RecipeUser.Recipe == recipe select RecipeUser).Include(c => c.User).ToList();
                if (users != null)
                    return users;

            }
            return new List<RecipeUser>();
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
        public IList<RecipeIngredient> GetIngredients(Recipe recipe)
        {
            if (recipe!=null)
            {
                IList<RecipeIngredient> ingredients = (from RI in _cont.RecipeIngredients where RI.Recipe == recipe select RI).Include(i => i.Ingredient).ToList();
                if (ingredients != null)
                    return ingredients;
            }
            return new List<RecipeIngredient>();
        }

        public void OnPostEdit(int itemId)
        {
            recipeId = itemId;
            Recipe recipe = GetRecipe(itemId);
            AddName = recipe.Name;
            //AddIngs = recipe.Ings;
            AddDesc = recipe.Desc;
        }
        public Recipe GetRecipe(int recipeId)
        {
            return (from Recipes
                    in _cont.Recipes
                    where Recipes.Id == recipeId
                    orderby Recipes.date
                    select Recipes).FirstOrDefault();
        }
        public void AddRecipeUser(Recipe recipe, ApplicationUser User)
        {
            _cont.RecipeUsers.Add(new RecipeUser(User, recipe));
        }
        public void AddRecipeCategory(Recipe recipe, Category category)
        {
            _cont.RecipeCategories.Add(new RecipeCategory(recipe, category));
        }
        public void AddRecipeIng(Recipe recipe, Ingriedient ingriedient)
        {
            _cont.RecipeIngredients.Add(new RecipeIngredient(recipe, ingriedient));
        }

        public void RemoveRecipeCategories(Recipe recipe)
        {
            IList<RecipeCategory> recipeCategories = (from RC in _cont.RecipeCategories where RC.Recipe == recipe select RC).ToList();
            foreach(var item in recipeCategories)
            {
                _cont.RecipeCategories.Remove(item);
            }
        }
        public void RemoveRecipeUser(Recipe recipe)
        {
            IList<RecipeUser> recipeUsers = (from RU in _cont.RecipeUsers where RU.Recipe == recipe select RU).ToList();
            foreach (var item in recipeUsers)
            {
                _cont.RecipeUsers.Remove(item);
            }
        }
        public void RemoveRecipeIngriedient(Recipe recipe)
        {
            IList<RecipeIngredient> recipeIngs = (from RI in _cont.RecipeIngredients where RI.Recipe == recipe select RI).ToList();
            foreach (var item in recipeIngs)
            {
                _cont.RecipeIngredients.Remove(item);
            }
        }

        public ApplicationUser GetUser()
        {
            Task<ApplicationUser> identityUser = _userManager.GetUserAsync(HttpContext.User);
            return identityUser.Result;
        }
        public ApplicationUser GetUser(string username)
        {
            ApplicationUser user = (from User in _cont.Users where User.UserName == username select User).FirstOrDefault();
            return user;
        }
        private string PrecessFoto()
        {
            string uniFileName = null;
            
            if (Foto != null)
            {
                string uploadsfolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniFileName = Guid.NewGuid().ToString() + "_" + Foto.FileName;
                string filePath = (Path.Combine(uploadsfolder, uniFileName));
                using (var fileStream = new FileStream(filePath, FileMode.Create)) 
                {
                    Foto.CopyTo(fileStream);
                }
            }
            return uniFileName;
        }
    }
}

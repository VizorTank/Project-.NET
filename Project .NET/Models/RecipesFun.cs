using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project_.NET.Data;

namespace Project_.NET.Models
{
    
    public class RecipesFun : PageModel
    {
        public string letter;
        public IList<Recipe> Recipes { get; set; }
        public IList<Like> LDEL { get; set; }
        public QueryString GetData { get; set; }
        protected readonly UserManager<ApplicationUser> _userManager;
        protected readonly ApplicationDbContext _cont;
        protected readonly string _NamePage;
        private readonly IWebHostEnvironment _webHostEnvironmen;

        public RecipesFun(ApplicationDbContext cont, UserManager<ApplicationUser> userManager, string NamePage, IWebHostEnvironment webHostEnvironmen)
        {
            _cont = cont;
            _userManager = userManager;
            _NamePage = NamePage;
            _webHostEnvironmen = webHostEnvironmen;
        }
        
        public IActionResult OnPostDeleteAsync(int itemId, string getData)
        {
            ApplicationUser user = GetUser();
            if (user == null)
                return Reload(getData);
            Recipe recipe = GetRecipe(itemId);
            if (recipe != null && recipe.User == user)
            {
                removeFoto(recipe.Img);
                _cont.Recipes.Remove(recipe);
                _cont.SaveChanges();
            }

            return Reload(getData);
        }
        public IActionResult OnPostEditAsync(int itemId)
        {
            return RedirectToPage("./Recipes?Id=" + itemId.ToString());
        }
        public IActionResult OnPostLikeAsync(int itemId, string getData)
        {
            return ChangeLike(itemId, 1, getData);
        }
        public IActionResult OnPostHateAsync(int itemId, string getData)
        {
            return ChangeLike(itemId, -1, getData);
        }
        public IActionResult ChangeLike(int itemId, int value, string getData)
        {
            ApplicationUser user = GetUser();
            if (user == null)
                return Reload(getData);
            Recipe recipe = GetRecipe(itemId);
            Like like = GetLike(recipe, user);

            if (like == null)
            {
                Like newlike = new Like(user, recipe, value);
                _cont.Likes.Add(newlike);
                recipe.Votes = recipe.Votes + value;
                _cont.Recipes.Update(recipe);
                _cont.SaveChanges();
            }
            else if (like.Value != value)
            {
                like.Value = like.Value + value;
                _cont.Likes.Update(like);
                recipe.Votes = recipe.Votes + value;
                _cont.Recipes.Update(recipe);
                _cont.SaveChanges();
            }

            return Reload(getData);
        }
        public IActionResult OnPostFavoriteAsync(int itemId, string getData)
        {
            return ChangeFavorite(itemId, true, getData);
        }
        public IActionResult OnPostUnFavoriteAsync(int itemId, string getData)
        {
            return ChangeFavorite(itemId, false, getData);
        }
        public IActionResult ChangeFavorite(int itemId, bool value, string getData)
        {
            ApplicationUser user = GetUser();
            if (user == null)
                return Reload(getData);
            Recipe recipe = GetRecipe(itemId);

            Favorite favorite = GetFavorite(recipe, user);
            if (favorite == null)
            {
                Favorite favi = new Favorite(recipe, user, value);
                _cont.Favorites.Add(favi);
                _cont.SaveChanges();
            }
            else if (favorite.value != value)
            {
                favorite.value = value;
                _cont.Favorites.Update(favorite);
                _cont.SaveChanges();
            }
            return Reload(getData);
        }
        public void removeFoto(string imgname)
        {
            if(imgname!=null && imgname.Contains("../images/"))
            {
                imgname.Replace("../images/", "");
                string filePath = Path.Combine(_webHostEnvironmen.WebRootPath,"images", imgname);
                System.IO.File.Delete(filePath);
            }
        }
        public string RecipePage(int Id)
        {
            return "./Recipes?Id=" + Id.ToString();
        }
        public string UserPage(string username)
        {
            return "./User?username=" + username;
        }
        public ApplicationUser GetUser()
        {
            Task<ApplicationUser> identityUser = _userManager.GetUserAsync(HttpContext.User);
            return identityUser.Result;
        }
        public ApplicationUser GetUserByName(string username)
        {
            var querry = _userManager.Users.Where(u => u.UserName == username).FirstOrDefault();
            return querry;
        }
        public string GetUserId()
        {
            return _userManager.GetUserId(HttpContext.User);
        }
        public bool IsFavorite(int itemId)
        {
            ApplicationUser user = GetUser();
            Recipe recipe = GetRecipe(itemId);
            Favorite favorite = GetFavorite(recipe, user);
            if (favorite == null)
                return false;
            else 
                return favorite.value;
        }
        public Recipe GetRecipe(int recipeId)
        {
            return (from Recipes 
                    in _cont.Recipes 
                    where Recipes.Id == recipeId 
                    orderby Recipes.date 
                    select Recipes).FirstOrDefault();
        }
        public Like GetLike(Recipe recipe, ApplicationUser user)
        {
            return (from Like 
                    in _cont.Likes 
                    where Like.User == user && Like.Recipe == recipe 
                    select Like).FirstOrDefault();
        }
        public Favorite GetFavorite(Recipe recipe, ApplicationUser user)
        {
            return (from Favorite 
                    in _cont.Favorites 
                    where Favorite.User == user && Favorite.Recipe == recipe 
                    select Favorite).ToList().LastOrDefault();
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
        public virtual IActionResult Reload(string getData)
        {
            return Redirect(HttpContext.Request.Path + getData);
        }
    }
}


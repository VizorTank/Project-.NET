using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
        public IList<Recipe> RP { get; set; }
        public IList<Like> LDEL { get; set; }
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
        
        public virtual void OnGet()
        { }

        public IActionResult OnPostDeleteAsync(int itemId, string userId)
        {
            if (userId != null && userId.CompareTo(GetUserId()) == 0)
                DelRep(itemId);
            return RedirectToPage(_NamePage);
        }


        
        public IActionResult OnPostEditAsync(int itemId)
        {
            return RedirectToPage("./Recipes?Id=" + itemId.ToString());
        }
        // Likes
        public IActionResult OnPostLikeAsync(int itemId, string userId)
        {
            return ChangeLike(itemId, 1);
        }
        public IActionResult OnPostHateAsync(int itemId, string userId)
        {
            return ChangeLike(itemId, -1);
        }

        public IActionResult ChangeLike(int itemId, int value)
        {
            ApplicationUser user = GetUser();
            if (user == null)
                return RedirectToPage(_NamePage);
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

            return RedirectToPage(_NamePage);
        }
        // Favorites
        public IActionResult OnPostFavoriteAsync(int itemId, string userId)
        {
            ApplicationUser user = GetUser();
            if (user == null)
                return RedirectToPage(_NamePage);
            Recipe RPUp = GetRecipe(itemId);
            Favorite FavQ = (from Favorite in _cont.Favorites where Favorite.User == user && Favorite.Recipe == RPUp select Favorite).ToList().LastOrDefault();
            if (FavQ == null)
            {
                Favorite favi = new Favorite(RPUp, user, true);
                _cont.Favorites.Add(favi);
                _cont.SaveChanges();
            }
            else if (FavQ.value == false)
            {
                FavQ.value = true;
                _cont.Favorites.Update(FavQ);
                _cont.SaveChanges();
            }
            return RedirectToPage(_NamePage);
        }
        
        public IActionResult OnPostUnFavoriteAsync(int itemId, string userId)
        {
            Recipe RPUp = (from Recipes in _cont.Recipes where Recipes.Id == itemId orderby Recipes.date select Recipes).FirstOrDefault();
            ApplicationUser user = GetUser();
            Favorite FavQ = (from Favorite in _cont.Favorites where Favorite.User == user && Favorite.Recipe == RPUp select Favorite).ToList().LastOrDefault();
            if (FavQ == null)
            {
                Favorite favi = new Favorite(RPUp, user, false);
                _cont.Favorites.Add(favi);
                _cont.SaveChanges();
            }
            else if (FavQ.value == true)
            {
                FavQ.value = false;
                _cont.Favorites.Update(FavQ);
                _cont.SaveChanges();
            }
            return RedirectToPage(_NamePage);
        }
        public void ChangeFavorite()
        {

        }
        public void DelRep(int recipeId)
        {
            Recipe recipe = GetRecipe(recipeId);
            if (recipe != null)
            {
                removeFoto(recipe.Img);
                _cont.Recipes.Remove(recipe);
                _cont.SaveChanges();
            }
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
        public string Letter(int Id)
        {
            return "./Recipes?Id=" + Id.ToString();
        }

        public ApplicationUser GetUser()
        {
            Task<ApplicationUser> identityUser = _userManager.GetUserAsync(HttpContext.User);
            return identityUser.Result;
        }

        public string GetUserId()
        {
            return _userManager.GetUserId(HttpContext.User);
        }

        public bool IsFavorite(int itemId)
        {
            ApplicationUser user = GetUser();
            Recipe RPUp = GetRecipe(itemId);
            Favorite FavQ = (from Favorite
                            in _cont.Favorites
                             where Favorite.User == user && Favorite.Recipe == RPUp
                             select Favorite).ToList().LastOrDefault();
            if (FavQ == null)
                return false;
            else 
                return FavQ.value;
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
                    select Favorite).LastOrDefault();
        }
    }
}


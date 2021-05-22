using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_.NET.Data;
using Project_.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_.NET.Secrvices
{
    public class RecipeController : PageModel
    {
        protected readonly UserManager<ApplicationUser> _userManager;
        protected readonly ApplicationDbContext _cont;
        public RecipeController(ApplicationDbContext cont, UserManager<ApplicationUser> userManager)
        {
            _cont = cont;
            _userManager = userManager;
        }
        // User
        public ApplicationUser GetUser()
        {
            Task<ApplicationUser> identityUser = _userManager.GetUserAsync(HttpContext.User);
            return identityUser.Result;
        }
        public string GetUserName(string userId)
        {
            var username = _userManager.FindByIdAsync(userId).Result;
            if (username != null)
                return username.UserName;
            return "Anonim";
        }
        public string GetUserId()
        {
            return _userManager.GetUserId(HttpContext.User);
        }
        // Recipes
        protected void DelRep(int itemId)
        {
            Recipe RPDel = GetRecipe(itemId);
            if (RPDel != null)
            {
                _cont.Recipes.Remove(RPDel);
                _cont.SaveChanges();
            }
        }
        protected Recipe GetRecipe(int itemId)
        {
            return (from Recipes in _cont.Recipes where Recipes.Id == itemId orderby Recipes.date select Recipes).FirstOrDefault();
        }
        // Favorites
        protected Favorite GetFavorites(ApplicationUser user, Recipe RPUp)
        {
            return (from Favorite in _cont.Favorites where Favorite.User == user && Favorite.Recipe == RPUp select Favorite).ToList().LastOrDefault();
        }
        protected void DelFavorite(Favorite FavQ)
        {
            _cont.Favorites.Remove(FavQ);
            _cont.SaveChanges();
        }
        protected void AddFavorite(ApplicationUser user, Recipe RPUp)
        {
            Favorite favi = new Favorite(user, RPUp, true);
            _cont.Favorites.Add(favi);
            _cont.SaveChanges();
        }
        public bool IsFavorite(int itemId)
        {
            ApplicationUser user = GetUser();
            Recipe RPUp = GetRecipe(itemId);
            Favorite FavQ = GetFavorites(user, RPUp);
            if (FavQ != null)
                return true;
            else return false;
        }
        // Likes
        protected void ModifyLike(Recipe RPUp, Like LQ, int value)
        {
            LQ.Value = LQ.Value + value;
            _cont.Likes.Update(LQ);
            RPUp.up_vote = RPUp.up_vote + value;
            _cont.Recipes.Update(RPUp);
            _cont.SaveChanges();
        }
        protected void AddLike(ApplicationUser user, Recipe RPUp, int value)
        {
            Like newlike = new Like(user, RPUp, value);
            _cont.Likes.Add(newlike);
            RPUp.up_vote = RPUp.up_vote + value;
            _cont.Recipes.Update(RPUp);
            _cont.SaveChanges();
        }
        protected Like GetLike(ApplicationUser user, Recipe RPUp)
        {
            return (from Like in _cont.Likes where Like.User == user && Like.Recipe == RPUp select Like).ToList().ToList().LastOrDefault();
        }
    }
}

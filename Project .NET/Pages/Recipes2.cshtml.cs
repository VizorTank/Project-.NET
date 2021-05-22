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
    public class Recipes2Model : PageModel
    {
        public IList<Recipe> RP { get; set; }
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _cont;
        public Recipes2Model(ApplicationDbContext cont, UserManager<ApplicationUser> userManager)
        {
            _cont = cont;
            _userManager = userManager;
        }
        public void OnGet()
        {
            var RPQuerry = (from Recipes in _cont.Recipes orderby Recipes.date descending select Recipes).Include(u => u.User);
            RP = RPQuerry.ToList();
        }
        public IActionResult OnPostEditAsync()
        {
            return RedirectToPage("./Recipes");
        }

        // Recipes
        public IActionResult OnPostDeleteAsync(int itemId, string userId)
        {
            if (userId != null && userId.CompareTo(GetUserId()) == 0)
            {
                DelRep(itemId);
            }
            
            return  RedirectToPage("./Recipes2");
        }
        public void DelRep(int itemId)
        {
            Recipe RPDel = GetRecipe(itemId);
            if (RPDel != null)
            {
                _cont.Recipes.Remove(RPDel);
                _cont.SaveChanges();
            }
        }

        // Likes
        public IActionResult OnPostLikeAsync(int itemId, string userId)
        {
            Recipe RPUp = GetRecipe(itemId);
            ApplicationUser user = GetUser();
            Like LQ = GetLike(user, RPUp);
            if (LQ == null)
            {
                AddLike(user, RPUp, 1);
            }
            else if (LQ.Value < 1)
            {
                ModifyLike(RPUp, LQ, 1);
            }

            return RedirectToPage("./Recipes2");
        }
        public IActionResult OnPostHateAsync(int itemId, string userId)
        {
            Recipe RPUp = GetRecipe(itemId);
            ApplicationUser user = GetUser();
            Like LQ = GetLike(user, RPUp);
            if (LQ == null)
            {
                AddLike(user, RPUp, -1);
            }
            else if (LQ.Value > -1)
            {
                ModifyLike(RPUp, LQ, -1);
            }

            return RedirectToPage("./Recipes2");
        }

        private void ModifyLike(Recipe RPUp, Like LQ, int value)
        {
            LQ.Value = LQ.Value + value;
            _cont.Likes.Update(LQ);
            RPUp.up_vote = RPUp.up_vote + value;
            _cont.Recipes.Update(RPUp);
            _cont.SaveChanges();
        }
        private void AddLike(ApplicationUser user, Recipe RPUp, int value)
        {
            Like newlike = new Like(user, RPUp, value);
            _cont.Likes.Add(newlike);
            RPUp.up_vote = RPUp.up_vote + value;
            _cont.Recipes.Update(RPUp);
            _cont.SaveChanges();
        }
        private Like GetLike(ApplicationUser user, Recipe RPUp)
        {
            return (from Like in _cont.Likes where Like.User == user && Like.Recipe == RPUp select Like).ToList().ToList().LastOrDefault();
        }

        // Favorites
        public IActionResult OnPostFavoriteAsync(int itemId, string userId)
        {
            Recipe RPUp = GetRecipe(itemId);
            ApplicationUser user = GetUser();
            Favorite FavQ = GetFavorites(user, RPUp);
            if (FavQ==null)
            {
                AddFavorite(user, RPUp);
            }

            return RedirectToPage("./Recipes2");
        }

        public IActionResult OnPostUnFavoriteAsync(int itemId, string userId)
        {
            Recipe RPUp = GetRecipe(itemId);
            ApplicationUser user = GetUser();
            Favorite FavQ = GetFavorites(user, RPUp);
            if (FavQ != null)
            {
                DelFavorite(FavQ);
            }
            
            return RedirectToPage("./Recipes2");
        }

        private void DelFavorite(Favorite FavQ)
        {
            _cont.Favorites.Remove(FavQ);
            _cont.SaveChanges();
        }
        private void AddFavorite(ApplicationUser user, Recipe RPUp)
        {
            Favorite favi = new Favorite(user, RPUp, true);
            _cont.Favorites.Add(favi);
            _cont.SaveChanges();
        }

        // Other functions
        public ApplicationUser GetUser()
        {
            Task<ApplicationUser> identityUser = _userManager.GetUserAsync(HttpContext.User);
            return identityUser.Result;
        }
        private Recipe GetRecipe(int itemId)
        {
            return (from Recipes in _cont.Recipes where Recipes.Id == itemId orderby Recipes.date select Recipes).FirstOrDefault();
        }
        private Favorite GetFavorites(ApplicationUser user, Recipe RPUp)
        {
            return (from Favorite in _cont.Favorites where Favorite.User == user && Favorite.Recipe == RPUp select Favorite).ToList().LastOrDefault();
        }

        public bool UserProperty(string userId)
        {
            if (userId == _userManager.GetUserId(HttpContext.User))
                return true;
            else
                return false;
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
        public bool IsFavorite(int itemId)
        {
            ApplicationUser user = GetUser();
            Recipe RPUp = GetRecipe(itemId);
            Favorite FavQ = GetFavorites(user, RPUp);
            if (FavQ != null)
                return true;
            else return false;
        }
    }
}

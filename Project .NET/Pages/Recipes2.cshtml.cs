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
using Project_.NET.Secrvices;

namespace Project_.NET.Pages
{
    public class Recipes2Model : RecipeController
    {
        public IList<Recipe> RP { get; set; }
        public Recipes2Model(ApplicationDbContext cont, UserManager<ApplicationUser> userManager) : base (cont, userManager) { }
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
        // Likes
        public IActionResult OnPostLikeAsync(int itemId, string userId)
        {
            Recipe RPUp = GetRecipe(itemId);
            ApplicationUser user = GetUser();
            if (user == null)
                return RedirectToPage("./Recipes2");
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
            if (user == null)
                return RedirectToPage("./Recipes2");
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
        // Favorites
        public IActionResult OnPostFavoriteAsync(int itemId, string userId)
        {
            Recipe RPUp = GetRecipe(itemId);
            ApplicationUser user = GetUser();
            if (user == null)
                return RedirectToPage("./Recipes2");
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
            if (user == null)
                return RedirectToPage("./Recipes2");
            Favorite FavQ = GetFavorites(user, RPUp);
            if (FavQ != null)
            {
                DelFavorite(FavQ);
            }
            
            return RedirectToPage("./Recipes2");
        }
    }
}

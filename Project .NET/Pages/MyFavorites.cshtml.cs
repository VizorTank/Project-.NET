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
    public class MyFavoritesModel : PageModel
    {
        public IList<Recipe> RP { get; set; }
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _cont;
        public MyFavoritesModel(ApplicationDbContext cont, UserManager<IdentityUser> userManager)
        {
            _cont = cont;
            _userManager = userManager;
        }
        public void OnGet()
        {

            IdentityUser user = GetUser();
            var FRQ = (from Favorite in _cont.Favorites where Favorite.user == user && Favorite.value == true select Favorite.recipes);
            RP = FRQ.ToList();
        }


        public IActionResult OnPostLikeAsync(int itemId, string userId)
        {

            Recipe RPUp = (from Recipes in _cont.Recipes where Recipes.Id == itemId orderby Recipes.date select Recipes).FirstOrDefault();
            IdentityUser user = GetUser();
            Like LQ = (from Like in _cont.Likes where Like.user == user && Like.recipes == RPUp select Like).ToList().ToList().LastOrDefault();
            if (LQ == null)
            {
                Like newlike = new Like(RPUp, user, 1);
                _cont.Likes.Add(newlike);
                RPUp.up_vote = RPUp.up_vote + 1;
                _cont.Recipes.Update(RPUp);
                _cont.SaveChanges();
            }
            else if (LQ.value < 1)
            {
                LQ.value = LQ.value + 1;
                _cont.Likes.Update(LQ);
                RPUp.up_vote = RPUp.up_vote + 1;
                _cont.Recipes.Update(RPUp);
                _cont.SaveChanges();
            }

            return RedirectToPage("./MyFavorites");
        }
        public IActionResult OnPostHateAsync(int itemId, string userId)
        {

            Recipe RPUp = (from Recipes in _cont.Recipes where Recipes.Id == itemId orderby Recipes.date select Recipes).FirstOrDefault();
            IdentityUser user = GetUser();
            Like LQ = (from Like in _cont.Likes where Like.user == user && Like.recipes == RPUp select Like).ToList().ToList().LastOrDefault();
            if (LQ == null)
            {
                Like newlike = new Like(RPUp, user, -1);
                _cont.Likes.Add(newlike);
                RPUp.up_vote = RPUp.up_vote - 1;
                _cont.Recipes.Update(RPUp);
                _cont.SaveChanges();
            }
            else if (LQ.value > -1)
            {
                LQ.value = LQ.value - 1;
                _cont.Likes.Update(LQ);
                RPUp.up_vote = RPUp.up_vote - 1;
                _cont.Recipes.Update(RPUp);
                _cont.SaveChanges();
            }

            return RedirectToPage("./MyFavorites");
        }
        public IActionResult OnPostFavoriteAsync(int itemId, string userId)
        {
            Recipe RPUp = (from Recipes in _cont.Recipes where Recipes.Id == itemId orderby Recipes.date select Recipes).FirstOrDefault();
            IdentityUser user = GetUser();
            Favorite FavQ = (from Favorite in _cont.Favorites where Favorite.user == user && Favorite.recipes == RPUp select Favorite).ToList().LastOrDefault();
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
            return RedirectToPage("./MyFavorites");
        }
        public IActionResult OnPostUnFavoriteAsync(int itemId, string userId)
        {
            Recipe RPUp = (from Recipes in _cont.Recipes where Recipes.Id == itemId orderby Recipes.date select Recipes).FirstOrDefault();
            IdentityUser user = GetUser();
            Favorite FavQ = (from Favorite in _cont.Favorites where Favorite.user == user && Favorite.recipes == RPUp select Favorite).ToList().LastOrDefault();
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
            return RedirectToPage("./MyFavorites");
        }
       

        public IdentityUser GetUser()
        {
            Task<IdentityUser> identityUser = _userManager.GetUserAsync(HttpContext.User);
            return identityUser.Result;
        }

        public string GetUsername2(int itemID)
        {
            Recipe RPDel = (from Recipes in _cont.Recipes where Recipes.Id == itemID orderby Recipes.date select Recipes).Include(i => i.User).FirstOrDefault();
            return RPDel.User.UserName;
        }

        public bool IsFavorite(int itemId)
        {
            IdentityUser user = GetUser();
            Recipe RPUp = (from Recipes in _cont.Recipes where Recipes.Id == itemId orderby Recipes.date select Recipes).FirstOrDefault();
            Favorite FavQ = (from Favorite in _cont.Favorites where Favorite.user == user && Favorite.recipes == RPUp select Favorite).ToList().LastOrDefault();
            if (FavQ == null)
            {
                return false;
            }
            if (FavQ.value == true)
                return true;
            else return false;
        }

    }
}

    


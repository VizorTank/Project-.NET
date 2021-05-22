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
    public class IndexModel : PageModel
    {

        public IList<Recipe> RP { get; set; }
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _cont;
        public IndexModel(ApplicationDbContext cont, UserManager<IdentityUser> userManager)
        {
            _cont = cont;
            _userManager = userManager;
        }
        public void OnGet()
        {
            var RPQuerry = (from Recipes in _cont.Recipes orderby Recipes.up_vote descending select Recipes).Include(u => u.User).Take(10);
            RP = RPQuerry.ToList();
        }

        public IActionResult OnPostDeleteAsync(int itemId, string userId)
        {
            if (userId != null && userId.CompareTo(GetUserId()) == 0)
                DelRep(itemId);
            return RedirectToPage("./Index");
        }



        public IActionResult OnPostEditAsync()
        {
            return RedirectToPage("./Index");
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

            return RedirectToPage("./Index");
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

            return RedirectToPage("./Index");
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
            return RedirectToPage("./Index");
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
            return RedirectToPage("./Index");
        }
        public void DelRep(int i)
        {
            Recipe RPDel = (from Recipes in _cont.Recipes where Recipes.Id == i orderby Recipes.date select Recipes).FirstOrDefault();
            if (RPDel != null)
            {
                bool checkvalue = false;

                while (true)
                {

                    Like LikeDEL = (from Like in _cont.Likes where Like.recipes == RPDel select Like).FirstOrDefault();
                    if (LikeDEL == null)
                    {
                        break;

                    }
                    _cont.Likes.Remove(LikeDEL);


                    _cont.SaveChanges();
                    checkvalue = true;

                }
                while (true)
                {

                    Favorite FavDEL = (from Favorite in _cont.Favorites where Favorite.recipes == RPDel select Favorite).FirstOrDefault();
                    if (FavDEL == null)
                    {
                        break;

                    }
                    _cont.Favorites.Remove(FavDEL);

                    _cont.SaveChanges();


                }
                _cont.Recipes.Remove(RPDel);
                
                    _cont.SaveChanges();
            }
        }

        public IdentityUser GetUser()
        {
            Task<IdentityUser> identityUser = _userManager.GetUserAsync(HttpContext.User);
            return identityUser.Result;
        }

        public string GetUserId()
        {
            return _userManager.GetUserId(HttpContext.User);
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


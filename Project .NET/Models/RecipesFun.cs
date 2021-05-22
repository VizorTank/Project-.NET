using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project_.NET.Data;

namespace Project_.NET.Models
{
    public class RecipesFun : PageModel
    {
        public IList<Recipe> RP { get; set; }
        public IList<Like> LDEL { get; set; }
        protected readonly UserManager<IdentityUser> _userManager;
        protected readonly ApplicationDbContext _cont;
        protected readonly string _NamePage;
        public RecipesFun(ApplicationDbContext cont, UserManager<IdentityUser> userManager, string NamePage)
        {
            _cont = cont;
            _userManager = userManager;
            _NamePage = NamePage;
        }

        public virtual void OnGet()
        { }

        public IActionResult OnPostDeleteAsync(int itemId, string userId)
        {
            if (userId != null && userId.CompareTo(GetUserId()) == 0)
                DelRep(itemId);
            return RedirectToPage(_NamePage);
        }



        public IActionResult OnPostEditAsync()
        {
            return RedirectToPage("./Recipes");
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

            return RedirectToPage(_NamePage);
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

            return RedirectToPage(_NamePage);
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
            return RedirectToPage(_NamePage);
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
            return RedirectToPage(_NamePage);
        }
        public void DelRep(int i)
        {
            Recipe RPDel = (from Recipes in _cont.Recipes where Recipes.Id == i orderby Recipes.date select Recipes).FirstOrDefault();
            if (RPDel != null)
            {


                while (true)
                {

                    Like LikeDEL = (from Like in _cont.Likes where Like.recipes == RPDel select Like).FirstOrDefault();
                    if (LikeDEL == null)
                    {
                        break;

                    }
                    _cont.Likes.Remove(LikeDEL);


                    _cont.SaveChanges();


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


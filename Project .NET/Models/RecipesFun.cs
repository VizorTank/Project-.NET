using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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



        public IActionResult OnPostEditAsync()
        {
            return RedirectToPage("./Recipes");
        }
        // Likes
        public IActionResult OnPostLikeAsync(int itemId, string userId)
        {

            Recipe RPUp = (from Recipes in _cont.Recipes where Recipes.Id == itemId orderby Recipes.date select Recipes).FirstOrDefault();
            ApplicationUser user = GetUser();
            Like LQ = (from Like in _cont.Likes where Like.User == user && Like.Recipe == RPUp select Like).ToList().ToList().LastOrDefault();
            if (LQ == null)
            {
                Like newlike = new Like(user, RPUp, 1);
                _cont.Likes.Add(newlike);
                RPUp.Votes = RPUp.Votes + 1;
                _cont.Recipes.Update(RPUp);
                _cont.SaveChanges();
            }
            else if (LQ.Value < 1)
            {
                LQ.Value = LQ.Value + 1;
                _cont.Likes.Update(LQ);
                RPUp.Votes = RPUp.Votes + 1;
                _cont.Recipes.Update(RPUp);
                _cont.SaveChanges();
            }

            return RedirectToPage(_NamePage);
        }
        public IActionResult OnPostHateAsync(int itemId, string userId)
        {

            Recipe RPUp = (from Recipes in _cont.Recipes where Recipes.Id == itemId orderby Recipes.date select Recipes).FirstOrDefault();
            ApplicationUser user = GetUser();
            Like LQ = (from Like in _cont.Likes where Like.User == user && Like.Recipe == RPUp select Like).ToList().ToList().LastOrDefault();
            if (LQ == null)
            {
                Like newlike = new Like(user, RPUp, - 1);
                _cont.Likes.Add(newlike);
                RPUp.Votes = RPUp.Votes - 1;
                _cont.Recipes.Update(RPUp);
                _cont.SaveChanges();
            }
            else if (LQ.Value > -1)
            {
                LQ.Value = LQ.Value - 1;
                _cont.Likes.Update(LQ);
                RPUp.Votes = RPUp.Votes - 1;
                _cont.Recipes.Update(RPUp);
                _cont.SaveChanges();
            }

            return RedirectToPage(_NamePage);
        }
        // Favorites
        public IActionResult OnPostFavoriteAsync(int itemId, string userId)
        {
            Recipe RPUp = (from Recipes in _cont.Recipes where Recipes.Id == itemId orderby Recipes.date select Recipes).FirstOrDefault();
            ApplicationUser user = GetUser();
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
        public void DelRep(int i)
        {
            Recipe RPDel = (from Recipes in _cont.Recipes where Recipes.Id == i orderby Recipes.date select Recipes).FirstOrDefault();
            if (RPDel != null)
            {


                while (true)
                {

                    Like LikeDEL = (from Like in _cont.Likes where Like.Recipe == RPDel select Like).FirstOrDefault();
                    if (LikeDEL == null)
                    {
                        break;

                    }
                    _cont.Likes.Remove(LikeDEL);


                    _cont.SaveChanges();


                }
                while (true)
                {

                    Favorite FavDEL = (from Favorite in _cont.Favorites where Favorite.Recipe == RPDel select Favorite).FirstOrDefault();
                    if (FavDEL == null)
                    {
                        break;

                    }
                    _cont.Favorites.Remove(FavDEL);

                    _cont.SaveChanges();


                }
                removeFoto(RPDel.Img);
                _cont.Recipes.Remove(RPDel);

                _cont.SaveChanges();
            }
        }
        public void removeFoto(string imgname)
        {
            if(imgname.Contains("../images/"))
            if(imgname!=null)
            {
                imgname.Replace("../images/", "");
                string filePath =Path.Combine(_webHostEnvironmen.WebRootPath,"images", imgname);
                System.IO.File.Delete(filePath);
            }
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
            IdentityUser user = GetUser();
            Recipe RPUp = (from Recipes in _cont.Recipes where Recipes.Id == itemId orderby Recipes.date select Recipes).FirstOrDefault();
            Favorite FavQ = (from Favorite in _cont.Favorites where Favorite.User == user && Favorite.Recipe == RPUp select Favorite).ToList().LastOrDefault();
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


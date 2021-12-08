using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project_.NET.Data;
using Project_.NET.Models;

namespace Project_.NET.Pages
{
    public class UserModel : RecipesFun
    {
        public IList<RecipeUser> RecipeUser { get; set; }
        public string Answer { get; set; }
        public string Username { get; set; }
        public UserModel(ApplicationDbContext cont, 
            UserManager<ApplicationUser> userManager, 
            IWebHostEnvironment webHostEnvironment) : base(cont, userManager, webHostEnvironment) { }
        public void OnGet(string username = null)
        {
            Username = username;
            ApplicationUser user = GetUserByName(Username);
            var RPQuerry = _cont.RecipeUsers
                    .Where(u => u.User.UserName == Username)
                    .Include(u => u.User)
                    .Include(r => r.Recipe).ThenInclude(u => u.RecipeUsers).ThenInclude(u => u.User)
                    .Select(r => r.Recipe).ToList();
            Recipes = RPQuerry.ToList();
        }
        public IActionResult OnPostFavoriteAutorAsync(string itemId, string getData)
        {
            return ChangeFavoriteAutor(itemId, getData);
        }
        public IActionResult OnPostUnFavoriteAutorAsync(string itemId, string getData)
        {
            return ChangeFavoriteAutor(itemId, getData);
        }
        public IActionResult ChangeFavoriteAutor(string itemId, string getData)
        {
            ApplicationUser user = GetUser();
            if (user == null)
                return Reload(getData);
            ApplicationUser autor = GetUserByName(itemId);

            FavouriteAutor favorite = GetFavouriteAutor(user, autor);
            if (favorite == null)
            {
                FavouriteAutor favi = new FavouriteAutor(user, autor);
                _cont.FavouriteAutors.Add(favi);
                _cont.SaveChanges();
            }
            else
            {
                _cont.FavouriteAutors.Remove(favorite);
                _cont.SaveChanges();
            }
            return Reload(getData);
        }

        public FavouriteAutor GetFavouriteAutor(ApplicationUser user, ApplicationUser autor)
        {
            return (from FavouriteAutor
                    in _cont.FavouriteAutors
                    where FavouriteAutor.User == user && FavouriteAutor.Autor == autor
                    select FavouriteAutor).ToList().LastOrDefault();
        }

        public bool IsFavoriteAutor(string autorname)
        {
            ApplicationUser user = GetUser();
            ApplicationUser autor = GetUserByName(autorname);
            FavouriteAutor favorite = GetFavouriteAutor(user, autor);
            if (favorite == null)
                return false;
            else
                return true;
        }
    }
}



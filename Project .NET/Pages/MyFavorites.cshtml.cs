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
    public class MyFavoritesModel : RecipesFun
    {

        public MyFavoritesModel(ApplicationDbContext cont, UserManager<IdentityUser> userManager) : base(cont, userManager, "./MyFavorites")
        { }
        public override void OnGet()
        {

            IdentityUser user = GetUser();
            var FRQ = (from Favorite in _cont.Favorites where Favorite.User == user && Favorite.value == true select Favorite.Recipes);
            RP = FRQ.ToList();
        }


        public string GetUsername(int itemID)
        {
            Recipe RPDel = (from Recipes in _cont.Recipes where Recipes.Id == itemID orderby Recipes.date select Recipes).Include(i => i.User).FirstOrDefault();
            return RPDel.User.UserName;
        }


    }
}

    


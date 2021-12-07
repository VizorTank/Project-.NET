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
    [Authorize]
    public class MyFavoritesModel : RecipesFun
    {

        public MyFavoritesModel(ApplicationDbContext cont, 
            UserManager<ApplicationUser> userManager, 
            IWebHostEnvironment webHostEnvironment) : base(cont, userManager, webHostEnvironment) { }
        public void OnGet()
        {
            IdentityUser user = GetUser();
            var RPQuerry = (from Favorites 
                            in _cont.Favorites 
                            where Favorites.User == user 
                            && Favorites.value == true 
                            select Favorites).Include(r => r.Recipe).ThenInclude(ru => ru.RecipeUsers).ThenInclude(r => r.User).Select(f => f.Recipe);
            Recipes = RPQuerry.ToList();
        }

        public string GetUsername(int itemID)
        {
            Recipe RPDel = (from Recipes 
                            in _cont.Recipes 
                            where Recipes.Id == itemID 
                            orderby Recipes.date descending
                            select Recipes).Include(ru => ru.RecipeUsers).ThenInclude(i => i.User).FirstOrDefault();
            // TODO: User list
            return RPDel.RecipeUsers.FirstOrDefault().User.UserName;
        }
    }
}

    


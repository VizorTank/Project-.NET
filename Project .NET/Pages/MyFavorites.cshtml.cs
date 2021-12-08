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
                            in _cont.Favourites 
                            where Favorites.User == user 
                            && Favorites.value == true 
                            select Favorites).Include(r => r.Recipe).ThenInclude(ru => ru.RecipeUsers).ThenInclude(r => r.User).Select(f => f.Recipe);
            Recipes = RPQuerry.ToList();
        }
    }
}

    


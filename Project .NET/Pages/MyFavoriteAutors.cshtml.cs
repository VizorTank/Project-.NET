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
    public class MyFavoriteAutorsModel : RecipesFun
    {
        public IList<ApplicationUser> Autors;
        public MyFavoriteAutorsModel(ApplicationDbContext cont, 
            UserManager<ApplicationUser> userManager, 
            IWebHostEnvironment webHostEnvironment) : base(cont, userManager, webHostEnvironment) { }
        public void OnGet()
        {
            IdentityUser user = GetUser();
            var RPQuerry = (from FavoriteAutors
                            in _cont.FavouriteAutors 
                            where FavoriteAutors.User == user 
                            select FavoriteAutors).Include(r => r.Autor).Select(f => f.Autor);
            Autors = RPQuerry.ToList();
            //Recipes = RPQuerry.ToList();
        }
    }
}

    


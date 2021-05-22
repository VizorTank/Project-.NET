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
    public class UserAddedModel : RecipesFun
    {

        public UserAddedModel(ApplicationDbContext cont, UserManager<IdentityUser> userManager) : base(cont, userManager, "./UserAdded")
        { }
        public override void OnGet()
        {
            IdentityUser user = GetUser();
            var RPQuerry = (from Recipes in _cont.Recipes where Recipes.User==user orderby Recipes.date descending select Recipes).Include(u => u.User);
            RP = RPQuerry.ToList();
        }


    }

}


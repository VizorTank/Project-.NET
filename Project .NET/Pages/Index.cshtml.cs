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
using Project_.NET.Secrvices;

namespace Project_.NET.Pages
{
    public class IndexModel : RecipesFun
    {


        public IndexModel(ApplicationDbContext cont, UserManager<ApplicationUser> userManager): base (cont , userManager, "./Index")
        {

        }
        public override void OnGet()
        {
            var RPQuerry = (from Recipes in _cont.Recipes orderby Recipes.Votes descending select Recipes).Include(u => u.User).Take(10);
            RP = RPQuerry.ToList();
        }



    }

}


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
namespace Project_.NET.Pages.Shared
{
    public class RecipesModel : PageModel
    {
        public IList<Recipe> RP { get; set; }
        private readonly ApplicationDbContext _cont;
        private readonly UserManager<IdentityUser> _userManager;
        public RecipesModel(ApplicationDbContext cont, UserManager<IdentityUser> userManager)
        {
            _cont = cont;
            _userManager = userManager;
        }
        public void OnGet()
        {
            //var RPQuerry = (from Recipes in _cont.Recipes orderby Recipes.date descending select Recipes).;
            var RPQuerry = _cont.Recipes.Include(i => i.User);
            RP = RPQuerry.ToList();
        }

        public string GetUserName(string userId)
        {
            var username = _userManager.FindByIdAsync(userId).Result;
            if (username != null)
                return username.UserName;
            return "Anonim";
        }
    }
}

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
       
        public IList<Recipes> RP { get; set; }
        private readonly RecipesContext _cont;
        private readonly UserManager<IdentityUser> _userManager;
        public RecipesModel(RecipesContext cont, UserManager<IdentityUser> userManager)
        {
            _cont = cont;
            _userManager = userManager;
        }
        public void OnGet()
        {
            var RPQuerry = (from Recipes in _cont.Recipes orderby Recipes.date descending select Recipes);
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

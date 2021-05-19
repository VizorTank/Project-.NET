using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_.NET.Data;
using Project_.NET.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
namespace Project_.NET.Pages.Shared
{
    public class Recipes2Model : PageModel
    {
        public IList<Recipes> RP { get; set; }
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RecipesContext _cont;
        public Recipes2Model(RecipesContext cont, UserManager<IdentityUser> userManager)
        {
            _cont = cont;
            _userManager = userManager;
        }
        public void OnGet()
        {
            var RPQuerry = (from Recipes in _cont.Recipes orderby Recipes.date descending select Recipes);
            RP = RPQuerry.ToList();
        }


        public bool UserProperty(string iduser)
        {
            if (iduser == _userManager.GetUserId(HttpContext.User))
                return true;
            else
                return false;
        }
    }
}

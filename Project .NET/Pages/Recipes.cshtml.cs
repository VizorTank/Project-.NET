using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_.NET.Data;
using Project_.NET.Models;
namespace Project_.NET.Pages.Shared
{
    public class RecipesModel : PageModel
    {
        public IList<Recipes> RP { get; set; }
        private readonly RecipesContext _cont;
        public RecipesModel(RecipesContext cont)
        {
            _cont = cont;
        }
        public void OnGet()
        {
            var RPQuerry = (from Recipes in _cont.Recipes orderby Recipes.date descending select Recipes);
            RP = RPQuerry.ToList();
        }
    }
}

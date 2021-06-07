using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_.NET.Models;

namespace Project_.NET.Pages
{
    [Authorize]
    public class EditRecipeModel : PageModel
    {
        [BindProperty]
        public string RecipeId { get; set; }
        [BindProperty]
        public string NewDesc { get; set; }
        [BindProperty]
        public string NewName { get; set; }
        public IList<Category> Categories { get; set; }
        
        public void OnGet()
        {
            
        }

        public void OnPostCName()
        {

        }
        public void OnPostCDesc()
        {

        }

        public void OnPostACat()
        {

        }

    }
}

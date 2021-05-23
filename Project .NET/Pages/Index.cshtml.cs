using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Project_.NET.Data;


using Project_.NET.Models;

namespace Project_.NET.Pages
{
    public class IndexModel : PageModel
    {
      
        public string SearchString;

        public IList<Recipes> Receipe { get; set; }

        private readonly RecipesContext _recipesContext;
        private readonly ILogger<IndexModel> _logger;
        [BindProperty]
        public Recipes Recipes { get; set; }

   
        public IndexModel(ILogger<IndexModel> logger,RecipesContext recipesContext)
        {
            _logger = logger;
            _recipesContext = recipesContext;
        }

        
        
       
           //var Rp = (from receipe in _recipesContext.Recipes where receipe.Name == SearchString || receipe.User_Id == SearchString orderby receipe.date descending select receipe);

            //Receipe = Rp.ToList();
        

        public async Task OnGetAsync()
        {
            var receipes = (from m in _recipesContext.Recipes 
                         select m);

            if (!string.IsNullOrEmpty(SearchString))
            {
                receipes = (from m in _recipesContext.Recipes where m.Name==SearchString || m.User_Id==SearchString orderby m.date descending select m);
            }

            

            Receipe = await receipes.ToListAsync();
           
        }
        /*
        [HttpPost]
        public ActionResult Index(string ModelZnajdz)
        {
            var receipes = from i in _recipesContext.Recipes
                       select i;
            //jeśli coś przesłano, to wyszukaj po tym
            if (!String.IsNullOrEmpty(Recipes))
            {
                receipes = from i in _recipesContext.Recipes
                       where i.Name.Equals(ModelZnajdz)
                       select i;
            }

            return View(receipes.ToList());
        }
        [HttpPost]
        public async Task<IActionResult> Index1(string searchString)
        {
            var movies = from m in _recipesContext.Recipes
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Name.Contains(searchString));
            }

            return View(await movies.ToListAsync());
        }*/

    }
}

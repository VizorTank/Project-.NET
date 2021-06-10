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
        [BindProperty(SupportsGet = true)]
        public string searchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string authorString { get; set; }

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
            
            if (!string.IsNullOrEmpty(searchString))
            {
                receipes = (from m in _recipesContext.Recipes where m.Name==searchString || m.User_Id==searchString orderby m.date descending select m);
            }

            if (!string.IsNullOrEmpty(authorString))
            {
                receipes = (from m in _recipesContext.Recipes where  m.User_Id == searchString orderby m.date descending select m);
            }

            Receipe = await receipes.ToListAsync();
           
        }
       

    }
}

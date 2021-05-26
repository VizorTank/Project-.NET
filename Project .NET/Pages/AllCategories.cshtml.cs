using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_.NET.Data;
using Project_.NET.Models;

namespace Project_.NET.Pages
{
    [Authorize]
    public class AllCategoriesModel : PageModel
    {
        private readonly ApplicationDbContext _cont;
        public IList<Category> Categories { get; set; }
        public AllCategoriesModel(ApplicationDbContext cont)
        {
            _cont = cont;
        }
        public void OnGet()
        {
            Categories = (from Category in _cont.Categories select Category).ToList();
        }
        public IActionResult OnPostDeleteAsync(string itemId)
        {
            Category category = (from Category in _cont.Categories where Category.Name == itemId select Category).FirstOrDefault();
            if (category != null)
            {
                _cont.Categories.Remove(category);
                _cont.SaveChanges();
            }
            return RedirectToPage("./AllCategories");
        }
    }
}

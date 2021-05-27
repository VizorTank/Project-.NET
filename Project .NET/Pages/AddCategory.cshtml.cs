using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project_.NET.Data;
using Project_.NET.Models;

namespace Project_.NET.Pages
{
    [Authorize]
    public class AddCategoryModel : PageModel
    {
        private readonly ApplicationDbContext _cont;
        private readonly UserManager<ApplicationUser> _userManager;
        public string ErrorMessage { get; set; }

        [BindProperty, Required(ErrorMessage = "Pole Nazwa jest wymagane"), MaxLength(50, ErrorMessage = "Miej ni� 50 znak�w")]
        public string CategoryName { get; set; }

        public void OnGet() { }
        public AddCategoryModel(ApplicationDbContext cont, UserManager<ApplicationUser> userManager)
        {
            _cont = cont;
            _userManager = userManager;
        }
        public IActionResult OnPost()
        {
            ErrorMessage = null;
            if (ModelState.IsValid)
            {
                if (_cont.Categories.Find(CategoryName) == null)
                {
                    Category category = new Category(CategoryName);
                    _cont.Categories.Add(category);
                    _cont.SaveChanges();
                    return RedirectToPage("./AllCategories");
                }
                ErrorMessage = "Kategoria ju� istnieje";
            }
            return Page();
        }
    }
}

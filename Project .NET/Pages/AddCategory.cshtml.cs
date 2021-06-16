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

        [BindProperty, Required(ErrorMessage = "Pole Nazwa jest wymagane"), MaxLength(50, ErrorMessage = "Miej ni¿ 50 znaków")]
        public string CategoryName { get; set; }
        [BindProperty, MaxLength(2500, ErrorMessage = "Miej ni¿ 2500 znaków")]
        public string Description { get; set; }

        public void OnGet() { }
        public AddCategoryModel(ApplicationDbContext cont, UserManager<ApplicationUser> userManager)
        {
            _cont = cont;
            _userManager = userManager;
        }
        public IActionResult OnPostAdd()
        {
            ErrorMessage = null;
            if (ModelState.IsValid)
            {
                if (_cont.Categories.Find(CategoryName) == null)
                {
                    Category category = new Category(CategoryName, Description);
                    _cont.Categories.Add(category);
                    _cont.SaveChanges();
                    return RedirectToPage("./AllCategories");
                }
                else
                {
                    Category category = _cont.Categories.Find(CategoryName);
                    category.Description = Description;
                    _cont.SaveChanges();
                    return RedirectToPage("./AllCategories");
                }
            }
            return Page();
        }

        public void OnPostEdit(string itemId)
        {
            CategoryName = itemId;
            Category category = _cont.Categories.Find(CategoryName);
            Description = category.Description;
        }
    }
}

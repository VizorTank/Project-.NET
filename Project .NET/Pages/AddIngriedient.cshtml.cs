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
    public class AddIngiedientModel : PageModel
    {
        private readonly ApplicationDbContext _cont;
        private readonly UserManager<ApplicationUser> _userManager;
        public string ErrorMessage { get; set; }

        [BindProperty, Required(ErrorMessage = "Pole Nazwa jest wymagane"), MaxLength(50, ErrorMessage = "Miej ni¿ 50 znaków")]
        public string IngriedientName { get; set; }
        [BindProperty, MaxLength(2500, ErrorMessage = "Miej ni¿ 2500 znaków")]
        public string Description { get; set; }

        public void OnGet() { }
        public AddIngiedientModel(ApplicationDbContext cont, UserManager<ApplicationUser> userManager)
        {
            _cont = cont;
            _userManager = userManager;
        }
        public IActionResult OnPostAdd()
        {
            ErrorMessage = null;
            if (ModelState.IsValid)
            {
                if (_cont.Ingredients.Where(r => r.Name == IngriedientName).FirstOrDefault() == null)
                {
                    Ingriedient ingredient = new Ingriedient(IngriedientName);
                    _cont.Ingredients.Add(ingredient);
                    _cont.SaveChanges();
                    return RedirectToPage("./AllIngriedients");
                }

            }
            return Page();
        }

       // public void OnPostEdit(string itemId)
       // {
       //     CategoryName = itemId;
       //     Category category = _cont.Categories.Where(r => r.Name == CategoryName).FirstOrDefault();
       //     Description = category.Description;
       // }
    }
}

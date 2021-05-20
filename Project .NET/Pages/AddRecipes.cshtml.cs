using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Project_.NET.Data;
using Project_.NET.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Project_.NET.Pages
{
    [Authorize]
    public class AddRecipesModel : PageModel
    {
        private readonly ApplicationDbContext _cont;
        private readonly UserManager<ApplicationUser> _userManager;

        [BindProperty, Required(ErrorMessage = "Pole Nazwa jest wymagane "), MaxLength(50, ErrorMessage = "Miej ni¿ 50 znaków")]
        public string AddName { get; set; }
        [BindProperty, Required(ErrorMessage = "Pole Sk³adniki jest wymagane "), MaxLength(255, ErrorMessage = "Miej ni¿ 255 znaków")]
        public string AddIngs { get; set; }

        [BindProperty, Required(ErrorMessage = "Pole Opis Przygotowania jest wymagane "), MaxLength(255, ErrorMessage = "Miej ni¿ 255 znaków")]
        public string AddDesc { get; set; }
        public string AddImg { get; set; }

        public void OnGet() { }
        public AddRecipesModel(ApplicationDbContext cont, UserManager<ApplicationUser> userManager)
        {
            _cont = cont;
            _userManager = userManager;
        }
        public IActionResult OnPost()
        {
            if(ModelState.IsValid)
            {
                Recipe rece = new Recipe(GetUser(), AddName, AddIngs, AddDesc, AddImg);
                _cont.Recipes.Add(rece);
                _cont.SaveChanges();
                return RedirectToPage("./Recipes");
            }
            return Page();
        }
        public ApplicationUser GetUser()
        {
            Task<ApplicationUser> identityUser = _userManager.GetUserAsync(HttpContext.User);
            return identityUser.Result;
        }
    }
}

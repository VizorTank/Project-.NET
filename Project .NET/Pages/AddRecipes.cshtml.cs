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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Project_.NET.Pages
{
    [Authorize]
    public class AddRecipesModel : PageModel
    {
        private readonly ApplicationDbContext _cont;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty, Required(ErrorMessage = "Pole Nazwa jest wymagane "), MaxLength(50, ErrorMessage = "Miej ni¿ 50 znaków")]
        public string AddName { get; set; }
        [BindProperty, Required(ErrorMessage = "Pole Sk³adniki jest wymagane "), MaxLength(255, ErrorMessage = "Miej ni¿ 255 znaków")]
        public string AddIngs { get; set; }

        [BindProperty, Required(ErrorMessage = "Pole Opis Przygotowania jest wymagane "), MaxLength(255, ErrorMessage = "Miej ni¿ 255 znaków")]
        public string AddDesc { get; set; }
        [BindProperty]
        public string AddImg { get; set; }
        [BindProperty]
        public IFormFile Foto { get; set; }
        public IList<Category> Categories { get; set; }
        [BindProperty]
        public IList<string> ChosenCategories { get; set; }

        public void OnGet() 
        {
            Categories = (from Category in _cont.Categories select Category).ToList();
        }
        public AddRecipesModel(ApplicationDbContext cont, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _cont = cont;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if(Foto != null)
                {
                    AddImg = "../images/" + PrecessFoto();
                }
                Recipe rece = new Recipe(GetUser(), AddName, AddIngs, AddDesc, AddImg);
                foreach (string item in ChosenCategories)
                {
                    AddRecipeCategory(rece, (from Category in _cont.Categories where Category.Name == item select Category).SingleOrDefault());
                }
                _cont.Recipes.Add(rece);
                _cont.SaveChanges();
                return RedirectToPage("./AllRecipes");
            }
            return Page();
        }

        public void AddRecipeCategory(Recipe recipe, Category category)
        {
            _cont.RecipeCategories.Add(new RecipeCategory(recipe, category));
        }

        public ApplicationUser GetUser()
        {
            Task<ApplicationUser> identityUser = _userManager.GetUserAsync(HttpContext.User);
            return identityUser.Result;
        }
        private string PrecessFoto()
        {
            string uniFileName = null;
            
            if (Foto != null)
            {
                string uploadsfolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniFileName = Guid.NewGuid().ToString() + "_" + Foto.FileName;
                string filePath = (Path.Combine(uploadsfolder, uniFileName));
                using (var fileStream = new FileStream(filePath, FileMode.Create)) 
                {
                    Foto.CopyTo(fileStream);
                }
            }
            return uniFileName;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Project_.NET.Data;
using Project_.NET.Models;

namespace Project_.NET.Pages
{
    public class AddRecipesModel : PageModel
    {
        private readonly RecipesData _cont;
        [BindProperty]
        public int AddUser_id { get; set; }

        [BindProperty,Required(ErrorMessage = "Pole Nazwa jest wymagane "),MaxLength(50, ErrorMessage = "Miej ni¿ 50 znaków")]
        public string AddName { get; set; }
        [BindProperty, Required(ErrorMessage = "Pole Sk³adniki jest wymagane "), MaxLength(255, ErrorMessage = "Miej ni¿ 255 znaków")]
        public string AddIngs { get; set; }

        [BindProperty, Required(ErrorMessage = "Pole Opis Przygotowania jest wymagane "), MaxLength(255, ErrorMessage = "Miej ni¿ 255 znaków")]
        public string AddDesc { get; set; }
        public string AddImg { get; set; }
        public void OnGet()
        {
            
        }
        public AddRecipesModel(RecipesData cont)
        {
            _cont = cont;
        }
        public void OnPost()
        {
            if(ModelState.IsValid)
            {
                Recipes rece = new Recipes(AddUser_id, AddName, AddIngs, AddDesc, AddImg);
                _cont.Recipes.Add(rece);
                _cont.SaveChanges();
            }
        }
    }
}

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
    public class AllIngriedientsModel : PageModel
    {
        private readonly ApplicationDbContext _cont;
        public IList<Ingriedient> Ingriedients { get; set; }
        public AllIngriedientsModel(ApplicationDbContext cont)
        {
            _cont = cont;
        }
        public void OnGet()
        {
            Ingriedients = (from Ingriedient in _cont.Ingredients select Ingriedient).ToList();
        }
        public IActionResult OnPostDeleteAsync(string itemId)
        {
            Ingriedient ingriedient = (from Ingriedient in _cont.Ingredients where Ingriedient.Name == itemId select Ingriedient).FirstOrDefault();
            if (ingriedient != null)
            {
                _cont.Ingredients.Remove(ingriedient);
                _cont.SaveChanges();
            }
            return RedirectToPage("./AllIngriedients");
        }
    }
}

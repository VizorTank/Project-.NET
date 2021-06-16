using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project_.NET.Data;
using Project_.NET.Models;

namespace Project_.NET.Pages
{
    public class UserModel : RecipesFun
    {
        public string Answer { get; set; }
        public UserModel(ApplicationDbContext cont, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment) : base(cont, userManager, "./UserAdded", webHostEnvironment)
        { }
        public void OnGet(string username)
        {
            if (username == null || username == "")
                Answer = "Nie znaleziono u�ytkownika";
            Answer = HttpContext.Request.Path;
            ApplicationUser user = GetUserByName(username);
            var RPQuerry = (from Recipes in _cont.Recipes where Recipes.User==user orderby Recipes.date descending select Recipes).Include(u => u.User);
            Recipes = RPQuerry.ToList();
        }
    }

}

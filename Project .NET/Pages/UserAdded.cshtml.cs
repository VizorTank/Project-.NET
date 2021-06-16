using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_.NET.Data;
using Project_.NET.Models;

namespace Project_.NET.Pages
{
    [Authorize]
    public class UserAddedModel : RecipesFun
    {
        public UserAddedModel(ApplicationDbContext cont,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment webHostEnvironment) : base(cont, userManager, "./User", webHostEnvironment) { }
        public IActionResult OnGet()
        {
            return Redirect(UserPage(GetUser().UserName));
        }
    }
}

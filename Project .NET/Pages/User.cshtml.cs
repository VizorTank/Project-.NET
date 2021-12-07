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
        public IList<RecipeUser> RecipeUser { get; set; }
        public string Answer { get; set; }
        public string Username { get; set; }
        public UserModel(ApplicationDbContext cont, 
            UserManager<ApplicationUser> userManager, 
            IWebHostEnvironment webHostEnvironment) : base(cont, userManager, webHostEnvironment) { }
        public void OnGet(string username = null)
        {
            Username = username;
            ApplicationUser user = GetUserByName(Username);
            //var RPQuerry = (from Recipes 
            //in _cont.Recipes 
            //// TODO: User search
            ////where Recipes.User == user 
            //orderby Recipes.date descending
            //select Recipes).Include(r => r.RecipeUsers).ThenInclude(u => u.User);
            var RPQuerry = (from RecipeUser in _cont.RecipeUsers where RecipeUser.User == user select RecipeUser).Include(r=>r.Recipe).Include(u => u.User);
           // var RPQuerry = (from Recipes in _cont.Recipes where Recipes.RecipeUsers == (from RecipeUser in _cont.RecipeUsers where RecipeUser.User==user select RecipeUser) select Recipes).Include(r => r.RecipeUsers).ThenInclude(u => u.User);
            RecipeUser = RPQuerry.ToList();
            foreach(RecipeUser item in RecipeUser)
            {
                Recipes.Add(item.Recipe);
            }
            
        }
        }
    }



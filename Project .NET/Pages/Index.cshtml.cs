using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project_.NET.Data;
using Project_.NET.Models;
using Project_.NET.Secrvices;

namespace Project_.NET.Pages
{
    public class IndexModel : Recipes2Model
    {
        public IndexModel(ApplicationDbContext cont, UserManager<ApplicationUser> userManager) : base(cont, userManager) { }
    }
}

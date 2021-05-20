using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_.NET.Data;
using Project_.NET.Models;
namespace Project_.NET.Pages.Shared
{
    public class Recipes2Model : PageModel
    {
        public IList<Recipes> RP { get; set; }
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _cont;
        public Recipes2Model(ApplicationDbContext cont, UserManager<IdentityUser> userManager)
        {
            _cont = cont;
            _userManager = userManager;
        }
        public void OnGet()
        {
            var RPQuerry = (from Recipes in _cont.Recipes orderby Recipes.date descending select Recipes);
            RP = RPQuerry.ToList();
        }

        public IActionResult OnPostDeleteAsync(int itemId, string userId)
        {
            if (userId != null && userId.CompareTo(GetUserId()) == 0)
            DelRep(itemId);
            return  RedirectToPage("./Recipes2");
        }
        public async Task<ActionResult> OnPostEditAsync()
        {
            return  RedirectToPage("./Recipes");
        }
        public async Task<ActionResult> OnPostLikeAsync(int itemId, string userId)
        {
            Recipes RPUp = (from Recipes in _cont.Recipes where Recipes.Id == itemId orderby Recipes.date select Recipes).FirstOrDefault();
            if (RPUp != null)
            {
                RPUp.up_vote = RPUp.up_vote + 1;
                _cont.Recipes.Update(RPUp);
                _cont.SaveChanges();
            }
            return RedirectToPage("./Recipes2");
        }
        public async Task<ActionResult> OnPostHateAsync(int itemId, string userId)
        {
            Recipes RPUp = (from Recipes in _cont.Recipes where Recipes.Id == itemId orderby Recipes.date select Recipes).FirstOrDefault();
            if (RPUp != null)
            {
                RPUp.up_vote = RPUp.up_vote - 1;
                _cont.Recipes.Update(RPUp);
                _cont.SaveChanges();
            }
            return RedirectToPage("./Recipes2");
        }
        public async Task<ActionResult> OnPostFavoriteAsync()
        {
            return RedirectToPage("./Index");
        }
        public void DelRep(int i)
        {
            Recipes RPDel = (from Recipes in _cont.Recipes where Recipes.Id == i orderby Recipes.date select Recipes).FirstOrDefault();
            if(RPDel != null)
            {
                _cont.Recipes.Remove(RPDel);
                _cont.SaveChanges();
            }
        }
        public bool UserProperty(string iduser)
        {
            if (iduser == _userManager.GetUserId(HttpContext.User))
                return true;
            else
                return false;
        }
        public string GetUserName(string userId)
        {
            var username = _userManager.FindByIdAsync(userId).Result;
            if (username != null)
                return username.UserName;
            return "Anonim";
        }
        public string GetUserId()
        {
            return _userManager.GetUserId(HttpContext.User);
        }
       
    }
}

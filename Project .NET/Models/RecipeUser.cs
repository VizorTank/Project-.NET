using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_.NET.Models
{
    public class RecipeUser
    {
        public int RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public RecipeUser() { }
        public RecipeUser(ApplicationUser user, Recipe recipe)
        {
            User = user;
            Recipe = recipe;
        }
    }
}

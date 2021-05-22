using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_.NET.Models
{
    public class Like
    {
        public int RecipeId { get; set; }
        public string UserId { get; set; }
        public Recipe Recipe { get; set; }
        public ApplicationUser User { get; set; }
        public int Value { get; set; }

        public Like() { }
        public Like(ApplicationUser user, Recipe recipe, int value)
        {
            User = user;
            Recipe = recipe;
            Value = value;
        }
    }
}

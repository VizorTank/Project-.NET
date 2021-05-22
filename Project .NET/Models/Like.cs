using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project_.NET.Models
{
    public class Like
    {
        public int RecipeId { get; set; }
        public string UserId { get; set; }

        public Recipe Recipe { get; set; }

        public IdentityUser User { get; set; }
        public int value { get; set; }

        public Like()
        { }
        public Like(Recipe _recipes, IdentityUser _user, int _value)
        {
            Recipe = _recipes;
            User = _user;
            value = _value;
        }
    }
}

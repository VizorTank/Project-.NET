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
    public class Favorite
    {
        public int RecipeId { get; set; }
        public string UserId { get; set; }

        public Recipe Recipe { get; set; }
        
        public ApplicationUser User { get; set; }
        public bool value { get; set; }

        public Favorite()
        { }
        public Favorite(Recipe _recipes, ApplicationUser _user, bool _value)
        {
            Recipe = _recipes;
            User = _user;
            value = _value;
        }
    }
}

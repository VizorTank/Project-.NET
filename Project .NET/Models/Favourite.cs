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
    public class Favourite
    {
        public int RecipeId { get; set; }
        public string UserId { get; set; }

        public virtual Recipe Recipe { get; set; }
        
        public virtual ApplicationUser User { get; set; }
        public bool value { get; set; }

        public Favourite()
        { }
        public Favourite(Recipe _recipes, ApplicationUser _user, bool _value)
        {
            Recipe = _recipes;
            User = _user;
            value = _value;
        }
    }
}

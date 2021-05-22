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

        public Favorite() { }
        public Favorite(ApplicationUser applicationUser, Recipe recipe)
        {
            User = applicationUser;
            Recipe = recipe;
        }

        public Favorite(ApplicationUser applicationUser, Recipe recipe, bool value) : this(applicationUser, recipe) { }
    }
}

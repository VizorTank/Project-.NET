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
        public int Id { get; set; }

        public Recipe recipes { get; set; }
        
        public IdentityUser user { get; set; }
        public bool value { get; set; }

        public Favorite()
        { }
        public Favorite(Recipe _recipes, IdentityUser _user, bool _value)
        {
            recipes = _recipes;
            user = _user;
            value = _value;
        }
    }
}

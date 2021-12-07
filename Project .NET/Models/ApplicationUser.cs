using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_.NET.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Favourite> Favorites { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<RecipeUser> RecipeUsers { get; set; }
        public virtual ICollection<FavouriteAutor> FavouriteAutors { get; set; }
        public virtual ICollection<FavouriteAutor> FavouritedByUsers { get; set; }
    }
}

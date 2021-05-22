using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_.NET.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Favorite> Favorites { get; set; }
        public ICollection<Like> Likes { get; set; }
    }
}

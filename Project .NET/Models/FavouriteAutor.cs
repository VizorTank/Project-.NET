using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_.NET.Models
{
    public class FavouriteAutor
    {
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string AutorId { get; set; }
        public virtual ApplicationUser Autor { get; set; }

        public FavouriteAutor() { }
        public FavouriteAutor(ApplicationUser user, ApplicationUser autor)
        {
            User = user;
            Autor = autor;
        }

    }
}

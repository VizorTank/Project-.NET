using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace Project_.NET.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        public IdentityUser User { get; set; }

        public string Name { get; set; }
        public string Ings { get; set; }
        public string Desc { get; set; }
        public int Votes { get; set; }

        public string date { get; set; }
        public string Img { get; set; }

        public ICollection<Favorite> Favorites { get; set; }
        public ICollection<Like> Likes { get; set; }
        public Recipe(IdentityUser _User, string _Name, string _Ings, string _Desc, string _Img )
        {
            User = _User;
            Name = _Name;
            Ings = _Ings;
            Desc = _Desc;
            Img = _Img;
            Votes = 0;
            date = DateTime.Now.ToString();

        }
        public Recipe()
        { }

    }
}

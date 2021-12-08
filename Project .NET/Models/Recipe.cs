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
        //public ApplicationUser User { get; set; }
        public string Name { get; set; }
        public string Ings { get; set; }
        public string Desc { get; set; }
        public int Votes { get; set; }
        public string date { get; set; }
        public string Img { get; set; }
        public virtual ICollection<Favourite> Favorites { get; set; }
        public virtual ICollection<Like> Likes { get; set;  }
        public virtual ICollection<RecipeCategory> RecipeCategories { get; set; }
        public virtual ICollection<RecipeUser> RecipeUsers { get; set; }
        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }
        public virtual ICollection<Image> Images { get; set; }

        public Recipe()
        { }

        public Recipe(string _Name, string _Ings, string _Desc, string _Img )
        {
            Name = _Name;
            Ings = _Ings;
            Desc = _Desc;
            Img = _Img;
            Votes = 0;
            date = DateTime.Now.ToString();

        }
        public Recipe(string _Name, string _Desc, string _Img)
        {
            Name = _Name;
            Desc = _Desc;
            Img = _Img;
            Votes = 0;
            date = DateTime.Now.ToString();

        }
    }
}

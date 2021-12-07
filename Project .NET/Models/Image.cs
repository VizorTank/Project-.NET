using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_.NET.Models
{
    public class Image
    {
        public int ImageId { get; set; }
        public int RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }
        
        public Image() { }
        public Image(Recipe recipe)
        {
            Recipe = recipe;
        }
    }
}

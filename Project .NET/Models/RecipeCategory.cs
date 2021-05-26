using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_.NET.Models
{
    public class RecipeCategory
    {
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        public string CategoryId { get; set; }
        public Category Category { get; set; }

        public RecipeCategory() { }

    }
}

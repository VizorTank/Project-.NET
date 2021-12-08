using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_.NET.Models
{
    public class RecipeIngredient
    {
        public int RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }
        public int IngredientId { get; set; }
        public virtual Ingriedient Ingredient { get; set; }
        public float Amount { get; set; }

        public RecipeIngredient() { }
        public RecipeIngredient(Recipe recipe, Ingriedient ingredient)
        {
            Recipe = recipe;
            Ingredient = ingredient;
        }
    }
}

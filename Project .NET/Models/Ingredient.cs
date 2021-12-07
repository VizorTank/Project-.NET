using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_.NET.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public string Name { get; set; }
        public IngredientAmountType Type { get; set; }
        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }
    }

    public enum IngredientAmountType { Mass, Volume, Quantity }
}

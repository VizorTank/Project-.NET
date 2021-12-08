using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_.NET.Models
{
    public class Ingriedient
    {
        public int IngredientId { get; set; }
        public string Name { get; set; }
        public IngredientAmountType Type { get; set; }
        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }

        public Ingriedient() { }
        public Ingriedient(string name)
        {
            Name = name;
        }


    }

    public enum IngredientAmountType { Mass, Volume, Quantity }
}

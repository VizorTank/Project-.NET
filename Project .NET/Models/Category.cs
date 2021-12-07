using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project_.NET.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<RecipeCategory> RecipeCategories { get; set; }

        public Category() { }
        public Category(string name)
        {
            Name = name;
        }
        public Category(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}

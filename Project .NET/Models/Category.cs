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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<RecipeCategory> RecipeCategories { get; set; }

        public Category() { }
        public Category(string name)
        {
            Name = name;
        }
    }
}

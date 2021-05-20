using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_.NET.Models
{
    public class Category
    {
        [Key]
        public string CategoryName { get; set; }
    }
}

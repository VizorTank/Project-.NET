using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project_.NET.Models
{
    public class Favorite
    {
        [Required]
        public Recipe recipes { get; set; }
        [Required]
        public IdentityUser user { get; set; }
        public bool value { get; set; }
    }
}

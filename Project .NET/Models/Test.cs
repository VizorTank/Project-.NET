using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_.NET.Models
{
    public class Test
    {
        public int Id { get; set; }
        public Recipes T { get; set; }
    }
}
/*
 * table.ForeignKey(
                        name: "FK_Test_Recipes_TId",
                        column: x => x.TId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
 */
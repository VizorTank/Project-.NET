﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace Project_.NET.Models
{
    public class Recipes
    {
        public int Id { get; set; }

        public int User_Id { get; set; }

        public string Name { get; set; }
        public string Ings { get; set; }

        public string Desc { get; set; }
        public int up_vote { get; set; }

        public int down_vote { get; set; }

        public string date { get; set; }

        public Recipes(int _User_Id, string _Name, string _Ings, string _Desc )
        {
            User_Id = _User_Id;
            Name = _Name;
            Ings = _Ings;
            Desc = _Desc;
            up_vote = 0;
            down_vote = 0;
            date = DateTime.Now.ToString();

        }
        public Recipes()
        { }
     //  public Recipes()
     //   {
     //       User_Id = 0;
     //       Name = null;
     //       Ings = null;
     //       Desc = null;
     //       up_vote = 0;
     //       down_vote = 0;
     //       date = DateTime.Now.ToString();
     //
     //   }
    }
}

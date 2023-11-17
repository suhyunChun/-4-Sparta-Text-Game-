﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRpg.Player;

namespace TextRpg.Item
{
    public class Items : IItem
    {
        public string Name { get;}

        public string Kind { get;}

        public int Grade { get; }

        public int Price { get;  }

        public bool IsEquiped { get; set; }

        public void Drop(){

        }

        public void Use(Job player){

        }


        public Items(string name, string kind, int grade, int price, bool isEquiped)
        {
            Name = name;
            Kind = kind;
            Grade = grade;
            Price = price;
            IsEquiped = isEquiped;
        }

    }
}
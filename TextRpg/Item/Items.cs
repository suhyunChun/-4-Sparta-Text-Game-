﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRpg.InvenShop;
using TextRpg.Player;

namespace TextRpg.Item
{
    public class Items : IItem
    {
        public string Name { get;}

        public string Kind { get;}

        public int Grade { get; }

        public int Price { get; }

        public bool IsEquiped { get; set; }

        public void Drop(){

        }
        //아이템사용시 플레이어에 어떠한 변화를 주기 때문에 player를 받아옴
        public virtual void Use(Job player)
        {

        }

        public void BonusStatus(Inventory invetory) { }
      

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
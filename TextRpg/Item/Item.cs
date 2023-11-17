using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRpg.Player;

namespace TextRpg.Item
{
    public abstract class Item : IItem
    {
        public string Name { get; }
        public string Kind { get; }
        public int Grade { get; }
        public int Price { get; }
        public bool IsEquiped { get; set; }

        // 아이템 사용
        public abstract void Use(Job player);
        // 아이템 드랍
        public abstract void Drop();

        public Item(string name, string kind, int grade, int price ){
            Name = name;
            Kind = kind;
            Grade = grade;
            Price = price;
            IsEquiped = false; // 초반에는 false 
        }

    }

}

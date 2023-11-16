using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRpg.Item
{
    // 무기
    public class Weapon : IItem
    {
        public string Name { get; }

        public int Grade { get; }

        public int Price { get; }

        public bool IsEquiped { get; set; }

        // 공격력
        private int Atk;

        public void Drop()
        {
            
        }

        public void Use()
        {
   
        }

        // 무기 생성자
        public Weapon (string name, int grade, int price, int atk)
        {
            Name = name;
            Grade = grade;
            Price = price;
            IsEquiped = false;
            Atk = atk;
        }
    }

    // 방어구
    public class Armor : IItem
    {
        public string Name { get; }
        public int Grade { get; }

        public int Price { get; }

        public bool IsEquiped { get; set; }

        // 방어력
        private int Def;

        public void Drop()
        {
            
        }

        public void Use()
        {
            
        }

        // 방어구 생성자
        public Armor(string name, int grade, int price, int def)
        {
            Name = name;
            Grade = grade;
            Price = price;
            IsEquiped = false;
            Def = def;
        }
    }
}

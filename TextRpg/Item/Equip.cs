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

        public string Kind { get; }
        public int Grade { get; }

        public int Price { get; }

        public bool IsEquiped { get; set; }


        // 공격력
        private int Atk;

        // public static int ItemCnt = 0;

        public void Drop()
        {
            if(Grade >= 2)
            {
                Console.WriteLine($"아이템 {Name}을(를) 버렸습니다!");
            }else
            {
                Console.WriteLine("3등급 이상의 아이템은 버릴 수 없습니다!");
            }
        }

        public void Use()
        {
            IsEquiped = !IsEquiped;
            
        }

        // 무기 생성자
        public Weapon (string name, int grade, int price, int atk)
        {
            Name = name;
            Kind = "무기";
            Grade = grade;
            Price = price;
            IsEquiped = false;
            Atk = atk;

            // ItemCnt++;
        }
    }

    // 방어구
    public class Armor : IItem
    {
        public string Name { get; }
        public string Kind { get; }
        public int Grade { get; }

        public int Price { get; }

        public bool IsEquiped { get; set; }

       // public static int ItemCnt = 0;
        // 방어력
        private int Def;

        public void Drop()
        {
            if(Grade >= 2)
            {
                Console.WriteLine($"아이템 {Name}을(를) 버렸습니다!");
            }else
            {
                Console.WriteLine("3등급 이상의 아이템은 버릴 수 없습니다!");
            }
        }

        public void Use()
        {
             IsEquiped = !IsEquiped;
        }

        // 방어구 생성자
        public Armor(string name, int grade, int price, int def)
        {
            Name = name;
            Kind = "방어구";
            Grade = grade;
            Price = price;
            IsEquiped = false;
            Def = def;

            //ItemCnt++;
        }
    }
}

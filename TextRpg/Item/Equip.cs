using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRpg.Player;
using System.Xml.Linq;

namespace TextRpg.Item
{
    // 무기
    public class Weapon : Items
    {


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

        public void Use(Job player) //장착
        {
            IsEquiped = !IsEquiped;
        }

        // 무기 생성자
        public Weapon(string name, int grade, int price, int atk, bool isEquiped)
            : base(name, "무기", grade, price, false)
        {
            Atk = atk;
        }

    }

    // 방어구
    public class Armor : Items
    {
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

        public void Use(Job player)
        {
             IsEquiped = !IsEquiped;
        }

        // 방어구 생성자
        public Armor(string name, int grade, int price, int def, bool isEquiped) //외부에서 받아오는 값 
            : base(name, "방어구", grade, price, false) // = Name = name 
        {
            Def = def;
        }
    }
}

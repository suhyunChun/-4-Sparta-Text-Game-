using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRpg.Player;
using System.Xml.Linq;
using TextRpg.InvenShop;

namespace TextRpg.Item
{
    // 무기
    public class Weapon : Items
    {


        // 공격력
        private int Atk;

        public int atk
        {
            get { return Atk; }
            set { Atk = value; }
        }
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

        // 아이템 방어력 계산, 아이템중 weapon을 들고와 BonusDef를 return
        public int BonusStatus(Inventory inventory)
        {
            int BonusAtk = 0;
            List<Items> itemList = inventory.invenItems;

            foreach (Items equip in itemList)
            {
                if (equip is Weapon && equip.IsEquiped == true)
                {
                    return BonusAtk += Atk;
                }
               
            }

            return BonusAtk;

        }

        public void Use(Job player) //장착
        {
            
        }

        // 무기 생성자
        public Weapon(int id, string name, int grade, int price, int atk, bool isEquiped)
            : base(id, name, "무기", grade, price, false)
        {
            Atk = atk;
        }

    }

    // 방어구
    public class Armor : Items
    {
        private int Def;

        public int def
        {
            get { return Def; }
            set { Def = value; }
        }
        // 아이템 방어력 계산, 아이템중 아머를 들고와 BonusDef를 return
        public int BonusStatus(Inventory inventory)
        {
            int BonusDef = 0;
            List<Items> itemList = inventory.invenItems;

            foreach (Items equip in itemList)
            {
                if (equip is Armor && equip.IsEquiped == true)
                {
                    return BonusDef += Def;
                }

            }

            return BonusDef;

        }

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
        public Armor(int id, string name, int grade, int price, int def, bool isEquiped) //외부에서 받아오는 값 
            : base(id, name, "방어구", grade, price, false) // = Name = name 
        {
            Def = def;
        }
    }
}

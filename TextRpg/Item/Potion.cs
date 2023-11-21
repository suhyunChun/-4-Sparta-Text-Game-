using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRpg.InvenShop;
using TextRpg.Player;

namespace TextRpg.Item
{

    // 힐링 포션
    public class HealingPotion : Items
    {
        // 힐링 양
        private int HealingAmount;

        // 힐링 포션 생성자
        public HealingPotion(int id, string name, int grade, int price, int healingAmount ,bool isEquiped)
            : base(id, name, "힐링포션", grade, price, false)
        {
            HealingAmount = healingAmount;
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
        
        // 힐링 포션에서 아이템을 사용하면 나타나는 로직
        public override void Use(Job player)
        {
            if(player != null) 
            {
                player.Health += HealingAmount;
            
            }
        }


    }

    // 마나 포션
    public class ManaPotion : Items
    {
        private int ManaAmount;

        public ManaPotion(int id, string name, int grade, int price, int manaAmount, bool isEquiped)
        : base(id, name, "마나포션", grade, price, false)
        {
            ManaAmount = manaAmount;
        }

        public override void Use(Job player)
        {
            if (player != null)
            {
                player.Mana += ManaAmount;

            }
        }

    }

}

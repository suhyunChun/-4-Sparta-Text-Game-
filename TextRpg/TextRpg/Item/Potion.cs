using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRpg.Item
{

    // 힐링 포션
    public class HealingPotion : IItem
    {
        public string Name { get; }

        public int Grade { get; }

        public int Price { get; }

        public bool IsEquiped { get; set; }

        // 힐링 양
        private int HealingAmount;

        public void Drop()
        {
            
        }

        public void Use()
        {
            
        }

        // 힐링 포션 생성자
        public HealingPotion(string name, int grade, int price, int healingAmount)
        {
            Name = name;
            Grade = grade;
            Price = price;
            IsEquiped = false;
            HealingAmount = healingAmount;

        }
    }
}

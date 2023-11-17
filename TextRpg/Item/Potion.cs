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
<<<<<<< HEAD

    public class HealingPotion : IItem
    {
        public string Name { get; }

        // 종류
        public string Kind { get; }

        public int Grade { get; }

        public int Price { get; }

        public bool IsEquiped { get; set; }


        // 힐링 양
        private int HealingAmount; 

//        public static int ItemCnt = 0;
=======
    public class HealingPotion : Items
    {
        // 힐링 양
        private int HealingAmount;

        // 힐링 포션 생성자
        public HealingPotion(string name, int grade, int price, int healingAmount ,bool isEquiped)
            : base(name, "힐링포션", grade, price, false)
        {
            HealingAmount = healingAmount;
        }
>>>>>>> 9c199b94dcc9d57b397971b492788242ae3917e5

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
<<<<<<< HEAD

        public void Use(Job player)
        {

            IsEquiped = true;
            //캐릭터의 health 받아옴 
            int health =player.Health;
            health +=HealingAmount;
            //새로운 hp로 다시 설정
            player.Health = health; 
   
        }

        public HealingPotion(string name, int grade, int price, int healingAmount)
        {
            Name = name;
            Kind = "물약";
            Grade = grade;
            Price = price;
            IsEquiped = false;
            HealingAmount = healingAmount;


            //ItemCnt++;
        }


        // 힐링 포션 생성자

=======
        
        public void Use()
        {

        }

>>>>>>> 9c199b94dcc9d57b397971b492788242ae3917e5
    }
}

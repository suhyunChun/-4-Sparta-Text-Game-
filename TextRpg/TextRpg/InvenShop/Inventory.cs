using TextRpg.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRpg.InvenShop
{
    // 인벤토리 클래스
    public class Inventory
    {
        private List<IItem> invenItems;

        // 인벤토리
        public Inventory()
        {
            invenItems = new List<IItem>();
        }

        // 아이템 추가
        internal void AddItem(IItem item)
        {
            invenItems.Add(item);
<<<<<<< HEAD
            //Console.WriteLine($"{item.Name}을(를) 추가했습니다.");
=======
            Console.WriteLine($"{item.Name}을(를) 추가했습니다.");
>>>>>>> parent of 45baabc (Delete TextRpg directory)
        }
         
        // 인벤토리 목록
        public void DisplayInventory()
        {
            Console.WriteLine("! 소지품 목록 !");
            foreach(var item in invenItems) 
            {
                Console.WriteLine($"- {item.Name}");
            }
        }

    }
}

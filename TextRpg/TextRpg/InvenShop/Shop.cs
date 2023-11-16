using TextRpg.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRpg.InvenShop
{
    //상점 클래스
    public class Shop
    {
        private List<IItem> shopItems;

        // 상점
        public Shop()
        {
            shopItems = new List<IItem>();
        }

        // 상점 아이템 추가
        internal void AddShopItem(IItem item)
        {
            shopItems.Add(item);
        }

        // 상점 목록
        public void DisplayInventory()
        {
            Console.WriteLine("! 상점 목록 !");
            foreach (var item in shopItems)
            {
                Console.WriteLine($"- {item.Name}");
            }
        }

    }
}

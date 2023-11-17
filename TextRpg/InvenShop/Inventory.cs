using TextRpg.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace TextRpg.InvenShop
{
   
    // 인벤토리 클래스
    public class Inventory
    {
        private List<IItem> invenItems;

        public int ItemCnt
        {
            get { return invenItems.Count; }
        }

        // 인벤토리
        public Inventory()
        {
            invenItems = new List<IItem>();
        }

        // 아이템 추가
        internal void AddItem(IItem item)
        {
            
            invenItems.Add(item);
            //Console.WriteLine($"{item.Name}을(를) 추가했습니다.");
        }
         
        // 인벤토리 목록
        public void DisplayInventory()
        {
            Console.WriteLine("[소지품 목록]");
            Console.WriteLine("");

            if(invenItems.Count == 0)
            {
                Console.WriteLine("[아이템 목록 없음]");
            }
            else
            {
                foreach (var item in invenItems)
                {
                    Console.WriteLine($"- 이름: {item.Name}, 종류: {item.Kind}, " +
                        $"등급: {item.Grade}★, 가격: {item.Price}");
                }
             }

         }

        //드랍할 아이템 목록
        public void DropItem()
        {
            int idx = 0;

            
            Console.WriteLine("[소지품 목록]");
            Console.WriteLine("");
            foreach (var item in invenItems)
            {
                Console.WriteLine($"- {idx + 1} 이름: {item.Name}, 종류: {item.Kind}, " +
                    $"등급: {item.Grade}★, 가격: {item.Price}");

                idx++;
            }
        }

        // 아이템버리기 - 현재 선택된 아이템
        public void CurrentRemoveItem(int index)
        {
            IItem removeItem = invenItems[index];
            Console.WriteLine($"현재 선택된 아이템: {removeItem.Name}");
        }

        // 아이템 삭제
        public void RemoveItem(int index)
        {

            if(index >= 0 && index <  invenItems.Count) 
            {
                IItem removeItem = invenItems[index];
                Console.WriteLine($"아이템이 삭제되었습니다: {removeItem.Name}");
                Console.WriteLine("아무키나 입력하시면 인벤토리로 이동합니다.");
                invenItems.RemoveAt(index);
                Console.ReadLine();

            }
        }


    }
}

using TextRpg.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using TextRpg.Player;

namespace TextRpg.InvenShop
{

    // 인벤토리 클래스
    public class Inventory
    {
        private List<Items> invenItems;
        public bool onEquipMenu;
        public int ItemCnt
        {
            get { return invenItems.Count; }
        }

        // 인벤토리
        public Inventory()
        {
            invenItems = new List<Items>();
        }

        // 아이템 추가
        internal void AddItem(Items item)
        {

            invenItems.Add(item);
            //Console.WriteLine($"{item.Name}을(를) 추가했습니다.");
        }

        // 인벤토리 목록
        public void DisplayInventory()
        {
            Console.WriteLine("[소지품 목록]");
            Console.WriteLine("");

            if (invenItems.Count == 0)
            {
                Console.WriteLine("[아이템 목록 없음]");
            }
            else
            {
                if (onEquipMenu == false)
                {
                    foreach (var item in invenItems)
                    {
                        Console.WriteLine($"- 이름: {item.Name}, 종류: {item.Kind}, " +
                            $"등급: {item.Grade}★, 가격: {item.Price}");
                    }
                }
                else if (onEquipMenu == true)
                {
                    int idx = 0;
                    foreach (var item in invenItems)
                    {
                        if(item.IsEquiped == true)
                        {
                            Console.Write("[E] ");
                        }
                        Console.WriteLine($"- {idx + 1} 이름: {item.Name}, 종류: {item.Kind}, " +
                            $"등급: {item.Grade}★, 가격: {item.Price}");
                         idx++;
                    }
                }
            }
        }

        public void EquipmentStatusChange(int num)
        {

            if(invenItems[num].IsEquiped == true)
            {
                invenItems[num].IsEquiped = false;
            }
            else if(invenItems[num].IsEquiped == false)
            {
                invenItems[num].IsEquiped = true;
            }
        }

        //아이템 목록 index
        public void ShowInvenItem()
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

        // 아이템 판매하기
        public void SellItem(Job player, int index)
        {
            if (index >= 0 && index < invenItems.Count)
            {
                Items sellInvenItem = invenItems[index];

                int totalGold = player.Gold + sellInvenItem.Price;

                player.Gold += sellInvenItem.Price;

                Console.Clear();
                Console.WriteLine($"아이템이 판매되었습니다: {sellInvenItem.Name}");
                Console.WriteLine($"전체 금액: {totalGold}");
                Console.WriteLine($"현재 소지금액: {player.Gold}");
                Console.WriteLine("");
                Console.WriteLine("아무키나 입력하시면 상점으로 이동합니다.");
                invenItems.RemoveAt(index);
                Console.ReadLine();
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

            if (index >= 0 && index < invenItems.Count)
            {
                Items removeItem = invenItems[index];
                Console.WriteLine($"아이템이 삭제되었습니다: {removeItem.Name}");
                Console.WriteLine("아무키나 입력하시면 인벤토리로 이동합니다.");
                invenItems.RemoveAt(index);
                Console.ReadLine();
            }
        }


    }
}

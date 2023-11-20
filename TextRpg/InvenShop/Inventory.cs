using TextRpg.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using TextRpg.Player;
using System.Collections;
using System.Numerics;

namespace TextRpg.InvenShop
{

    // 인벤토리 클래스
    public class Inventory
    {
        Items emptyItem;
        public Job player;
        public List<Items> invenItems;
        public bool onEquipMenu;
        public int arraySortNum = 0;
        public int ItemCnt
        {
            get { return invenItems.Count; }
        }

        // 인벤토리
        public Inventory(Job _player)
        {
            invenItems = new List<Items>();
            player = _player;
        }

        // 아이템 추가
        internal void AddItem(Items item)
        {
            invenItems.Add(item);
            //Console.WriteLine($"{item.Name}을(를) 추가했습니다.");
        
        }

        // 힐링포션 개수


        // 인벤토리 목록
        public void DisplayInventory()
        {
            Console.Write("[소지품 목록]");
            switch (arraySortNum)
            {
                case 0:
                    Console.WriteLine("");
                    break;
                case 1:
                    Console.WriteLine(" - 등급 순");
                    break;
                case 2:
                    Console.WriteLine(" - 등급 역순");
                    break;
                case 3:
                    Console.WriteLine(" - 가격 순");
                    break;
                case 4:
                    Console.WriteLine(" - 가격 역순");
                    break;
            }
            Console.WriteLine("");

            if (invenItems.Count == 0)
            {
                Console.WriteLine("[아이템 목록 없음]");
            }
            else
            {
                if (onEquipMenu == false)//인벤토리창
                {
                    foreach (var item in invenItems)
                    {
                        Console.WriteLine($"- 이름: {item.Name}, 종류: {item.Kind}, " +
                            $"등급: {item.Grade}★, 가격: {item.Price}");
                    }
                }
                else if (onEquipMenu == true)//장비관리창
                {
                    int idx = 0;
                    foreach (var item in invenItems)
                    {
                        if (item.IsEquiped == true)
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
        //장착상태 적용
        public void EquipmentStatusChange(int num)
        {

            if (invenItems[num].IsEquiped == true)
            {
                invenItems[num].IsEquiped = false;
            }
            else if (invenItems[num].IsEquiped == false)
            {
                invenItems[num].IsEquiped = true;
            }
        }
        //아이템 분류
        public void InventoryArraySort()
        {
            arraySortNum++;
            if (arraySortNum == 5)
            {
                arraySortNum = 1;
            }
            switch (arraySortNum)
            {
                case 1://아이템 등급 순서대로 오름정렬
                    for (int i = 0; i < ItemCnt - 1; i++)
                    {
                        for (int j = i + 1; j < ItemCnt; j++)
                        {
                            if (invenItems[i].Grade > invenItems[j].Grade)
                            {
                                emptyItem = invenItems[i];
                                invenItems[i] = invenItems[j];
                                invenItems[j] = emptyItem;
                            }
                        }
                    }
                    break;
                case 2://아이템 등급 순서대로 내림정렬
                    for (int i = 0; i < ItemCnt - 1; i++)
                    {
                        for (int j = i + 1; j < ItemCnt; j++)
                        {
                            if (invenItems[i].Grade < invenItems[j].Grade)
                            {
                                emptyItem = invenItems[i];
                                invenItems[i] = invenItems[j];
                                invenItems[j] = emptyItem;
                            }
                        }
                    }
                    break;
                case 3://아이템 가격 순서대로 오름정렬
                    for (int i = 0; i < ItemCnt - 1; i++)
                    {
                        for (int j = i + 1; j < ItemCnt; j++)
                        {
                            if (invenItems[i].Price > invenItems[j].Price)
                            {
                                emptyItem = invenItems[i];
                                invenItems[i] = invenItems[j];
                                invenItems[j] = emptyItem;
                            }
                        }
                    }
                    break;
                case 4://아이템 가격 순서대로 내림정렬
                    for (int i = 0; i < ItemCnt - 1; i++)
                    {
                        for (int j = i + 1; j < ItemCnt; j++)
                        {
                            if (invenItems[i].Price < invenItems[j].Price)
                            {
                                emptyItem = invenItems[i];
                                invenItems[i] = invenItems[j];
                                invenItems[j] = emptyItem;
                            }
                        }
                    }
                    break;
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

        // 아이템 사용
        public void UseHpPotion()
        {
            // OfType -> LINQ의 지정된 형식으로 형변환이 가능한 요소만을 선택하여 .ToList list에 담은걸 hpPotions에 넣는다.
            var hpPotions = invenItems.OfType<HealingPotion>().ToList();
            
            foreach (var hpPotion in hpPotions)
            {
                if (hpPotion is HealingPotion)
                {
                    // hpPotion을 사용하고 remove해줌
                    // 하나만 사용해야 하기 때문에 사용시 바로 break로 반복문 탈출
                    hpPotion.Use(player);

                    invenItems.Remove(hpPotion);

                    break;
                }
            }
        }

        public void UseMpPotion()
        {
            var mpPotions = invenItems.OfType<ManaPotion>().ToList();

            foreach (var mpPotion in mpPotions)
            {
                if (mpPotion is ManaPotion)
                {
                    mpPotion.Use(player);

                    invenItems.Remove(mpPotion);

                    break;
                }
            }
        }


    }
}

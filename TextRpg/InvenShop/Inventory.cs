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
using TextRpg.Appearance;
using System.Xml.Linq;
using System.Reflection.Metadata.Ecma335;

namespace TextRpg.InvenShop
{

    // 인벤토리 클래스
    public class Inventory
    {
        Items emptyItem;
        FontColor fontColor;
        public Job player;
        public List<Items> invenItems;
        public List<Items> potionItems;
        public List<Items> equipmentItems;
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
            potionItems = new List<Items>();
            equipmentItems = new List<Items>();
            player = _player;
            fontColor = new FontColor();
        }

        // 아이템 추가
        internal void AddItem(Items item, Job Player)
        {
            Player.Item.Add(item.Id);
            invenItems.Add(item);
            if(item is HealingPotion||item is ManaPotion)
            {
                potionItems.Add(item);
            }
            else if (item is Weapon || item is Armor)
            {
                equipmentItems.Add(item);
            }
            //Console.WriteLine($"{item.Name}을(를) 추가했습니다.");
        }

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
                    Console.WriteLine(" - 높은 등급 순");
                    break;
                case 2:
                    Console.WriteLine(" - 낮은 등급 순");
                    break;
                case 3:
                    Console.WriteLine(" - 높은 가격 순");
                    break;
                case 4:
                    Console.WriteLine(" - 낮은 가격 순");
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
                        if (item.IsEquiped)
                        {
                            Console.Write("- [");
                            fontColor.WriteColorFont("E", FontColor.Color.Cyan);
                            Console.Write("]");
                            Console.Write($" 이름: {TextSort.PadRightForMixedText(item.Name, 15)}" + " | ");
                        }else
                        {
                            Console.Write($"- 이름: {TextSort.PadRightForMixedText(item.Name, 18)} " + " | ");
                        }
                            Console.Write(
                                                    $"종류: {TextSort.PadRightForMixedText(item.Kind, 10)}" + " | " +
                                                    $"등급: {item.Grade} ★ " + " | " +
                                                    $"가격: {TextSort.PadRightForMixedNum(item.Price, 8)}" + " | ");

                            if (item is Weapon weapon)
                            {
                            Console.Write($"공격력: {weapon.atk}");
                            }
                            else if(item is Armor armor)
                            {
                                Console.Write($"방어력: {armor.def}");
                            }
                            else if(item is HealingPotion hpPotion)
                            {
                                Console.Write($"HP 회복량: {hpPotion.healingAmount}");
                            }
                            else if(item is ManaPotion mpPotion)
                            {
                                Console.Write($"MP 회복량: {mpPotion.manaAmount}");
                            }

                        Console.WriteLine();
                    }
                }
                else if (onEquipMenu == true)//장비관리창
                {

                    int idx = 0;

                    foreach (var item in invenItems)
                    {

                        Console.Write("- ");
                        fontColor.WriteColorFont($"{TextSort.PadRightForMixedText($" {idx + 1} ", 4)}", FontColor.Color.Yellow);

                        if (item.IsEquiped)
                        {
                            Console.Write("[");
                            fontColor.WriteColorFont("E", FontColor.Color.Cyan);
                            Console.Write("]");
                            Console.Write($" 이름: {TextSort.PadRightForMixedText(item.Name, 16)}" + " | ");
                        }
                        else
                        {
                            Console.Write($" 이름: {TextSort.PadRightForMixedText(item.Name, 18)} " + " | ");
                         }
                       
                            Console.Write(
                                                    $"종류: {TextSort.PadRightForMixedText(item.Kind, 10)}" + " | " +
                                                    $"등급: {item.Grade} ★ " + " | " +
                                                    $"가격: {TextSort.PadRightForMixedNum(item.Price, 8)}" + " | "
                                                    );

                        if (item is Weapon weapon)
                        {
                            Console.Write($"공격력: {weapon.atk}");
                        }
                        else if (item is Armor armor)
                        {
                            Console.Write($"방어력: {armor.def}");
                        }
                        else if (item is HealingPotion hpPotion)
                        {
                            Console.Write($"HP 회복량: {hpPotion.healingAmount}");
                        }
                        else if (item is ManaPotion mpPotion)
                        {
                            Console.Write($"MP 회복량: {mpPotion.manaAmount}");
                        }

                        Console.WriteLine();

                        idx++;

                    }
                }
            }
        }
        //장착상태 적용
        public void EquipmentStatusChange(int num)
        {
            // 토글
            invenItems[num].IsEquiped = !invenItems[num].IsEquiped;
            Program.player.EquippedList[num] = !Program.player.EquippedList[num];
            if (invenItems[num] is Weapon)
            {
                for (int i = 0; i < invenItems.Count; i++)
                {
                    if (i != num && invenItems[i] is Weapon)
                    {
                        invenItems[i].IsEquiped = false;
                        Program.player.EquippedList[i] = false;
                    }
                }
            }

            else if (invenItems[num] is Armor)
            {
                for (int i = 0; i < invenItems.Count; i++)
                {
                    if (i != num && invenItems[i] is Armor)
                    {
                        invenItems[i].IsEquiped = false;
                        Program.player.EquippedList[i] = false;
                    }
                }
            }

            // 포션 여러개 장착 이슈로 삭제
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
                case 1://아이템 등급 순서대로 내림정렬
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
                case 2://아이템 등급 순서대로 오름정렬
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
                case 3://아이템 가격 순서대로 내림정렬
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
                case 4://아이템 가격 순서대로 오름정렬
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
            }
        }
        //아이템 목록 index
        public void ShowInvenItem (int category)
        {
            //category 0전부 1장비류 2물약류
            int idx = 0;
            Console.WriteLine("[소지품 목록]");
            Console.WriteLine("");
            if (category == 0)
            {
                foreach (var item in invenItems)
                {
                    Console.Write("- ");
                    fontColor.WriteColorFont($"{TextSort.PadRightForMixedText($" {idx + 1} ", 4)}", FontColor.Color.Yellow);

                    if (item.IsEquiped)
                    {
                        Console.Write("[");
                        fontColor.WriteColorFont("E", FontColor.Color.Cyan);
                        Console.Write("]");
                        Console.Write($" 이름: {TextSort.PadRightForMixedText(item.Name, 16)}" + " | ");
                    }
                    else
                    {
                        Console.Write($" 이름: {TextSort.PadRightForMixedText(item.Name, 18)} " + " | ");
                    }

                    Console.Write(
                                            $"종류: {TextSort.PadRightForMixedText(item.Kind, 10)}" + " | " +
                                            $"등급: {item.Grade} ★ " + " | " +
                                            $"가격: {TextSort.PadRightForMixedNum(item.Price, 8)}" + " | "
                                            );

                    if (item is Weapon weapon)
                    {
                        Console.Write($"공격력: {weapon.atk}");
                    }
                    else if (item is Armor armor)
                    {
                        Console.Write($"방어력: {armor.def}");
                    }
                    else if (item is HealingPotion hpPotion)
                    {
                        Console.Write($"HP 회복량: {hpPotion.healingAmount}");
                    }
                    else if (item is ManaPotion mpPotion)
                    {
                        Console.Write($"MP 회복량: {mpPotion.manaAmount}");
                    }

                    Console.WriteLine();

                    idx++;
                }
            }
            else if (category == 1)
            {
                foreach (var item in invenItems)
                {
                    if (item is Weapon || item is Armor)
                    {
                        Console.Write("-");
                        fontColor.WriteColorFont($" {idx + 1} ", FontColor.Color.Green);

                        Console.Write(
                            $"이름: {TextSort.PadRightForMixedText(item.Name, 17)} " + " | " +
                            $"종류: {TextSort.PadRightForMixedText(item.Kind, 10)}" + " | " +
                            $"등급: {item.Grade} ★ " + " | " +
                            $"가격: {TextSort.PadRightForMixedNum(item.Price, 8)}" + " | "
                                                    );

                        if (item is Weapon weapon)
                        {
                            Console.Write($"공격력: {weapon.atk}");
                        }
                        else if (item is Armor armor)
                        {
                            Console.Write($"방어력: {armor.def}");
                        }
                        idx++;
                        Console.WriteLine("");
                    }
                }
            }
            else if (category == 2)
            {
                foreach (var item in invenItems)
                {
                    if (item is HealingPotion || item is ManaPotion)
                    {
                        Console.Write("-");
                        fontColor.WriteColorFont($" {idx + 1} ", FontColor.Color.Green);

                        Console.Write(
                            $"이름: {TextSort.PadRightForMixedText(item.Name, 17)} " + " | " +
                            $"종류: {TextSort.PadRightForMixedText(item.Kind, 10)}" + " | " +
                            $"등급: {item.Grade} ★ " + " | " +
                            $"가격: {TextSort.PadRightForMixedNum(item.Price, 8)}" + " | "
                                                    );

                       if (item is HealingPotion hpPotion)
                        {
                            Console.Write($"HP 회복량: {hpPotion.healingAmount}");
                        }
                        else if (item is ManaPotion mpPotion)
                        {
                            Console.Write($"MP 회복량: {mpPotion.manaAmount}");
                        }

                        idx++;
                        Console.WriteLine("");
                    }
                }
            }
        }

        // 아이템 판매하기
        public void SellItem(Job player, int index)
        {
            if (index >= 0 && index < invenItems.Count)
            {
                Items sellInvenItem = invenItems[index];

                if(sellInvenItem.IsEquiped)
                {
                    Console.Clear();
                    fontColor.WriteColorFont("장착한 아이템은 판매할 수 없습니다!", FontColor.Color.Red);
                    Console.WriteLine("\n");
                    Console.WriteLine("아이템 장착 해제한 후 판매해주세요");
                    Console.WriteLine("");
                    Console.WriteLine("아무키나 입력하시면 상점으로 이동합니다.");
                    Console.ReadLine();

                    return;
                }

                int totalGold = player.Gold + (int)(sellInvenItem.Price * 0.8f); // 판매가격 80% - 나재민
                player.Gold += (int)(sellInvenItem.Price * 0.8f);

                Console.Clear();
                Console.WriteLine($"아이템이 판매되었습니다: {sellInvenItem.Name}");
                Console.WriteLine($"전체 금액: {totalGold}");
                Console.WriteLine($"현재 소지금액: {player.Gold}");
                Console.WriteLine("");
                Console.WriteLine("아무키나 입력하시면 상점으로 이동합니다.");
                player.Item.Remove(invenItems[index].Id);
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
        public void RemoveItem(int index, Job Player)
        {

            if (index >= 0 && index < invenItems.Count)
            {
                Items removeItem = invenItems[index];
                Console.WriteLine($"아이템이 삭제되었습니다: {removeItem.Name}");
                Console.WriteLine("아무키나 입력하시면 인벤토리로 이동합니다.");
                Player.Item.Remove(invenItems[index].Id);
                invenItems.RemoveAt(index);
                Console.ReadLine();
            }
        }

        // 아이템 사용
        public void UseHpPotion(Job Player)
        {
            // OfType -> LINQ의 지정된 형식으로 형변환이 가능한 요소만을 선택하여 .ToList list에 담은걸 hpPotions에 넣는다.
            var hpPotions = invenItems.OfType<HealingPotion>().ToList();

            foreach (var hpPotion in hpPotions)
            {
                if (hpPotion is HealingPotion)
                {
                    // 장착하지 않은 포션일 경우 패스
                    if (!hpPotion.IsEquiped)
                        continue;

                    // hpPotion을 사용하고 remove해줌
                    // 하나만 사용해야 하기 때문에 사용시 바로 break로 반복문 탈출
                    hpPotion.Use(player);
                    Player.Item.Remove(hpPotion.Id);
                    invenItems.Remove(hpPotion);

                    break;
                }
            }
        }

        public void UseMpPotion(Job Player)
        {
            var mpPotions = invenItems.OfType<ManaPotion>().ToList();

            foreach (var mpPotion in mpPotions)
            {
                if (mpPotion is ManaPotion)
                {
                    if (!mpPotion.IsEquiped)
                        continue;

                    mpPotion.Use(player);
                    Player.Item.Remove(mpPotion.Id);
                    invenItems.Remove(mpPotion);

                    break;
                }
            }
        }


    }
}

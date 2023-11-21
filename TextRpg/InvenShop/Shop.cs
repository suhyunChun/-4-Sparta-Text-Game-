using TextRpg.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRpg.Player;
using TextRpg.Appearance;

namespace TextRpg.InvenShop
{
    //상점 클래스
    public class Shop
    {
        FontColor fontColor;
        private List<Items> shopItems;
        private Inventory inventory;
         
        public int ShopItemCnt
        {
            get { return shopItems.Count; }
        }

        // 가격 호출 메서드
        public int ShopItemPrice(int index)
        {
            if(index >= 0 && index < shopItems.Count)
            {
                return shopItems[index].Price;
            }
            return 0;
        }

        // 상점
        public Shop()
        {
            shopItems = new List<Items>();
            fontColor = new FontColor();
        }

        // 상점 아이템 추가
        internal void AddShopItem(Items item)
        {
            shopItems.Add(item);
        }

        // 상점 목록
        public void DisplayShop()
        {
            Console.WriteLine("[상점 목록]");
            Console.WriteLine("");
            foreach (var item in shopItems)
            {

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
                else if (item is HealingPotion hpPotion)
                {
                    Console.Write($"HP 회복량: {hpPotion.healingAmount}");
                }
                else if (item is ManaPotion mpPotion)
                {
                    Console.Write($"MP 회복량: {mpPotion.manaAmount}");
                }

                Console.WriteLine();
            }
        }

        // 아이템 사기
        public void BuyShopItem()
        {
            int idx = 0;

            foreach (var item in shopItems)
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
                else if (item is HealingPotion hpPotion)
                {
                    Console.Write($"HP 회복량: {hpPotion.healingAmount}");
                }
                else if (item is ManaPotion mpPotion)
                {
                    Console.Write($"MP 회복량: {mpPotion.manaAmount}");
                }

                idx++;

                Console.WriteLine();
            }
        }

        // 상점에서 구매시 인벤토리 추가 메서드
        public void BuyItemAddInventory(Job player, Inventory inventory, int index)
        {

            if (index >= 0 && index < shopItems.Count)
            {
                Items purchasedItem = shopItems[index];
                
                int totalGold = player.Gold - purchasedItem.Price;
                player.Gold -= purchasedItem.Price;

                Console.Clear();
                fontColor.WriteColorFont("아이템 구매 완료", FontColor.Color.Magenta);
                Console.WriteLine("");

                Console.Write($"구매된 아이템:  ");
                fontColor.WriteColorFont($"{ purchasedItem.Name} {purchasedItem.Grade}★", FontColor.Color.Yellow);
                Console.WriteLine("");
                

                Console.WriteLine("");

                
                if(player.Gold < 1500)
                {
                    Console.Write("소지 금액: ");
                    fontColor.WriteColorFont($"{player.Gold}", FontColor.Color.DarkRed);
                }
                else
                {
                    Console.Write($"소지 금액: {player.Gold}");
                }

                Console.WriteLine("\n");

                Console.WriteLine("아무키나 입력하여 다음행동을 진행해주세요");
                Console.ReadLine();
                inventory.AddItem(purchasedItem, player);

                shopItems.RemoveAt(index);
                
            }
        }

    }
}

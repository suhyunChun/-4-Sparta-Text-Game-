using System;
using System.ComponentModel;
using System.Reflection;
using TextRpg.InvenShop;
using TextRpg.Item;
using TextRpg.Player;

namespace TextRpg
{
    internal class Program
    {
        static Inventory inventory;
        static Shop shop;
        static Job player;
        static IItem _item;
        static Battle battle;
        // 아이템 세팅
        private static void GameItemSetting(Inventory inventory, Shop shop)
        {
            inventory.AddItem(new Weapon("낡은 검", 1, 100, 10));
            inventory.AddItem(new Armor("낡은 방패", 1, 100, 10));
            inventory.AddItem(new HealingPotion("일반 회복 물약", 1, 100, 10));

            shop.AddShopItem(new Weapon("황금 검", 2, 300, 20));
            shop.AddShopItem(new Armor("황금 방패", 2, 300, 15));
            shop.AddShopItem(new HealingPotion("고급 회복 물약", 2, 200, 20));

        }
        // 시작 씬
        private static void PrintStartScene()
        {
            Console.WriteLine(" ____                                     \r\n|  _ \\ _   _ _ __   __ _  ___  ___  _ __  \r\n| | | | | | | '_ \\ / _` |/ _ \\/ _ \\| '_ \\ \r\n| |_| | |_| | | | | (_| |  __/ (_) | | | |\r\n|____/ \\__,_|_| |_|\\__, |\\___|\\___/|_| |_|\r\n                   |___/                  ");

            Console.WriteLine("============= Press Any Key =============");
            Console.ReadLine();
        }

        // 플레이어 이름 입력 메뉴
        private static void PlayerInputName()
        {
            string playerName;

            Console.Clear();
            Console.WriteLine("OO 마을에 오신걸 환영합니다.");

            // 공백시 입장 불가
            do
            {
                Console.WriteLine("플레이어 이름을 입력해주세요");
                playerName = Console.ReadLine();

                if(string.IsNullOrWhiteSpace(playerName))
                {
                    Console.WriteLine("플레이어 이름은 공백일 수 없습니다.");
                }

            }while(string.IsNullOrWhiteSpace(playerName));

            SelectedJobMenu(playerName);
        }

        // 직업선택 메뉴
        private static void SelectedJobMenu(string playerName)
        {
            Console.Clear();
            Console.WriteLine($"{playerName} 님 반갑습니다!");
            Console.WriteLine("먼저 직업을 선택해주세요.");
            Console.WriteLine("");
            Console.WriteLine("1. 전사");
            Console.WriteLine("2. 마법사");
            Console.WriteLine("3. 궁수");
            Console.WriteLine(" ");

            switch (CheckValidInput(1, 3))
            {
                case 1:
                    player = new Warrior(playerName);
                    StartMenu(player.Occupation);
                    break;
                case 2:
                    player = new Mage(playerName);
                    StartMenu(player.Occupation);
                    break;
                case 3:
                    player = new Archer(playerName);
                    StartMenu(player.Occupation);
                    break;
            }
        }

        // 시작 메뉴
        public static void StartMenu(string occupation)
        {
            Console.Clear();
            Console.WriteLine("{0} 을(를) 선택하셨습니다.", occupation);
            Console.WriteLine("던전에 입장하시기 전 정비할 수 있습니다.");
            Console.WriteLine("선택지 중 하나를 선택해 주세요");
            Console.WriteLine("");
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("");
            Console.WriteLine("4. 던전입장");
            Console.WriteLine("");

            switch (CheckValidInput(1, 4))
            {
                case 1:
                    StatusMenu();
                    break;
                case 2:
                    InventoryMenu();
                    break;
                case 3:
                    ShopMenu();
                    break;
                case 4:
                    battle = new Battle(player);
                    battle.BattleScene();
                    //StageSelected();
                    break;
            }

        }

        // 던전입장
        private static void StageSelected()
        {
            Console.Clear();
            Console.WriteLine("스테이지 선택 메뉴입니다.");
        }

        //상태 메뉴
        private static void StatusMenu()
        {
            Console.Clear();
            Console.WriteLine("상태 메뉴입니다.");
            Console.WriteLine("캐릭터의 정보를 표기하는 곳입니다.");

            Console.WriteLine($"플레이어 이름: {player.Name} ( {player.Occupation} )");
            Console.WriteLine("LV: {0}", player.Level.ToString("00"));
            Console.WriteLine();
            Console.WriteLine($"공격력: {player.Atk}");
            Console.WriteLine($"방어력: {player.Def}");
            Console.WriteLine($"체력: {player.Health}");
            Console.WriteLine($"소지골드: {player.Gold}");
            Console.WriteLine(" ");

            Console.WriteLine("0. 뒤로가기");
            Console.WriteLine("");
            
            switch(CheckValidInput(0, 0))
            {
                case 0:
                    StartMenu(player.Occupation);
                break;
            }

        }

        // 인벤토리 메뉴
        private static void InventoryMenu()
        {
            Console.Clear();
            Console.WriteLine("인벤토리 메뉴입니다.");
            Console.WriteLine("아이템을 관리할 수 있습니다.");

            Console.WriteLine("");
            inventory.DisplayInventory();

            Console.WriteLine("");
            Console.WriteLine("1. 장비 관리하기");
            Console.WriteLine("2. 아이템 사용하기");
            Console.WriteLine("3. 아이템 버리기");
            Console.WriteLine("");
            Console.WriteLine("0. 뒤로가기");
            Console.WriteLine("");


            switch (CheckValidInput(0, 3))
            {
                case 1:
                    EquipMenu();
                    break;
                case 2:
                    _item.Use();
                    break;
                case 3:
                    DropItemMenu();
                    break;
                case 0:
                    StartMenu(player.Occupation);
                    break;
            }

        }

        //아이템 버리기 메뉴
        private static void DropItemMenu()
        {
            Console.Clear();
            Console.WriteLine("인벤토리 - 아이템버리기");
            Console.WriteLine("어떤 아이템을 버리시겠습니까?");
            Console.WriteLine(" ");
            Console.WriteLine($"아이템개수: {inventory.ItemCnt}");

            Console.WriteLine("");
            inventory.DropItem();
            Console.WriteLine("");
            Console.WriteLine("0. 뒤로가기");
            Console.WriteLine("");

            int keyInput = CheckValidInput(0, inventory.ItemCnt);
            switch(keyInput)
            {
                case 0:
                    InventoryMenu();
                    break;
                default:
                    IsRemoveItem(keyInput - 1);
                    break;
            }
        }

        //아이템 버리기 경고
        private static void IsRemoveItem(int itemIndex)
        {
            Console.Clear();
            Console.WriteLine("정말 아이템을 삭제하시겠습니까?");
            Console.WriteLine("");
            inventory.CurrentRemoveItem(itemIndex);
            Console.WriteLine("");

            Console.WriteLine("1. 네");
            Console.WriteLine("2. 아니오");
            Console.WriteLine("");

            switch (CheckValidInput(1, 2))
            {
                case 1:
                    inventory.RemoveItem(itemIndex);
                    DropItemMenu();
                    break;
                case 2:
                    DropItemMenu();
                    break;
            }
        }

        //장비 관리
        private static void EquipMenu()
        {
            
        }

        // 상점 메뉴
        private static void ShopMenu()
        {
            Console.Clear();
            Console.WriteLine("상점 메뉴입니다.");
        }

        // 핸들러
        public static int CheckValidInput(int min, int max)
        {
            int keyInput;
            bool result;

            do
            {
                Console.WriteLine("번호를 입력해주세요.");
                result = int.TryParse(Console.ReadLine(), out keyInput);
            }while(result = false || CheckIfValid(keyInput, min, max) == false);

            return keyInput;

        }

        private static bool CheckIfValid(int keyInput, int min, int max)
        {
            if(min <= keyInput && max >= keyInput) 
            {
                return true;
            }
            return false;
        }

        // 메인
        static void Main(string[] args)
        {
            inventory = new Inventory();
            shop = new Shop();

            GameItemSetting(inventory, shop);
            PrintStartScene();
            PlayerInputName();

            //inventory.DisplayInventory();
            //shop.DisplayInventory();
            //Console.WriteLine(" ");
            //_player = new Warrior(playerName);
            //Console.WriteLine($"플레이어 이름 {_player.Name}");


        }
    }
}
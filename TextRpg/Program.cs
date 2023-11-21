using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Reflection.Emit;
using TextRpg.Appearance;
using TextRpg.InvenShop;
using TextRpg.Item;
using TextRpg.Player;
using WMPLib;


namespace TextRpg
{
    internal class Program
    {
        static Inventory inventory;
        static Shop shop;
        // public이 붙음, player생성 시 다양한 곳에서 땡겨오기 위해
        public static Job player;
        static Items items;
        static Battle battle;
        static FontColor fontColor;

        delegate void func1(string str, int cursor);
        delegate void func2(int cursor);
        delegate void func3(int idx, int cursor);
        static ConsoleKeyInfo c;

        public static WindowsMediaPlayer wmp;

        // 아이템 세팅
        // 테스팅을 위해 포션추가
        private static void GameItemSetting(Inventory inventory, Shop shop)
        {
            inventory.AddItem(new Weapon("낡은 검1", 3, 1000, 3, false));
            inventory.AddItem(new Weapon("낡은 검2", 3, 1000, 4, false));
            inventory.AddItem(new Weapon("낡은 검3", 3, 1000, 12, false));

            inventory.AddItem(new Armor("낡은 방패", 1, 100, 10, false));
            inventory.AddItem(new HealingPotion("일반 회복 물약", 1, 100, 10, false));
            inventory.AddItem(new HealingPotion("일반 회복 물약", 1, 100, 10, false));
            inventory.AddItem(new ManaPotion("마나 회복 물약", 1, 100, 10, false));
            inventory.AddItem(new ManaPotion("마나 회복 물약", 1, 100, 10, false));

            shop.AddShopItem(new Weapon("황금 검", 2, 300, 20, false));
            shop.AddShopItem(new Armor("황금 방패", 2, 300, 15, false));
            shop.AddShopItem(new HealingPotion("고급 회복 물약", 2, 200, 20, false));
            shop.AddShopItem(new HealingPotion("고오급 회복 물약", 2, 1000000, 20, false));
        }
        // 시작 씬
        private static void PrintStartScene()
        {
            Console.Write("oooooooooo.                                                                      \n" +
                              "`888'   `Y8b                                                                     \n" +
                              " 888      888 oooo  oooo  ooo. .oo.    .oooooooo  .ooooo.   .ooooo.  ooo. .oo.   \n" +
                              " 888      888 `888  `888  `888P\"Y88b  888' `88b  d88' `88b d88' `88b `888P\"Y88b  \n" +
                              " 888      888  888   888   888   888  888   888  888ooo888 888   888  888   888  \n" +
                              " 888     d88'  888   888   888   888  `88bod8P'  888    .o 888   888  888   888  \n" +
                              "o888bood8P'    `V88V\"V8P' o888o o888o `8oooooo.  `Y8bod8P' `Y8bod8P' o888o o888o \n" +
                              "                                      d\"     YD                                  \n" +
                              "                                      \"Y88888P'                                  \n");
            Console.Write("                                         .o88o.                                  \n" +
                              "                                         888 `\"                                  \n" +
                              "                               .ooooo.  o888oo                                   \n" +
                              "                              d88' `88b  888                                     \n");
            fontColor.WriteColorFont("                              888   888  888                                     \n", FontColor.Color.DarkRed);
            fontColor.WriteColorFont("                              888   888  888                                     \n", FontColor.Color.DarkYellow);
            fontColor.WriteColorFont("                              `Y8bod8P' o888o                                    \n", FontColor.Color.DarkGreen);
            fontColor.WriteColorFont("           .oooooo..o                                   .                        \n", FontColor.Color.DarkBlue);
            fontColor.WriteColorFont("          d8P'    `Y8                                 .o8                        \n", FontColor.Color.Cyan);
            fontColor.WriteColorFont("          Y88bo.      oo.ooooo.   .oooo.   oooo d8b .o888oo  .oooo.              \n", FontColor.Color.Magenta);
            fontColor.WriteColorFont("           `\"Y8888o.   888' `88b `P  )88b  `888\"\"8P   888   `P  )88b          \n", FontColor.Color.DarkRed);
            fontColor.WriteColorFont("               `\"Y88b  888   888  .oP\"888   888       888    .oP\"888          \n", FontColor.Color.DarkYellow);
            fontColor.WriteColorFont("          oo     .d8P  888   888 d8(  888   888       888 . d8(  888             \n", FontColor.Color.DarkGreen);
            fontColor.WriteColorFont("          8\"\"88888P'   888bod8P' `Y888\"\"8o d888b      \"888\" `Y888\"\"8o    \n", FontColor.Color.DarkBlue);
            fontColor.WriteColorFont("                       888                                                       \n", FontColor.Color.Cyan);
            fontColor.WriteColorFont("                      o888o                                                      \n", FontColor.Color.Magenta);

            Console.WriteLine("================================= Press Any Key =================================");
            Console.ReadKey();
        }

        // 플레이어 이름 입력 메뉴
        private static void PlayerInputName()
        {
            string playerName;

            Console.Clear();
            Console.WriteLine("헤네시스에 오신걸 환영합니다.");

            wmp = new WindowsMediaPlayer();
            string executableFilePath = Assembly.GetEntryAssembly().Location;
            string executableDirectoryPath = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(executableFilePath))));
            string audioFilePath = Path.Combine(executableDirectoryPath, "henesys.wav");
            wmp.URL = audioFilePath;
            wmp.controls.play();
            wmp.settings.volume = 5;

            // 공백시 입장 불가
            do
            {
                Console.WriteLine("플레이어 이름을 입력해주세요");
                playerName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(playerName))
                {
                    Console.WriteLine("플레이어 이름은 공백일 수 없습니다.");
                }

            } while (string.IsNullOrWhiteSpace(playerName));

            SelectedJobMenu(playerName, 1);
        }

        // 직업선택 메뉴
        private static void SelectedJobMenu(string playerName, int cursor)
        {
            Console.Clear();
            Console.CursorVisible = false;
            fontColor.WriteColorFont($"{playerName}", FontColor.Color.DarkYellow);
            Console.WriteLine(" 님 반갑습니다!");
            Console.WriteLine("먼저 직업을 선택해주세요.");
            Console.WriteLine("");

            if (cursor == 1)
                HighlightText("1. 전사");
            else
                Console.WriteLine("1. 전사");
            if (cursor == 2)
                HighlightText("2. 마법사");
            else
                Console.WriteLine("2. 마법사");
            if (cursor == 3)
                HighlightText("3. 궁수");
            else
                Console.WriteLine("3. 궁수");
            Console.WriteLine(" ");

            // 임시 불러오기용 인벤토리
            List<int> items = new List<int>() { 11110101, 11412103 };
            // 캐릭터를 선택한 후 inventory, shop 생성, 사실상 게임 시작부분이기 때문에 이때 생성하여 인벤토리에
            // player를 전달하기 위함
            SetCursor(1, 3, cursor, playerName, SelectedJobMenu);
            switch (cursor)
            {
                case 1:
                    player = new Warrior("1", playerName, items);
                    inventory = new Inventory(player);
                    GameItemSetting(inventory, shop);
                    StartMenu(player.Occupation, 1);
                    break;
                case 2:
                    player = new Mage("2", playerName, items);
                    inventory = new Inventory(player);
                    GameItemSetting(inventory, shop);
                    StartMenu(player.Occupation, 1);
                    break;
                case 3:
                    player = new Archer("3", playerName, items);
                    inventory = new Inventory(player);
                    GameItemSetting(inventory, shop);
                    StartMenu(player.Occupation, 1);
                    break;
            }
        }
        private static void SetCursor(int min, int max, int cursor, string str, func1 Funcntion)
        {
            do
            {
                c = Console.ReadKey();
                switch (c.Key)
                {
                    case ConsoleKey.UpArrow:
                        cursor--;
                        if (cursor < min)
                            cursor = max;
                        Funcntion(str, cursor);
                        break;
                    case ConsoleKey.DownArrow:
                        cursor++;
                        if (cursor > max)
                            cursor = min;
                        Funcntion(str, cursor);
                        break;
                }

            } while (c.Key != ConsoleKey.Enter);
        }
        private static void SetCursor(int min, int max, int cursor, func2 Funcntion)
        {
            do
            {
                c = Console.ReadKey();
                switch (c.Key)
                {
                    case ConsoleKey.UpArrow:
                        cursor--;
                        if (cursor < min)
                            cursor = max;
                        Funcntion(cursor);
                        break;
                    case ConsoleKey.DownArrow:
                        cursor++;
                        if (cursor > max)
                            cursor = min;
                        Funcntion(cursor);
                        break;
                }

            } while (c.Key != ConsoleKey.Enter);
        }
        private static void SetCursor(int min, int max, int cursor, int idx, func3 Funcntion)
        {
            do
            {
                c = Console.ReadKey();
                switch (c.Key)
                {
                    case ConsoleKey.UpArrow:
                        cursor--;
                        if (cursor < min)
                            cursor = max;
                        Funcntion(idx, cursor);
                        break;
                    case ConsoleKey.DownArrow:
                        cursor++;
                        if (cursor > max)
                            cursor = min;
                        Funcntion(idx, cursor);
                        break;
                }

            } while (c.Key != ConsoleKey.Enter);
        }
        public static void HighlightText(string str)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(str);
            Console.ResetColor();
        }
        // 시작 메뉴
        public static void StartMenu(string occupation, int cursor)
        {
            Console.Clear();
            fontColor.WriteColorFont($"{occupation}", FontColor.Color.DarkYellow);
            Console.WriteLine(" 을(를) 선택하셨습니다.");
            Console.WriteLine("");
            Console.WriteLine("던전에 입장하시기 전 정비할 수 있습니다.");
            Console.WriteLine("선택지 중 하나를 선택해 주세요");
            Console.WriteLine("");

            if (cursor == 1)
                HighlightText("1. 상태 보기");
            else
                Console.WriteLine("1. 상태 보기");
            if (cursor == 2)
                HighlightText("2. 인벤토리");
            else
                Console.WriteLine("2. 인벤토리");
            if (cursor == 3)
                HighlightText("3. 상점");
            else
                Console.WriteLine("3. 상점");
            Console.WriteLine("");

            if (cursor == 4)
            {
                Console.BackgroundColor = ConsoleColor.White;
                fontColor.WriteColorFont("4. 던전입장", FontColor.Color.DarkRed);
                Console.WriteLine("\n");
            }
            else
            {
                fontColor.WriteColorFont("4. 던전입장", FontColor.Color.DarkRed);
                Console.WriteLine("\n");

            }

            SetCursor(1, 4, cursor, occupation, StartMenu);

            switch (cursor)
            {
                case 1:
                    StatusMenu();
                    break;
                case 2:
                    InventoryMenu(1);
                    break;
                case 3:
                    ShopMenu(1);
                    break;
                case 4:
                    battle = new Battle(player, inventory);
                    battle.BattleScene(1);
                    //StageSelected();
                    break;
            }

        }

        // 무기와 방어구의 스탯 계산
        private static (float bonusAtk, float bonusDef) PlusStatus()
        {
            List<Items> bonusItem = inventory.invenItems;
            var weapons = bonusItem.OfType<Weapon>().ToList();
            var armors = bonusItem.OfType<Armor>().ToList();

            float bonusAtk = 0;
            float bonusDef = 0;

            // 무기 합 계산
            foreach (var weapon in weapons)
            {
                bonusAtk = weapon.BonusStatus(inventory);
                break;
            }

            // 방어구 합 계산
            foreach (var armor in armors)
            {
                bonusDef = armor.BonusStatus(inventory);
                break;
            }

            // 총 합을 계산하여 plusAtk, Def설정
            player.PlusAtk = player.Atk + bonusAtk;
            player.PlusDef = player.Def + bonusDef;

            return (bonusAtk, bonusDef);
        }


        //상태 메뉴
        private static void StatusMenu()
        {
            var (bonusAtk, bonusDef) = PlusStatus();

            string playerLevel = player.Level.ToString("00");

            Console.Clear();
            fontColor.WriteColorFont("[ 상태 정보 ]", FontColor.Color.Magenta);
            Console.WriteLine("\n");
            Console.WriteLine("캐릭터의 정보를 표기하는 곳입니다.");
            Console.WriteLine("");

            Console.Write("플레이어 이름: ");
            fontColor.WriteColorFont($"{player.Name} ", FontColor.Color.DarkYellow);
            fontColor.WriteColorFont($"({player.Occupation})", FontColor.Color.DarkGreen);
            Console.WriteLine("\n");

            Console.Write("LV: ");
            fontColor.WriteColorFont($"{playerLevel}", FontColor.Color.Yellow);
            Console.WriteLine("\n");
            Console.WriteLine($"경험치: {player.Exp} / {player.MaxExp}");
            Console.WriteLine("");

            Console.Write($"공격력: {player.PlusAtk} + ");
            Console.Write("(");
            fontColor.WriteColorFont($"{bonusAtk}", FontColor.Color.Cyan);
            Console.Write(")");
            Console.WriteLine("");

            Console.Write($"방어력: {player.PlusDef} + ");
            Console.Write("(");
            fontColor.WriteColorFont($"{bonusDef}", FontColor.Color.Cyan);
            Console.Write(")");
            Console.WriteLine("");

            Console.WriteLine($"체력: {player.Health} / {player.MaxHealth}");
            Console.WriteLine($"마나: {player.Mana} / {player.MaxMana}");
            Console.WriteLine($"소지골드: {player.Gold}");
            Console.WriteLine(" ");

            Console.WriteLine("Press Any Key...");
            Console.WriteLine("");
            Console.ReadKey();
            StartMenu(player.Occupation, 1);

        }

        // 인벤토리 메뉴
        private static void InventoryMenu(int cursor)
        {
            Console.Clear();
            fontColor.WriteColorFont("[ 인벤토리 ]", FontColor.Color.Magenta);
            Console.WriteLine("\n");
            Console.WriteLine("아이템을 관리할 수 있습니다.");
            Console.WriteLine("");
            inventory.DisplayInventory();
            Console.WriteLine("");

            if (cursor == 1)
                HighlightText("1. 장비 관리하기");
            else
                Console.WriteLine("1. 장비 관리하기");
            if (cursor == 2)
                HighlightText("2. 아이템 사용하기");
            else
                Console.WriteLine("2. 아이템 사용하기");
            if (cursor == 3)
                HighlightText("3. 아이템 버리기");
            else
                Console.WriteLine("3. 아이템 버리기");
            if (cursor == 4)
                HighlightText("4. 아이템 정렬");
            else
                Console.WriteLine("4. 아이템 정렬");
            Console.WriteLine("");
            Console.WriteLine("");
            if (cursor == 0)
                HighlightText("0. 뒤로가기");
            else
                Console.WriteLine("0. 뒤로가기");
            Console.WriteLine("");
            SetCursor(0, 4, cursor, InventoryMenu);

            switch (cursor)
            {
                case 1:
                    EquipMenu();
                    break;
                case 2:
                    InventoryItemUseMenu(1);
                    break;
                case 3:
                    DropItemMenu(1);
                    break;
                case 4:
                    inventory.InventoryArraySort();
                    InventoryMenu(1);
                    break;
                case 0:
                    StartMenu(player.Occupation, 1);
                    break;
            }

        }

        //아이템 사용하기 메뉴 
        private static void InventoryItemUseMenu(int cursor)
        {
            Console.Clear();
            fontColor.WriteColorFont("[ 아이템 사용하기 ]", FontColor.Color.Magenta);
            Console.WriteLine("\n");
            Console.WriteLine("어떤 아이템을 사용하시겠습니까?");
            Console.WriteLine("");

            Console.WriteLine("번호를 입력해주세요");
            Console.WriteLine("");
            inventory.ShowInvenItem(2);
            Console.WriteLine("");

            //if (cursor == 0)
            //    HighlightText("0. 뒤로가기");
            //else
            Console.WriteLine("0. 뒤로가기");
            Console.WriteLine("");

            //SetCursor(0,inventory.potionItems.Count, cursor, InventoryItemUseMenu);
            int keyInput = CheckValidInput(0, inventory.ItemCnt);
            switch (keyInput)
            {
                case 0:
                    InventoryMenu(keyInput);
                    break;
                default:
                    ItemUsed(keyInput - 1);
                    break;
            }
        }
        //아이템 사용시 확인메뉴
        private static void ItemUsed(int indexItem)
        {
            Console.Clear();
            Console.WriteLine("");
            if (inventory.potionItems != null)
            {
                if (inventory.potionItems[indexItem].Kind == "힐링포션")//힐링포션부분
                {
                    if (player.Health >= player.MaxHealth)
                    {
                        player.Health = player.MaxHealth;
                        Console.WriteLine("체력이 가득 찼습니다.");
                    }
                    else
                    {
                        Console.WriteLine("힐링포션을 사용했습니다.");
                        int beforeHp = player.Health;
                        inventory.potionItems[indexItem].Use(player);
                        if (player.Health >= player.MaxHealth)
                            player.Health = player.MaxHealth;
                        fontColor.WriteColorFont($"물약을 사용하였습니다. HP : {beforeHp} -> {player.Health}", FontColor.Color.Blue);
                        Console.WriteLine("");
                        inventory.invenItems.Remove(inventory.potionItems[indexItem]);
                        inventory.potionItems.RemoveAt(indexItem);
                    }
                }
                else if (inventory.potionItems[indexItem].Kind == "마나포션")//마나포션부분
                {
                    if (player.Mana >= player.MaxMana)
                    {
                        player.Mana = player.MaxMana;
                        Console.WriteLine("마나가 가득 찼습니다.");
                    }
                    else
                    {
                        Console.WriteLine("마나포션을 사용했습니다.");
                        int beforeMP = player.Mana;
                        inventory.potionItems[indexItem].Use(player);
                        if (player.Mana >= player.MaxMana)
                            player.Mana = player.MaxMana;
                        fontColor.WriteColorFont($"물약을 사용하였습니다. HP : {beforeMP} -> {player.Mana}", FontColor.Color.Blue);
                        Console.WriteLine("");
                        inventory.invenItems.Remove(inventory.potionItems[indexItem]);
                        inventory.potionItems.RemoveAt(indexItem);
                    }
                }
            }
            else
            {
                Console.WriteLine("포션이 없습니다!");
            }
            Console.WriteLine("");
            Console.WriteLine(" Press Any Key . . .");
            Console.ReadKey();
            InventoryItemUseMenu(1);
        }
        //아이템 버리기 메뉴
        private static void DropItemMenu(int cursor)
        {
            Console.Clear();
            fontColor.WriteColorFont("[ 아이템 버리기 ]", FontColor.Color.Magenta);
            Console.WriteLine("\n");
            Console.WriteLine("어떤 아이템을 버리시겠습니까?");
            Console.WriteLine("");

            Console.WriteLine("번호를 입력해주세요");
            Console.WriteLine(" ");

            Console.WriteLine($"아이템개수: {inventory.ItemCnt}");

            Console.WriteLine("");
            inventory.ShowInvenItem(0);
            Console.WriteLine("");

            //if (cursor == 0)
            //    HighlightText("0. 뒤로가기");
            //else
            Console.WriteLine("0. 뒤로가기");
            Console.WriteLine("");

            int keyInput = CheckValidInput(0, inventory.ItemCnt);
            //SetCursor(0, inventory.ItemCnt, cursor, DropItemMenu);
            switch (keyInput)
            {
                case 0:
                    InventoryMenu(1);
                    break;
                default:
                    IsRemoveItem(keyInput - 1, 1);
                    break;
            }
        }

        //아이템 버리기 경고
        private static void IsRemoveItem(int itemIndex, int cursor)
        {
            Console.Clear();
            Console.WriteLine("정말 아이템을 삭제하시겠습니까?");
            Console.WriteLine("");
            inventory.CurrentRemoveItem(itemIndex);
            Console.WriteLine("");

            if (cursor == 1)
                HighlightText("1. 네");
            else
                Console.WriteLine("1. 네");
            if (cursor == 2)
                HighlightText("2. 아니오");
            else
                Console.WriteLine("2. 아니오");
            Console.WriteLine("");
            SetCursor(1, 2, cursor, itemIndex, IsRemoveItem);
            switch (cursor)
            {
                case 1:
                    inventory.RemoveItem(itemIndex);
                    DropItemMenu(1);
                    break;
                case 2:
                    DropItemMenu(1);
                    break;
            }
        }

        //장비 관리
        private static void EquipMenu()
        {
            PlusStatus();

            Console.Clear();
            fontColor.WriteColorFont("[ 장비 관리 ]", FontColor.Color.Magenta);
            Console.WriteLine("\n");
            Console.WriteLine("아이템을 장착 및 해제 할 수 있습니다.");
            Console.WriteLine("");
            Console.WriteLine("장착할 아이템의 번호를 입력해주세요");

            Console.WriteLine("");
            inventory.onEquipMenu = true;
            inventory.DisplayInventory();

            Console.WriteLine("");
            Console.WriteLine("0. 뒤로가기");
            Console.WriteLine("");

            int equipNum = CheckValidInput(0, inventory.ItemCnt);
            
            if (equipNum == 0)
            {
                inventory.onEquipMenu = false;
                InventoryMenu(1);
            }
            else
            {
                inventory.EquipmentStatusChange(equipNum - 1);
                EquipMenu();
            }
        }

        // 상점 메뉴
        private static void ShopMenu(int cursor)
        {
            Console.Clear();
            fontColor.WriteColorFont("[ 상점 ]", FontColor.Color.Magenta);
            Console.WriteLine("\n");
            Console.WriteLine("상점에 있는 아이템을 사고 팔 수 있습니다.");
            Console.WriteLine("");

            shop.DisplayShop();
            Console.WriteLine("");

            if (cursor == 1)
                HighlightText("1. 아이템 사기");
            else
                Console.WriteLine("1. 아이템 사기");
            if (cursor == 2)
                HighlightText("2. 아이템 팔기");
            else
                Console.WriteLine("2. 아이템 팔기");
            Console.WriteLine("");
            if (cursor == 0)
                HighlightText("0. 뒤로가기");
            else
                Console.WriteLine("0. 뒤로가기");
            SetCursor(0, 2, cursor, ShopMenu);

            switch (cursor)
            {
                case 1:
                    BuyShopMenu();
                    break;
                case 2:
                    SellShopMenu();
                    break;
                case 0:
                    StartMenu(player.Occupation, 1);
                    break;
            }

        }

        // 아이템 사기 메뉴
        private static void BuyShopMenu()
        {
            Console.Clear();
            fontColor.WriteColorFont("[ 아이템 구매 ]", FontColor.Color.Magenta);
            Console.WriteLine("\n");
            Console.WriteLine("구매하고 싶은 아이템을 선택해주세요");
            Console.WriteLine($"상점의 아이템 개수: {shop.ShopItemCnt}");
            Console.WriteLine("");

            shop.BuyShopItem();
            Console.WriteLine("");

            //if (cursor == 0)
            //    HighlightText("0. 뒤로가기");
            //else
            Console.WriteLine("0. 뒤로가기");
            Console.WriteLine("");
            //SetCursor(0, shop.ShopItemCnt, cursor, BuyShopMenu);
            int keyInput = CheckValidInput(0, shop.ShopItemCnt);

            switch (keyInput)
            {
                case 0:
                    ShopMenu(1);
                    break;
                default:
                    if (player.Gold == 0 || player.Gold < shop.ShopItemPrice(keyInput - 1))
                    {
                        Console.WriteLine("소지금이 부족합니다!");
                        Console.WriteLine($"현재 소지금액: {player.Gold}");
                        Console.WriteLine("아무키나 입력하시면 상점으로 이동합니다.");
                        Console.ReadKey();
                        ShopMenu(1);
                    }
                    else
                    {
                        shop.BuyItemAddInventory(player, inventory, keyInput - 1);
                        IsMoreBuyItem(1);
                    }
                    break;
            }
        }

        // 아이템을 더 구매할지 여부
        private static void IsMoreBuyItem(int cursor)
        {
            Console.Clear();
            Console.WriteLine("아이템을 더 구매하시겠습니까?");
            Console.WriteLine("");

            if (player.Gold < 1500)
            {
                Console.Write("소지 금액: ");
                fontColor.WriteColorFont($"{player.Gold}", FontColor.Color.DarkRed);
            }
            else
            {
                Console.Write($"소지 금액: {player.Gold}");
            }

            Console.WriteLine("\n");

            if (cursor == 1)
                HighlightText("1. 더 구매한다.");
            else
                Console.WriteLine("1. 더 구매한다.");
            if (cursor == 2)
                HighlightText("2. 시작메뉴로 돌아간다.");
            else
                Console.WriteLine("2. 시작메뉴로 돌아간다.");
            Console.WriteLine("");
            SetCursor(1, 2, cursor, IsMoreBuyItem);
            switch (cursor)
            {
                case 1:
                    BuyShopMenu();
                    break;
                case 2:
                    StartMenu(player.Occupation, 1);
                    break;
            }

        }

        // 아이템 팔기 메뉴
        private static void SellShopMenu()
        {
            Console.Clear();
            fontColor.WriteColorFont("[ 아이템 판매 ]", FontColor.Color.Magenta);
            Console.WriteLine("\n");
            Console.WriteLine("판매하고 싶은 아이템을 선택해주세요");
            Console.WriteLine($"인벤토리 아이템 개수: {inventory.ItemCnt}");
            Console.WriteLine("");

            inventory.ShowInvenItem(0);
            Console.WriteLine("");

            //if (cursor == 0)
            //    HighlightText("0. 뒤로가기");
            //else
            Console.WriteLine("0. 뒤로가기");
            Console.WriteLine("");

            //SetCursor(0, inventory.ItemCnt, cursor, SellShopMenu);
            int keyInput = CheckValidInput(0, inventory.ItemCnt);

            switch (keyInput)
            {
                case 0:
                    ShopMenu(1);
                    break;
                default:
                    if (IsRealSellItem(1))
                    {
                        inventory.SellItem(player, keyInput - 1);
                        ShopMenu(1);
                    }
                    else
                    {
                        Console.WriteLine("판매되지 않았습니다.");
                        SellShopMenu();
                    }
                    break;
            }
        }

        // 정말 판매할지 안할지 선택
        private static bool IsRealSellItem(int cursor)
        {
            Console.Clear();
            Console.WriteLine("정말 판매하시겠습니까?");
            Console.WriteLine($"선택된 아이템: ");
            Console.WriteLine("");

            if (cursor == 1)
                HighlightText("1. 판매한다.");
            else
                Console.WriteLine("1. 판매한다.");
            if (cursor == 2)
                HighlightText("2. 판매하지 않는다.");
            else
                Console.WriteLine("2. 판매하지 않는다.");

            do
            {
                c = Console.ReadKey();
                switch (c.Key)
                {
                    case ConsoleKey.UpArrow:
                        cursor--;
                        if (cursor < 1)
                            cursor = 2;
                        IsRealSellItem(cursor);
                        break;
                    case ConsoleKey.DownArrow:
                        cursor++;
                        if (cursor > 2)
                            cursor = 1;
                        IsRealSellItem(cursor);
                        break;
                }
            } while (c.Key != ConsoleKey.Enter);

            switch (cursor)
            {
                case 1:
                    return true;
                case 2:
                    return false;
            }

            return false;
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

            } while (result = false || CheckIfValid(keyInput, min, max) == false);

            return keyInput;

        }

        private static bool CheckIfValid(int keyInput, int min, int max)
        {
            if (min <= keyInput && max >= keyInput)
            {
                return true;
            }
            return false;
        }


        // 메인
        static void Main(string[] args)
        {
            shop = new Shop();

            fontColor = new FontColor();
            //Console.SetWindowSize(82, 30);
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
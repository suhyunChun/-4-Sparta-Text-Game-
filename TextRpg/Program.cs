using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Reflection.Emit;
using TextRpg.Appearance;
using System.Numerics;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TextRpg.InvenShop;
using TextRpg.Item;
using TextRpg.Player;
using WMPLib;
using System.Diagnostics.Metrics;
using System.Threading;


namespace TextRpg
{
    internal class Program
    {
        // 경로 설정
        static DirectoryInfo Domain;
        static string[] domainNum;
        static List<Items> item;
        static Inventory inventory;
        static Shop shop;
        // public이 붙음, player생성 시 다양한 곳에서 땡겨오기 위해
        public static Job player;
        public static string Pname;
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
        // 아이템들
        // 무기
        public static Weapon Sword1 = new Weapon(11110101, "나뭇가지", 1, 100, 3, false);
        public static Weapon Sword2 = new Weapon(11110102, "낡은 회초리", 1, 100, 4, false);
        public static Weapon Sword3 = new Weapon(11110103, "낡은 검", 1, 100, 12, false);
        public static Weapon RareSword1 = new Weapon(21110101, "황금 검", 2, 300, 20, false);
        public static Weapon RareSword2 = new Weapon(21110102, "폭풍의 창", 2, 500, 25, false);
        public static Weapon RareSword3 = new Weapon(21110103, "어둠의 활", 2, 1000, 28, false);
        public static Weapon RareSword4 = new Weapon(21110104, "영원의 대검", 3, 2000, 35, false);
        // 방어구
        public static Armor Armor1 = new Armor(11210101, "낡은 방패", 1, 100, 10, false);
        public static Armor Armor2 = new Armor(11210102, "녹슨 방패", 1, 100, 11, false);
        public static Armor Armor3 = new Armor(11210103, "빛바랜 방패", 1, 100, 11, false);
        public static Armor RareArmor1 = new Armor(21210101, "황금 방패", 2, 300, 15, false);
        public static Armor RareArmor2 = new Armor(21210102, "신령의 갑옷", 2, 500, 25, false);
        public static Armor RareArmor3 = new Armor(21210103, "대지의 방패", 2, 700, 30, false);
        public static Armor RareArmor4 = new Armor(21210104, "영원의 갑옷", 3, 1400, 45, false);
        // 체력 포션
        public static HealingPotion HPPotion1 = new HealingPotion(11412101, "일반 회복 물약", 1, 100, 10, false);
        public static HealingPotion RareHPPotion1 = new HealingPotion(21410101, "고급 회복 물약", 2, 200, 20, false);
        public static HealingPotion RareHPPotion2 = new HealingPotion(21410102, "고오급 회복 물약", 2, 1000000, 2000, false);
        // 마나 포션
        public static ManaPotion MPPotion1 = new ManaPotion(11510101, "마나 회복 물약", 1, 100, 10, false);
        // 더미
        public static HealingPotion Dummy = new HealingPotion(0, null, 0, 0, 0, false);


        private static void FirstInventorySetting(Inventory inventory)
        {
            inventory.AddItem(Sword1, player);
            inventory.AddItem(Sword2, player);
            inventory.AddItem(Sword3, player);

            inventory.AddItem(Armor1, player);
            inventory.AddItem(Armor2, player);
            inventory.AddItem(Armor3, player);
            inventory.AddItem(HPPotion1, player);
            inventory.AddItem(HPPotion1, player);
            inventory.AddItem(MPPotion1, player);
            inventory.AddItem(MPPotion1, player);
        }
        // 아이템 탐색 등록
        private static Items ItemFind(int i)
        {
            Items j = Dummy;
            if(Sword1.Id == i)
            {
                j = Sword1;
            }
            else if (Sword2.Id == i)
            {
                j = Sword2;
            }
            else if (Sword3.Id == i)
            {
                j = Sword3;
            }
            else if (RareSword1.Id == i)
            {
                j = RareSword1;
            }
            else if (RareSword2.Id == i)
            {
                j = RareSword2;
            }
            else if (RareSword3.Id == i)
            {
                j = RareSword3;
            }
            else if (RareSword4.Id == i)
            {
                j = RareSword4;
            }
            else if (Armor1.Id == i)
            {
                j = Armor1;
            }
            else if (Armor2.Id == i)
            {
                j = Armor2;
            }
            else if (Armor3.Id == i)
            {
                j = Armor3;
            }
            else if (RareArmor1.Id == i)
            {
                j = RareArmor1;
            }
            else if (RareArmor2.Id == i)
            {
                j = RareArmor2;
            }
            else if (RareArmor3.Id == i)
            {
                j = RareArmor3;
            }
            else if (RareArmor4.Id == i)
            {
                j = RareArmor4;
            }
            else if (HPPotion1.Id == i)
            {
                j = HPPotion1;
            }
            else if (RareHPPotion1.Id == i)
            {
                j = RareHPPotion1;
            }
            else if (RareHPPotion2.Id == i)
            {
                j = RareHPPotion2;
            }
            else if (MPPotion1.Id == i)
            {
                j = MPPotion1;
            }
            return j;
        }

        private static void LoadInventorySetting(Inventory inventory, Job player)
        {
            List<int> Items = new List<int>();
            foreach (int i in player.Item)
            {
                Items.Add(i);
            }
            foreach (int i in Items)
            {
                // 아이템 추가 목록
                Items load = ItemFind(i);
                inventory.invenItems.Add(load);
                if (load is HealingPotion || load is ManaPotion)
                {
                    inventory.potionItems.Add(load);
                }
                else if (load is Weapon || load is Armor)
                {
                    inventory.equipmentItems.Add(load);
                }
            }
            for(int i = 0; i < inventory.invenItems.Count; i++)
            {
                if (player.EquippedList[i])
                    inventory.invenItems[i].IsEquiped = true;
            }
        }

        private static void GameItemSetting(Shop shop)
        {
            shop.AddShopItem(RareSword1);
            shop.AddShopItem(RareSword2);
            shop.AddShopItem(RareSword3);
            shop.AddShopItem(RareSword4);
            shop.AddShopItem(RareArmor1);
            shop.AddShopItem(RareArmor2);
            shop.AddShopItem(RareArmor3);
            shop.AddShopItem(RareArmor4);
            shop.AddShopItem(RareHPPotion1);
            shop.AddShopItem(RareHPPotion2);

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
            string audioFilePath = Path.Combine(executableDirectoryPath, "Sounds", "henesys.wav");
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
            List<int> items = new List<int>();
            List<bool> tmp = new List<bool>();
            // 캐릭터를 선택한 후 inventory, shop 생성, 사실상 게임 시작부분이기 때문에 이때 생성하여 인벤토리에
            // player를 전달하기 위함
            SetCursor(1, 3, cursor, playerName, SelectedJobMenu);
            switch (cursor)
            {
                case 1:
                    int Str = 4;
                    int Agi = 2;
                    int Int = 2;
                    int hp = 100 + Str * 20;
                    int mp = 10 + Int * 2;
                    player = new Warrior(Pname, playerName, 1, 0, 10, Str, Agi, Int, hp, mp, 3000, items, tmp);
                    inventory = new Inventory(player);
                    Registration(Pname, Domain);
                    StartMenu(player.Occupation, 1);
                    break;
                case 2:
                    Str = 2;
                    Agi = 4;
                    Int = 2;
                    hp = 100 + Str * 20;
                    mp = 10 + Int * 2;
                    player = new Mage(Pname, playerName, 1, 0, 10, Str, Agi, Int, hp, mp, 3000, items, tmp);
                    inventory = new Inventory(player);
                    Registration(Pname, Domain);
                    StartMenu(player.Occupation, 1);
                    break;
                case 3:
                    Str = 2;
                    Agi = 2;
                    Int = 4;
                    hp = 100 + Str * 20;
                    mp = 10 + Int * 2;
                    player = new Archer(Pname, playerName, 1, 0, 10, Str, Agi, Int, hp, mp, 3000, items, tmp);
                    inventory = new Inventory(player);
                    Registration(Pname, Domain);
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
            if (cursor == 4)
                HighlightText("4. 데이터 저장");
            else
                Console.WriteLine("4. 데이터 저장");
            Console.WriteLine("");

            if (cursor == 5)
            {
                Console.BackgroundColor = ConsoleColor.White;
                fontColor.WriteColorFont("5. 던전입장", FontColor.Color.DarkRed);
                Console.WriteLine("\n");
            }
            else
            {
                fontColor.WriteColorFont("5. 던전입장", FontColor.Color.DarkRed);
                Console.WriteLine("\n");

            }

            SetCursor(1, 5, cursor, occupation, StartMenu);

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
                    SaveData(1);
                    break;
                case 5:
                    battle = new Battle(player, inventory);
                    battle.BattleScene(1);
                    //StageSelected();
                    break;
            }

        }

        private static void SaveData(int cursor)
        {
            Console.Clear();
            var (bonusAtk, bonusDef) = PlusStatus();
            string playerLevel = player.Level.ToString("00");

            fontColor.WriteColorFont("[ 데이터 저장 ]", FontColor.Color.Magenta);
            Console.WriteLine("\n");
            Console.WriteLine("캐릭터의 정보를 저장하는 곳입니다.");
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

            Console.WriteLine("데이터를 저장하시겠습니까?");

            if (cursor == 1)
                HighlightText("1. 네");
            else
                Console.WriteLine("1. 네");
            if (cursor == 2)
                HighlightText("2. 아니요");
            else
                Console.WriteLine("2. 아니요");
            SetCursor(1, 2, cursor, SaveData);
            switch (cursor)
            {
                case 1:
                    Save(1);
                    break;
                case 2:
                    StartMenu(player.Occupation, 1);
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
                if(weapon != null && weapon.IsEquiped == true)
                {
                    bonusAtk = weapon.BonusStatus(inventory);
                }
            }

            // 방어구 합 계산
            foreach (var armor in armors)
            {
                if(armor != null && armor.IsEquiped == true)
                {
                    bonusDef = armor.BonusStatus(inventory);
                }
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
                        player.Item.Remove(inventory.potionItems[indexItem].Id);
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
                        fontColor.WriteColorFont($"물약을 사용하였습니다. MP : {beforeMP} -> {player.Mana}", FontColor.Color.Blue);
                        Console.WriteLine("");
                        player.Item.Remove(inventory.potionItems[indexItem].Id);
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
                    inventory.RemoveItem(itemIndex, player);
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
            Console.WriteLine();
            Console.Write($"상점의 아이템 개수: ");
            fontColor.WriteColorFont($"{shop.ShopItemCnt}", FontColor.Color.Blue);
            Console.WriteLine("\n");

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
            fontColor.WriteColorFont("판매 시, 80%의 금액을 받을 수 있습니다.", FontColor.Color.DarkRed);
            Console.WriteLine();
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

        // 계정 폴더 생성
        private static DirectoryInfo FolderSet()
        {
            return new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"save");
        }
        private static string[] Folder()
        {
            if (!Domain.Exists)
            {
                Domain.Create();
            }
            return Directory.GetFiles(Domain.ToString(), "*.json");
        }

        // 계정 등록
        private static void Registration(string id, DirectoryInfo domain)
        {

            
            JObject account = new JObject();
            JArray itemlist = new JArray();
            JArray equiplist = new JArray();
            Job memberPath;

            

            account.Add("Id", id);
            account.Add("Name", player.Name);
            account.Add("Occupation", player.Occupation);
            account.Add("Level", 1);
            account.Add("MaxExp", 10);
            account.Add("Exp", 0);
            account.Add("Strength", player.Strength);
            account.Add("Agility", player.Agility);
            account.Add("Intelligence", player.Intelligence);
            account.Add("Health", player.MaxHealth);
            account.Add("Mana", player.MaxMana);
            account.Add("PlusAtk", player.PlusAtk);
            account.Add("PlusDef", player.PlusDef);
            account.Add("Gold", 3000);
            account.Add("Weapon", player.Weapon);
            account.Add("Armor", player.Armor);

            FirstInventorySetting(inventory);

            foreach (int item in player.Item)
            {
                itemlist.Add(item);
                equiplist.Add(false);
            }

            account.Add("Item", itemlist);
            account.Add("EquippedList", equiplist);

            // 파일 경로 설정
            string fileName = $"{id}.json";
            string directory = domain.FullName + @"\" + fileName;
            // 데이터 파일 생성 (프로젝트 이름\bin\Debug\net6.0 경로에 저장)
            File.WriteAllText(directory, account.ToString());
        }

        // 로그인
        private static void Login(int cursor)
        {
            Console.Clear();
            Console.WriteLine("헤네시스에 오신걸 환영합니다.");
            Console.WriteLine("저장된 기록을 불러옵니다.");
            // 변수 지정
            FileInfo[] saves = Domain.GetFiles();
            List<string> saveId = new List<string> { };
            string savePath;
            string json;
            Job memberPath;

            // 시작
            Console.WriteLine("아이디를 선택해주세요.");
            Console.WriteLine();
            // 아이디 목록 검색
            for (int num = 0; num < saves.Length; num++)
            {
                // 검색된 아이디 불러오기
                savePath = saves[num].FullName;
                json = File.ReadAllText(savePath);
                memberPath = JsonConvert.DeserializeObject<Job>(json);
                saveId.Add(memberPath.Id);
                // 커서 적용 필요
                if(cursor==num+1)
                    HighlightText($"{num + 1}. {memberPath.Id}   ");
                else
                    Console.WriteLine($"{num + 1}. {memberPath.Id}   ");
            }
            SetCursor(1, saves.Length, cursor, Login);
            // 아이디 선택
            Console.WriteLine();

            // 아이디 다시 불러오기
            int select = cursor - 1;
            savePath = saves[select].FullName;
            json = File.ReadAllText(savePath);
            memberPath = JsonConvert.DeserializeObject<Job>(json);
            switch (memberPath.Occupation)
            {
                case "전사":
                    player = JsonConvert.DeserializeObject<Warrior>(json);
                    break;
                case "마법사":
                    player = JsonConvert.DeserializeObject<Mage>(json);
                    break;
                case "궁수":
                    player = JsonConvert.DeserializeObject<Archer>(json);
                    break;
            }
            inventory = new Inventory(player);
            LoadInventorySetting(inventory, memberPath);
            // 아이디 확정 후 리턴
            Console.WriteLine($"ID : {memberPath.Id} \n캐릭터 명 : {memberPath.Name} 을(를) 불러 옵니다.");
            Thread.Sleep(2000);
            StartMenu(player.Occupation, 1);
        }

        // 계정 저장
        private static void Save(int cursor)
        {
            Console.Clear();
            // 변수 지정
            JObject account = new JObject();
            JArray itemlist = new JArray();
            JArray equiplist = new JArray();
            // 파일 경로 설정
            string fileName = $"{player.Id}.json";
            string directory = Domain.FullName + @"\" + fileName;
            // 시작
            Console.WriteLine("저장하기");
            Console.WriteLine("자신의 캐릭터를 저장합니다.");
            fontColor.WriteColorFont("단, 사용한 물약의 효과는 저장되지 않습니다.", FontColor.Color.Red);
            Console.WriteLine("저장하시겠습니까?");
            Console.WriteLine();
            // 커서 적용 필요

            if (cursor == 1)
                HighlightText("1. 네");
            else
                Console.WriteLine("1. 네");
            if (cursor == 2)
                HighlightText("2. 아니요");
            else
                Console.WriteLine("2. 아니요");
            SetCursor(1, 2, cursor, Save);
            switch (cursor)
            {
                case 1:
                    // 아이템 저장 시작
                    foreach (var item in player.Item)
                    {
                        itemlist.Add(item);
                    }

                    // 아이템 장착 여부
                    foreach (var item in player.EquippedList)
                    {
                        equiplist.Add(item);
                    }
                    // 값 저장
                    account.Add("Id", player.Id);
                    account.Add("Name", player.Name);
                    account.Add("Occupation", player.Occupation);
                    account.Add("Level", player.Level);
                    account.Add("MaxExp", player.MaxExp);
                    account.Add("Exp", player.Exp);
                    account.Add("Strength", player.Strength);
                    account.Add("Agility", player.Agility);
                    account.Add("Intelligence", player.Intelligence);
                    account.Add("Health", player.Health);
                    account.Add("Mana", player.Mana);
                    account.Add("PlusAtk", player.PlusAtk);
                    account.Add("PlusDef", player.PlusDef);
                    account.Add("Gold", player.Gold);
                    account.Add("Weapon", player.Weapon);
                    account.Add("Armor", player.Armor);

                    //FirstInventorySetting(inventory);

                    account.Add("Item", itemlist);
                    account.Add("EquippedList", equiplist);

                    // 데이터 덮어쓰기 (프로젝트 이름\bin\Debug\net6.0 경로에 저장)
                    File.Delete(directory);
                    File.WriteAllText(directory, account.ToString());

                    string json = File.ReadAllText(directory);
                    Job memberPath = JsonConvert.DeserializeObject<Job>(json);
                    player = memberPath;

                    fontColor.WriteColorFont("정상적으로 저장되었습니다.", FontColor.Color.Blue);
                    Thread.Sleep(2000);
                    StartMenu(player.Occupation, 1);
                    break;
                case 2:
                    StartMenu(player.Occupation, 1);
                    break;
            }
        }
        private static void DataSetting(int cursor)
        {
            // 아이디 입력
            Console.Clear();
            if (domainNum.Length == 0)
            {
                Console.WriteLine("저장된 기록이 없습니다. ID를 입력해주세요.");
                Pname = Console.ReadLine();
                Console.WriteLine("정상적으로 등록되었습니다.");
                Thread.Sleep(2000);

                PlayerInputName();
                // 플레이어 재정의
                //player = Registration(Pname, Domain);
                //StartMenu(player.Occupation, 1);
            }
            else
            {
                Console.WriteLine("저장된 기록이 존재합니다.");
                // 커서 적용 필요
                if (cursor == 1)
                    HighlightText("1. 불러오기");
                else
                    Console.WriteLine("1. 불러오기");
                if (cursor == 2)
                    HighlightText("2. 새로 만들기");
                else
                    Console.WriteLine("2. 새로 만들기");
                Console.WriteLine("");
                SetCursor(1, 2, cursor, DataSetting);
                //string input = Console.ReadLine();
                switch (cursor)
                {
                    case 1:
                        wmp = new WindowsMediaPlayer();
                        string executableFilePath = Assembly.GetEntryAssembly().Location;
                        string executableDirectoryPath = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(executableFilePath))));
                        string audioFilePath = Path.Combine(executableDirectoryPath, "Sounds", "henesys.wav");
                        wmp.URL = audioFilePath;
                        wmp.controls.play();
                        wmp.settings.volume = 5;
                        // 플레이어 재정의
                        Login(1);
                        break;
                    case 2:
                        Console.WriteLine("새 기록을 새로 만듭니다.");
                        Console.WriteLine("이미 존재하는 기록의 이름을 입력 시 덮어쓰기가 됩니다.");
                        Console.WriteLine("ID를 입력해주세요.");
                        Pname = Console.ReadLine();
                        Console.WriteLine("정상적으로 등록되었습니다.");
                        Thread.Sleep(2000);
                        PlayerInputName();
                        // 플레이어 재정의
                        break;
                }
            }
        }
        // 메인
        static void Main(string[] args)
        {
            shop = new Shop();
            GameItemSetting(shop);
            Domain = FolderSet();
            domainNum = Folder();

            fontColor = new FontColor();
            //Console.SetWindowSize(82, 30);
            PrintStartScene();
                        

            DataSetting(1);





            //inventory.DisplayInventory();
            //shop.DisplayInventory();
            //Console.WriteLine(" ");
            //_player = new Warrior(playerName);
            //Console.WriteLine($"플레이어 이름 {_player.Name}");


        }
    }
}
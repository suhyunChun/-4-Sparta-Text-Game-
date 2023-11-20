using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRpg.InvenShop;
using TextRpg.Item;
using TextRpg.Player;
using static System.Net.Mime.MediaTypeNames;

namespace TextRpg
{
    class Battle
    {
        //몬스터 List
        List<Mob> mobs;
        List<Items> items;
        Mob mob;
        Job player;
        Inventory inventory;
        int originalHP;
        int deadCnt;
        FontColor fontColor;
        ConsoleKeyInfo c;

        delegate void OriginFunction(int cursor);

        public Battle(Job _player, Inventory _inventory)
        {

            mobs = new List<Mob>();
            player = _player;
            items = _inventory.invenItems;
            inventory = _inventory;
            deadCnt = 0;
            originalHP = _player.Health;
            ApperMonster();

            fontColor = new FontColor();

        }
        
        /// <summary>
        /// 전투 시작하면 보게 되는 화면
        /// </summary>
        public void BattleScene(int cursor)
        {
            SceneTitle(false);
            // 몬스터와 플레이어의 정보 나열
            DisplayStatus(false,1);
            Console.WriteLine("");

            // 1.공격 2.도망 선택
            if (cursor == 1)
                HighlightText("1. 공격하기");
            else
                Console.WriteLine("1. 공격하기");
            if (cursor == 2)
                HighlightText("2. 도망가기");
            else
                Console.WriteLine("2. 도망가기");
            /*            
                        do
                        {
                            c = Console.ReadKey();
                            switch (c.Key)
                            {
                                case ConsoleKey.UpArrow:
                                    cursor--;
                                    if (cursor < 1)
                                        cursor = 2;
                                    BattleScene(cursor);
                                    break;
                                case ConsoleKey.DownArrow:
                                    cursor++;
                                    if (cursor > 2)
                                        cursor = 1;
                                    BattleScene(cursor);
                                    break;
                            }

                        } while (c.Key != ConsoleKey.Enter);*/

            //int input = Program.CheckValidInput(1, 2);

            //위 내용을 SetCursor로 함수화 시킴
            SetCursor(1, 2, cursor, BattleScene);

            switch (cursor)
            {
                case 1:
                    SelectSkillOrAtk(1);
                    break;
                case 2:
                    Program.StartMenu("쫄보",1);
                    break;
            }
        }

        public int CirticalAttack(int damage, ref bool isCritical){
            int critical = new Random().Next(1, 100);
            if(critical <= 15){ // 크리티컬일때
                isCritical = true;
                double newCharacterSkill = damage*1.6;
                damage = (int)Math.Round(newCharacterSkill);
            } else{
                isCritical = false;
            }
           
            return damage;
        }
        // 스킬 공격 또는 아이템 사용 선택
        private void SelectSkillOrAtk(int cursor)
        {
            SceneTitle(false);
            // 몬스터와 플레이어의 정보 나열
            DisplayStatus(false,1);
            Console.WriteLine("");

            // 1.공격 2.스킬 3.아이템사용 선택
            if (cursor == 1)
                HighlightText("1. 일반 공격하기");
            else
                Console.WriteLine("1. 일반 공격하기");
            if (cursor == 2)
                HighlightText("2. 스킬 사용하기");
            else
                Console.WriteLine("2. 스킬 사용하기");
            if (cursor == 3)
                HighlightText("3. 아이템 사용하기");
            else
                Console.WriteLine("3. 아이템 사용하기");
            Console.WriteLine("");
            if (cursor == 0)
                HighlightText("0. 취소");
            else
                Console.WriteLine("0. 취소");
            //int input = Program.CheckValidInput(1, 3);
            SetCursor(0, 3, cursor, SelectSkillOrAtk);

            switch (cursor)
            {
                case 0:
                    BattleScene(1);
                    break;
                case 1:
                    SelectMonster(false,1);
                    break;
                case 2:
                    SelectSkillAtkMonster(false,1);
                    break;
                case 3:
                    UsingItem(1);
                    break;
            }

        }

        // 스킬로 어떤 몬스터를 가격할 지 선택
        private void SelectSkillAtkMonster(bool reSelect,int cursor)
        {
            SceneTitle(false);
            DisplayStatus(true,cursor);
            Console.WriteLine("");
            if (reSelect)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("그는 이미 갔습니다.");
                Console.ResetColor();
            }
            if (cursor == 0)
                HighlightText("0. 취소");
            else
                Console.WriteLine("0. 취소");

            Console.WriteLine("");
            Console.WriteLine("대상을 선택해주세요.");
            do
            {
                c = Console.ReadKey();
                switch (c.Key)
                {
                    case ConsoleKey.UpArrow:
                        cursor--;
                        if (cursor < 0)
                            cursor = mobs.Count;
                        SelectSkillAtkMonster(false, cursor);
                        break;
                    case ConsoleKey.DownArrow:
                        cursor++;
                        if (cursor > mobs.Count)
                            cursor = 0;
                        SelectSkillAtkMonster(false, cursor);
                        break;
                }

            } while (c.Key != ConsoleKey.Enter);
            //int input = Program.CheckValidInput(0, mobs.Count);
            //SetCursor(0, mobs.Count, cursor, SelectSkillAtkMonster);

            switch (cursor)
            {
                //취소
                case 0:
                    SelectSkillOrAtk(1);
                    break;
                //몬스터 고름
                case 1:
                case 2:
                case 3:
                case 4:
                    if (mobs[cursor - 1].IsDead)
                    SelectMonster(true,1);
                    PlayerSkillResult(cursor - 1);
                    break;
            }
        }

        // 마나 여부 체크
        private void TryUseSkillWithManaCheck(int skillMana,int cursor)
        {
            // 플레이어의 현재 마나가 스킬보다 작을 때
            if (player.Mana < skillMana)
            {
                // 플레이어 마나 부족 시 마나가 부족합니다.
                player.LackMana();
                Console.WriteLine("마나 포션을 사용하시겠습니까?");
                Console.WriteLine("");

                if (cursor == 1)
                    HighlightText("1. 사용하기");
                else
                    Console.WriteLine("1. 사용하기");
                if (cursor == 2)
                    HighlightText("2. 던전에서 나가기");
                else
                    Console.WriteLine("2. 던전에서 나가기");
                Console.WriteLine("");

                do
                {
                    c = Console.ReadKey();
                    switch (c.Key)
                    {
                        case ConsoleKey.UpArrow:
                            cursor--;
                            if (cursor < 1)
                                cursor = 2;
                            TryUseSkillWithManaCheck(skillMana, cursor);
                            break;
                        case ConsoleKey.DownArrow:
                            cursor++;
                            if (cursor > 2)
                                cursor = 1;
                            TryUseSkillWithManaCheck(skillMana, cursor);
                            break;
                    }
                } while (c.Key != ConsoleKey.Enter);

                switch (cursor)
                {
                    case 1:
                        IsMpPotionUsed(1);
                        break;
                    case 2:
                        Program.StartMenu(player.Occupation,1);
                        break;
                }

            }
        }

        // 플레이어의 스킬 결과
        private void PlayerSkillResult(int idx)
        {

            Console.Clear();

            // 스킬 사용시 드는 마나
            int skill_1Mana = 50;

            // 사용 마나 여부 체크
            TryUseSkillWithManaCheck(skill_1Mana,1);
            
            // 캐릭터의 스킬 
            bool isCritical = false;

            int characterSkill = player.Skill_1(player.Occupation, player.Atk, skill_1Mana);
            characterSkill = CirticalAttack(characterSkill,ref isCritical);


           
            //몬스터에게 데미지 가하기
            Console.WriteLine($"Lv.{mobs[idx].Level} {mobs[idx].Name} 을(를) 맞췄습니다. [데미지 : {characterSkill}]  {(isCritical? "- 치명타 공격!!" : "")}");
            Console.WriteLine("");
            mobs[idx].IsDead = mobs[idx].Health - characterSkill <= 0 ? true : false;
            Console.WriteLine($"Lv.{mobs[idx].Level} {mobs[idx].Name}");
            Console.WriteLine($"HP {mobs[idx].Health} -> {(mobs[idx].IsDead ? "Dead" : mobs[idx].Health - characterSkill)}");
            Console.WriteLine();
            if (mobs[idx].IsDead)
            {
                deadCnt++;
                player.Exp += mobs[idx].Exp;
                LevelController();
                Console.WriteLine($"현재 경험치: {player.Exp}");
                Console.WriteLine();
            }
            mobs[idx].Health -= characterSkill;
            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine("");
            int input = Program.CheckValidInput(0, 0);
            if (input == 0)
            {
                // 몬스터가 전부 죽었다면 Victory
                if (mobs.Count == deadCnt)
                    BattleResult(true);
                else
                    MonsterAttackResult();
            }


        }

        /// <summary>
        /// 몬스터 1~4마리 랜덤 출현 (몬스터 중복 가능, 순서 상관 없음)
        /// </summary>
        private void ApperMonster()
        {

            Random rand = new Random();
            int numberOfMob = rand.Next(1, 5);
            for (int i = 1; i <= numberOfMob; i++)
            {
                mobs.Add(new Mob("달팽이" + i, "달팽이", 2, 5, 10, 5, 5, false));

            }
        }
        private void DisplayStatus(bool isSelect,int cursor)
        {
            for (int i = 0; i < mobs.Count; i++)
            {
                if (mobs[i].IsDead)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                if (isSelect)
                    Console.Write($"{i + 1} ");
                if (isSelect && cursor == i + 1)
                    HighlightText($"Lv.{mobs[i].Level} {mobs[i].Name} {(mobs[i].IsDead ? "Dead" : $"HP{mobs[i].Health}")}");
                else
                    Console.WriteLine($"Lv.{mobs[i].Level} {mobs[i].Name} {(mobs[i].IsDead ? "Dead" : $"HP{mobs[i].Health}")}");

                Console.ResetColor();
            }
            Console.WriteLine("");
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{player.Level} {player.Name} ({player.Occupation})");
            //TODO: 100 -> 직업별 MaxHP로
            Console.WriteLine($"{player.Health}/ {player.MaxHealth}");
            Console.WriteLine($"{player.Mana} / {player.MaxMana}");
        }

        /// <summary>
        /// 공격 선택 시 공격할 몬스터 선택
        /// </summary>
        private void SelectMonster(bool reSelect,int cursor)
        {
            SceneTitle(false);
            DisplayStatus(true,cursor);
            Console.WriteLine("");
            if (reSelect)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("그는 이미 갔습니다.");
                Console.ResetColor();
            }

            if (cursor == 0)
                HighlightText("0. 취소");
            else
                Console.WriteLine("0. 취소");

            Console.WriteLine("");
            Console.WriteLine("대상을 선택해주세요.");
            do
            {
                c = Console.ReadKey();
                switch (c.Key)
                {
                    case ConsoleKey.UpArrow:
                        cursor--;
                        if (cursor < 0)
                            cursor = mobs.Count;
                        SelectMonster(false, cursor);
                        break;
                    case ConsoleKey.DownArrow:
                        cursor++;
                        if (cursor > mobs.Count)
                            cursor = 0;
                        SelectMonster(false, cursor);
                        break;
                }
            } while (c.Key != ConsoleKey.Enter);
            //int input = Program.CheckValidInput(0, mobs.Count);
            switch (cursor)
            {
                //취소
                case 0:
                    SelectSkillOrAtk(1);
                    break;
                //몬스터 고름
                case 1:
                case 2:
                case 3:
                case 4:
                    if (mobs[cursor - 1].IsDead)
                        SelectMonster(true,1);
                    PlayerAttackResult(cursor - 1);
                    break;
            }

        }

        /// <summary>
        /// 플레이어의 공격 결과
        /// </summary>
        private void PlayerAttackResult(int idx)
        {
            SceneTitle(false);

            int dodge = new Random().Next(1,100);
            //데미지 계산
            bool isCritical = false;
            int Damage = player.Attack(mobs[idx]);
            Damage = CirticalAttack(Damage, ref isCritical);
            //몬스터에게 데미지 가하기
            //Console.WriteLine($"{player.Name} 의 공격!");
            //Console.WriteLine($"Lv.{mobs[idx].Level} {mobs[idx].Name} 을(를) 맞췄습니다. [데미지 : {Damage}]");
            //Console.WriteLine("");
            //Console.WriteLine($"Lv.{mobs[idx].Level} {mobs[idx].Name}");
            //mobs[idx].IsDead = mobs[idx].Health - Damage <= 0 ? true : false;
            //if (mobs[idx].IsDead)
            //{
            //    deadCnt++;
            //}
            //Console.WriteLine($"HP {mobs[idx].Health} -> {(mobs[idx].IsDead ? "Dead" : mobs[idx].Health - Damage)}");
            //Console.WriteLine("");
            //mobs[idx].Health -= Damage;
            if(dodge > 11){
                Console.WriteLine($"Lv.{mobs[idx].Level} {mobs[idx].Name} 을(를) 맞췄습니다. [데미지 : {Damage}] {(isCritical? "- 치명타 공격!!" : "")}");
                Console.WriteLine("");
                Console.WriteLine($"Lv.{mobs[idx].Level} {mobs[idx].Name}");

                mobs[idx].IsDead = mobs[idx].Health - Damage <= 0 ? true : false;
                if (mobs[idx].IsDead)
                {
                    deadCnt++;
                    Console.WriteLine(mobs[idx].Exp);
                    player.Exp += mobs[idx].Exp;
                    Console.WriteLine($"현재 경험치: {player.Exp}");
                }
                Console.WriteLine($"HP {mobs[idx].Health} -> {(mobs[idx].IsDead ? "Dead" : mobs[idx].Health - Damage)}");
                Console.WriteLine("");
                mobs[idx].Health -= Damage;
            }else{
                Console.WriteLine($"Lv.{mobs[idx].Level} {mobs[idx].Name} 을(를) 공격했지만 아무일도 일어나지 않았습니다.");
            }
            Console.WriteLine("0. 다음");
            Console.WriteLine("");
            int input = Program.CheckValidInput(0, 0);
            if (input == 0)
            {
                // 몬스터가 전부 죽었다면 Victory
                if (mobs.Count == deadCnt)
                    BattleResult(true);
                else
                    MonsterAttackResult();
            }

        }

        /// <summary>
        /// 몬스터의 공격 결과
        /// </summary>
        private void MonsterAttackResult()
        {
            SceneTitle(false);
            for (int i = 0; i < mobs.Count; i++)
            {
                if (mobs[i].IsDead)
                    continue;
                if (player.IsDead)
                {
                    player.Health = 0;
                    break;
                }
                Console.WriteLine($"Lv.{mobs[i].Level} {mobs[i].Name} 의 공격!");
                Console.WriteLine($"{player.Name} 을(를) 맞췄습니다. [데미지 : {(int)mobs[i].Atk}]");
                Console.WriteLine("");
                player.IsDead = player.Health - (int)mobs[i].Atk <= 0 ? true : false;
                Console.WriteLine($"Lv.{player.Level} {player.Name}");
                Console.WriteLine($"HP {player.Health} -> {(player.IsDead ? "Dead" : player.Health - (int)mobs[i].Atk)}");
                Console.WriteLine("");
                player.Health -= (int)mobs[i].Atk;
            }

            Console.WriteLine("0. 다음");
            Console.WriteLine("");
            int input = Program.CheckValidInput(0, 0);
            if (input == 0)
            {
                // 플레이어가 죽었다면 Lose
                if (player.IsDead)
                    BattleResult(false);
                else
                    BattleScene(1);
            }
        }

        private void BattleResult(bool isVictory)
        {
            SceneTitle(true);
            if (isVictory)
            {
                //승리
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Victory");
                Console.ResetColor();
                Console.WriteLine("");
                Console.WriteLine($"던전에서 몬스터 {deadCnt}마리를 잡았습니다.");
                Console.WriteLine("");
                Console.WriteLine($"Lv.{player.Level} {player.Name}");
                Console.WriteLine($"HP {originalHP} -> {player.Health}");
                Console.WriteLine("");
            }
            else
            {
                //패배
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You Lose");
                Console.ResetColor();
                Console.WriteLine("");
                Console.WriteLine($"Lv.{player.Level} {player.Name}");
                Console.WriteLine($"HP {originalHP} -> {player.Health}");
                Console.WriteLine("");
            }

            Console.WriteLine("0. 다음");
            Console.WriteLine("");

            int input = Program.CheckValidInput(0, 0);
            if (input == 0)
            {
                Program.StartMenu(player.Occupation,1);
            }
        }

        // 아이템 사용하기
        private void UsingItem(int cursor)
        {
            Console.Clear();
            Console.WriteLine("아이템 사용하기 입니다.");
            Console.WriteLine("사용할 아이템의 종류를 선택해주세요.");
            if (cursor == 1)
                HighlightText("1. 힐링 포션");
            else
                Console.WriteLine("1. 힐링 포션");

            if (cursor == 2)
                HighlightText("2. 마나 포션");
            else
                Console.WriteLine("2. 마나 포션");
            Console.WriteLine("");
            if (cursor == 0)
                HighlightText("0. 취소");
            else
                Console.WriteLine("0. 취소");

            SetCursor(0, 2, cursor, UsingItem);

            switch (cursor)
            {
                case 0:
                    SelectSkillOrAtk(1);
                    break;
                case 1:
                    if (player.Health != player.MaxHealth)
                    {
                        IsHpPotionUsed(1);
                    }
                    else
                    {
                        FullHpPrint();
                    }
                    break;
                case 2:
                    if (player.Mana != player.MaxMana)
                    {
                        IsMpPotionUsed(1);
                    }
                    else
                    {
                        FullMpPrint();
                    }
                    break;
            }
        }

        // 플레이어의 체력이 가득 찼을 때 출력 및 이동
        private void FullHpPrint()
        {
            Console.Clear();
            Console.WriteLine("체력이 이미 만땅입니다.");
            Console.WriteLine("아무키나 누르면 전투로 돌아갑니다.");
            Console.ReadLine();
            SelectSkillOrAtk(1);
        }

        // 플레이어의 마나가 가득 찼을 때 출력 및 이동
        private void FullMpPrint()
        {
            Console.Clear();
            Console.WriteLine("마나가 이미 만땅입니다.");
            ReturnToSelectAtk();
        }

        // hp포션 사용 여부
        public void IsHpPotionUsed(int cursor)
        {
            int count = 0;

            // invetory 내의 아이템에서 HealingPotion으로 생성된 아이템을 카운트
            foreach (Items hpPotion in items) 
            {
                if (hpPotion is HealingPotion)
                {
                    count++;
                }
            }

            Console.Clear();
            Console.WriteLine($"힐링포션 개수: {count}");
            Console.WriteLine("");
            // 포션이 없을 경우 포션이 부족합니다.
            if (count <= 0)
            {
                Console.WriteLine("포션이 부족합니다.");
                ReturnToSelectAtk();

            }
            // 포션을 먹었을 때 체력이 가득 차는 경우 ex) 포션 힐량 20 이고 플레이어 체력 190 / 200 일 경우 초과하기 때문에
            // 맥스 체력 이상으로 올라가지 않게하기 위해 넘어갈 경우 Max체력을 넣어줌
            else if(player.Health > player.MaxHealth) 
            {
                player.Health = player.MaxHealth;
                Console.WriteLine("체력이 가득 찼습니다.");
                ReturnToSelectAtk();
                
            }
            // 체력이 가득차있는데 포션을 먹으려는 경우
            else if(player.Health == player.MaxHealth)
            {
                Console.WriteLine("체력이 가득찼습니다.");
                ReturnToSelectAtk();
            }
            else
            {
                // 포션 사용
                Console.WriteLine("힐링 포션을 사용하시겠습니까?");
                Console.WriteLine($"현재 체력: {player.Health}");

                if (cursor == 1)
                    HighlightText("1. 네 (아이템을 사용합니다.)");
                else
                    Console.WriteLine("1. 네 (아이템을 사용합니다.)");
                if (cursor == 2)
                    HighlightText("2. 아니오 (전투로 돌아갑니다.)");
                else
                    Console.WriteLine("2. 아니오 (전투로 돌아갑니다.)");
                Console.WriteLine("");

                SetCursor(1, 2, cursor, IsHpPotionUsed);

                switch (cursor)
                {
                    case 1:
                        // 포션 사용
                        int beforeHp = player.Health;
                        inventory.UseHpPotion();
                        fontColor.WriteColorFont($"물약을 사용하였습니다. HP : {beforeHp} -> {player.Health}", FontColor.Color.Blue);
                        Thread.Sleep(1500);
                        IsHpPotionUsed(1);
                        break;
                    case 2:
                        SelectSkillOrAtk(1);
                        break;
                }

            }
        }

        // SelectSkillOrAtk(); 로 돌아가는 로직
        private void ReturnToSelectAtk()
        {
            Console.WriteLine("전투화면으로 돌아갑니다.");
            Console.WriteLine("아무 키나 입력해주세요.");
            Console.ReadLine();
            SelectSkillOrAtk(1);
        }

        // 마나 포션 사용 여부
        private void IsMpPotionUsed(int cursor)
        {
            int count = 0;

            foreach (Items mpPotion in items)
            {
                if (mpPotion is ManaPotion)
                {
                    count++;
                }
            }

            Console.Clear();
            Console.WriteLine($"마나포션 개수: {count}");
            if (count <= 0)
            {
                Console.WriteLine("포션이 부족합니다.");
                Console.WriteLine("전투화면으로 돌아갑니다.");
                Console.WriteLine("아무 키나 입력해주세요.");
                Console.ReadLine();
                SelectSkillOrAtk(1);

            }
            else if (player.Mana > player.MaxMana)
            {
                player.Mana = player.MaxMana;
                Console.WriteLine("마나가 가득 찼습니다.");
                ReturnToSelectAtk();

            }
            else if (player.Mana == player.MaxMana)
            {
                Console.WriteLine("마나가 가득찼습니다.");
                ReturnToSelectAtk();
            }
            else
            {
                Console.WriteLine("마나 포션을 사용하시겠습니까?");
                Console.WriteLine($"현재 마나: {player.Mana}");

                if (cursor == 1)
                    HighlightText("1. 네 (아이템을 사용합니다.)");
                else
                    Console.WriteLine("1. 네 (아이템을 사용합니다.)");
                if (cursor == 2)
                    HighlightText("2. 아니오 (전투로 돌아갑니다.)");
                else
                    Console.WriteLine("2. 아니오 (전투로 돌아갑니다.)");
                Console.WriteLine("");
                SetCursor(1, 2, cursor, IsMpPotionUsed);
                switch (cursor)
                {
                    case 1:
                        int beforeMp = player.Mana;
                        inventory.UseMpPotion();
                        fontColor.WriteColorFont($"물약을 사용하였습니다. Mana : {beforeMp} -> {player.Mana}", FontColor.Color.Blue);
                        Thread.Sleep(1500);
                        IsMpPotionUsed(1);
                        break;
                    case 2:
                        SelectSkillOrAtk(1);
                        break;
                }

            }
        }


        private void SceneTitle(bool isResult)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (isResult)
                Console.WriteLine("Battle!! - Result");
            else
                Console.WriteLine("Battle!!");
            Console.ResetColor();
            Console.WriteLine("");
        }
        void HighlightText(string str)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(str);
            Console.ResetColor();
        }

        /// <summary>
        /// min에서부터 max까지인 함수에서 커서 컨트롤
        /// </summary>
        void SetCursor(int min,int max,int cursor, OriginFunction funcName)
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
                        funcName(cursor);
                        break;
                    case ConsoleKey.DownArrow:
                        cursor++;
                        if (cursor > max)
                            cursor = min;
                        funcName(cursor);
                        break;
                }

            } while (c.Key != ConsoleKey.Enter);
        }

        // 플레이어 레벨업 판단
        private void LevelController()
        {
            // 레벨업  
            if (player.Exp >= player.MaxExp)
            {
                player.Exp = player.Exp - player.MaxExp;
                player.Level++;
                player.MaxExp *= 1.5f;

                Console.WriteLine("레벨업을 했습니다!");
                Console.WriteLine($"Lv {player.Level - 1} -> Lv {player.Level}");

                // 레벨에 따라 캐릭터 능력치 변경 (?)

            }
        }

    }

}
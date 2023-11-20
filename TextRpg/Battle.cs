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
        public void BattleScene()
        {
            SceneTitle(false);
            // 몬스터와 플레이어의 정보 나열
            DisplayStatus(false);
            Console.WriteLine("");

            // 1.공격 2.도망 선택
            Console.WriteLine("1. 공격하기");
            Console.WriteLine("2. 도망가기");
            int input = Program.CheckValidInput(1, 2);
            switch (input)
            {
                case 1:
                    SelectSkillOrAtk();
                    break;
                case 2:
                    Program.StartMenu("쫄보");
                    break;
            }
        }

        // 스킬 공격 또는 아이템 사용 선택
        private void SelectSkillOrAtk()
        {
            SceneTitle(false);
            // 몬스터와 플레이어의 정보 나열
            DisplayStatus(false);
            Console.WriteLine("");

            // 1.공격 2.스킬 3.아이템사용 선택
            Console.WriteLine("1. 일반 공격하기");
            Console.WriteLine("2. 스킬 사용하기");
            Console.WriteLine("3. 아이템 사용하기");
            Console.WriteLine("");

            int input = Program.CheckValidInput(1, 3);

            switch(input)
            {
                case 1:
                    SelectMonster(false);
                    break;
                case 2:
                    SelectSkillAtkMonster(false);
                    break;
                case 3:
                    UsingItem();
                    break;
            }

        }

        // 스킬로 어떤 몬스터를 가격할 지 선택
        private void SelectSkillAtkMonster(bool reSelect)
        {
            SceneTitle(false);
            DisplayStatus(true);
            Console.WriteLine("");
            if (reSelect)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("그는 이미 갔습니다.");
                Console.ResetColor();
            }
            Console.WriteLine("0. 취소");
            Console.WriteLine("");
            Console.WriteLine("대상을 선택해주세요.");
            int input = Program.CheckValidInput(0, mobs.Count);
            switch (input)
            {
                //취소
                case 0:
                    BattleScene();
                    break;
                //몬스터 고름
                case 1:
                case 2:
                case 3:
                case 4:
                    if (mobs[input - 1].IsDead)
                    SelectMonster(true);
                    PlayerSkillResult(input - 1);
                    break;
            }
        }

        // 마나 여부 체크
        private void TryUseSkillWithManaCheck(int skillMana)
        {
            // 플레이어의 현재 마나가 스킬보다 작을 때
            if (player.Mana < skillMana)
            {
                // 플레이어 마나 부족 시 마나가 부족합니다.
                player.LackMana();
                Console.WriteLine("마나 포션을 사용하시겠습니까?");
                Console.WriteLine("");
                Console.WriteLine("1. 사용하기");
                Console.WriteLine("2. 던전에서 나가기");
                Console.WriteLine("");

                switch (Program.CheckValidInput(1, 2))
                {
                    case 1:
                        IsMpPotionUsed();
                        break;
                    case 2:
                        Program.StartMenu(player.Occupation);
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
            TryUseSkillWithManaCheck(skill_1Mana);
            
            // 캐릭터의 스킬 
            int characterSkill = player.Skill_1(player.Occupation, player.Atk, skill_1Mana);
           
            //몬스터에게 데미지 가하기
            Console.WriteLine($"Lv.{mobs[idx].Level} {mobs[idx].Name} 을(를) 맞췄습니다. [데미지 : {characterSkill}]");
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
        private void DisplayStatus(bool isSelect)
        {
            for (int i = 0; i < mobs.Count; i++)
            {
                if (mobs[i].IsDead)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                if (isSelect)
                    Console.Write($"{i+1} ");
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
        private void SelectMonster(bool reSelect)
        {
            SceneTitle(false);
            DisplayStatus(true);
            Console.WriteLine("");
            if (reSelect)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("그는 이미 갔습니다.");
                Console.ResetColor();
            }
            Console.WriteLine("0. 취소");
            Console.WriteLine("");
            Console.WriteLine("대상을 선택해주세요.");
            int input = Program.CheckValidInput(0, mobs.Count);
            switch (input)
            {
                //취소
                case 0:
                    BattleScene();
                    break;
                //몬스터 고름
                case 1:
                case 2:
                case 3:
                case 4:
                    if (mobs[input - 1].IsDead)
                        SelectMonster(true);
                    PlayerAttackResult(input - 1);
                    break;
            }

        }

        /// <summary>
        /// 플레이어의 공격 결과
        /// </summary>
        private void PlayerAttackResult(int idx)
        {
            SceneTitle(false);

            //데미지 계산
            int Damage = player.Attack(mobs[idx]);

            //몬스터에게 데미지 가하기
            Console.WriteLine($"{player.Name} 의 공격!");
            Console.WriteLine($"Lv.{mobs[idx].Level} {mobs[idx].Name} 을(를) 맞췄습니다. [데미지 : {Damage}]");
            Console.WriteLine("");
            mobs[idx].IsDead = mobs[idx].Health - Damage <= 0 ? true : false;
            Console.WriteLine($"Lv.{mobs[idx].Level} {mobs[idx].Name}");
            Console.WriteLine($"HP {mobs[idx].Health} -> {(mobs[idx].IsDead ? "Dead" : mobs[idx].Health - Damage)}");
            Console.WriteLine();
            if (mobs[idx].IsDead)
            {
                deadCnt++;
                player.Exp += mobs[idx].Exp;
                LevelController();
                Console.WriteLine($"현재 경험치: {player.Exp}");
                Console.WriteLine();
            }
            mobs[idx].Health -= Damage;
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
                    BattleScene();
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
                Program.StartMenu("");
            }
        }

        // 아이템 사용하기
        private void UsingItem()
        {
            Console.Clear();
            Console.WriteLine("아이템 사용하기 입니다.");
            Console.WriteLine("사용할 아이템의 종류를 선택해주세요.");
            Console.WriteLine("1. 힐링 포션");
            Console.WriteLine("2. 마나 포션");
            Console.WriteLine("");
            Console.WriteLine("0. 취소하기");

            switch(Program.CheckValidInput(0, 2))
            {
                case 0:
                    SelectSkillOrAtk();
                    break;
                case 1:
                    if (player.Health != player.MaxHealth)
                    {
                        IsHpPotionUsed();
                    }
                    else
                    {
                        FullHpPrint();
                    }
                    break;
                case 2:
                    if (player.Mana != player.MaxMana)
                    {
                        IsMpPotionUsed();
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
            SelectSkillOrAtk();
        }

        // 플레이어의 마나가 가득 찼을 때 출력 및 이동
        private void FullMpPrint()
        {
            Console.Clear();
            Console.WriteLine("마나가 이미 만땅입니다.");
            ReturnToSelectAtk();
        }

        // hp포션 사용 여부
        public void IsHpPotionUsed()
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
                Console.WriteLine("1. 네 (아이템을 사용합니다.)");
                Console.WriteLine("2. 아니오 (전투로 돌아갑니다.)");
                Console.WriteLine("");

                switch(Program.CheckValidInput(1, 2))
                {
                    case 1:
                        // 포션 사용
                        inventory.UseHpPotion();
                        IsHpPotionUsed();
                        break;
                    case 2:
                        SelectSkillOrAtk();
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
            SelectSkillOrAtk();
        }

        // 마나 포션 사용 여부
        private void IsMpPotionUsed()
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
                SelectSkillOrAtk();

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
                Console.WriteLine("1. 네 (아이템을 사용합니다.)");
                Console.WriteLine("2. 아니오 (전투로 돌아갑니다.)");
                Console.WriteLine("");

                switch (Program.CheckValidInput(1, 2))
                {
                    case 1:
                        inventory.UseMpPotion();
                        IsMpPotionUsed();
                        break;
                    case 2:
                        SelectSkillOrAtk();
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
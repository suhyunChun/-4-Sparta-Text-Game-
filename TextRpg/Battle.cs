using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRpg.Player;

namespace TextRpg
{
    class Battle
    {
        //몬스터 List
        List<Mob> mobs;
        Job player;
        //던전 입장시 HP
        int originalHP;
        //죽은 몬스터 수
        int deadCnt;

        FontColor fontColor;
        ConsoleKeyInfo c;

        public Battle(Job _player)
        {
            mobs = new List<Mob>();
            player = _player;
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
            // 1.공격 2.도망 3.아이템사용 선택
            if (cursor == 1)
                HighlightText("1. 공격하기");
            else
                Console.WriteLine("1. 공격하기");
            if (cursor == 2)
                HighlightText("2. 도망가기");
            else
                Console.WriteLine("2. 도망가기");
            if( cursor ==3)
                HighlightText("3. 아이템 사용하기");
            else
                Console.WriteLine("3. 아이템 사용하기");

            do
            {
                c = Console.ReadKey();
                switch (c.Key)
                {
                    case ConsoleKey.UpArrow:
                        cursor--;
                        if (cursor < 1)
                            cursor = 3;
                        BattleScene(cursor);
                        break;
                    case ConsoleKey.DownArrow:
                        cursor++;
                        if (cursor > 3)
                            cursor = 1;
                        BattleScene(cursor);
                        break;
                }

            } while (c.Key != ConsoleKey.Enter);

            //int input = Program.CheckValidInput(1, 3);
            switch (cursor)
            {
                case 1:
                    SelectMonster(false,1);
                    break;
                case 2:
                    Program.StartMenu("쫄보");
                    break;
                case 3:
                    UsingItem();
                    break;
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
                mobs.Add(new Mob("달팽이" + i, "달팽이", 2, 10, 5, 5, false));
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
                    Console.Write($"{i+1} ");
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
            Console.WriteLine($"{player.Health}/100");
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
                HighlightText("0, 취소");
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
                        SelectMonster(false,cursor);
                        break;
                    case ConsoleKey.DownArrow:
                        cursor++;
                        if (cursor > mobs.Count)
                            cursor = 0;
                        SelectMonster(false,cursor);
                        break;
                }

            } while (c.Key != ConsoleKey.Enter);
            //int input = Program.CheckValidInput(0, mobs.Count);
            switch (cursor)
            {
                //취소
                case 0:
                    BattleScene(1);
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

            //데미지 계산
            int Damage = player.Attack(mobs[idx]);

            //몬스터에게 데미지 가하기
            Console.WriteLine($"{player.Name} 의 공격!");
            Console.WriteLine($"Lv.{mobs[idx].Level} {mobs[idx].Name} 을(를) 맞췄습니다. [데미지 : {Damage}]");
            Console.WriteLine("");
            Console.WriteLine($"Lv.{mobs[idx].Level} {mobs[idx].Name}");
            mobs[idx].IsDead = mobs[idx].Health - Damage <= 0 ? true : false;
            if (mobs[idx].IsDead)
                deadCnt++;
            Console.WriteLine($"HP {mobs[idx].Health} -> {(mobs[idx].IsDead ? "Dead" : mobs[idx].Health - Damage)}");
            Console.WriteLine("");
            mobs[idx].Health -= Damage;

            Console.WriteLine("Press Any Key...");
            Console.ReadKey();

            // 몬스터가 전부 죽었다면 Victory
            if (mobs.Count == deadCnt)
                BattleResult(true);
            else
                MonsterAttackResult();

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

            Console.WriteLine("Press Any Key...");
            Console.ReadKey();
            // 플레이어가 죽었다면 Lose
            if (player.IsDead)
                BattleResult(false);
            else
                BattleScene(1);
        }

        private void BattleResult(bool isVictory)
        {
            SceneTitle(true);
            if (isVictory)
            {
                //승리
                fontColor.WriteColorFont("oooooo     oooo  o8o                .                                   \n" +
                                         " `888.     .8'   `\"'              .o8                                  \n" +
                                         "  `888.   .8'   oooo   .ooooo.  .o888oo  .ooooo.  oooo d8b oooo    ooo  \n" +
                                         "   `888. .8'    `888  d88' `\"Y8   888   d88' `88b `888\"\"8P  `88.  .8'\n" +
                                         "    `888.8'      888  888         888   888   888  888       `88..8'    \n" +
                                         "     `888'       888  888   .o8   888 . 888   888  888        `888'     \n" +
                                         "      `8'       o888o `Y8bod8P'   \"888\" `Y8bod8P' d888b        .8'    \n" +
                                         "                                                           .o..P'       \n" +
                                         "                                                           `Y8P'        \n",FontColor.Color.Green);
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
                fontColor.WriteColorFont("oooooo   oooo                            ooooo                                        \n" +
                                         " `888.   .8'                             `888'                                        \n" +
                                         "  `888. .8'    .ooooo.  oooo  oooo        888          .ooooo.   .oooo.o  .ooooo.     \n" +
                                         "   `888.8'    d88' `88b `888  `888        888         d88' `88b d88(  \"8 d88' `88b   \n" +
                                         "    `888'     888   888  888   888        888         888   888 `\"Y88b.  888ooo888   \n" +
                                         "     888      888   888  888   888        888       o 888   888 o.  )88b 888    .o    \n" +
                                         "    o888o     `Y8bod8P'  `V88V\"V8P'      o888ooooood8 `Y8bod8P' 8\"\"888P' `Y8bod8P' \n", FontColor.Color.Red);
                Console.WriteLine("");
                Console.WriteLine($"Lv.{player.Level} {player.Name}");
                Console.WriteLine($"HP {originalHP} -> {player.Health}");
                Console.WriteLine("");
            }

            Console.WriteLine("Press Any Key...");
            Console.ReadKey();

            Program.StartMenu(player.Occupation);
        }
        private void UsingItem()
        {

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
    }

}
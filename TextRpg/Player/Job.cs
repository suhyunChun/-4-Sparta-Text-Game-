﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TextRpg.Player;

namespace TextRpg.Player
{
    public class Job : ICharacter
    {
        // 캐릭터 번호
        public string Id { get; }
        // 캐릭터 이름
        public string Name { get; }
        // 직업
        public string Occupation { get; }
        // 레벨 = 전체적인 능력치 및 방어력 증가
        public int Level { get; set; }
        // 경험치
        public float Exp { get; set; }
        // 힘 = 체력 관련 능력치
        public int Strength { get; }
        // 민첩성 = 공격력 관련 능력치
        public int Agility { get; }
        // 지능 = 마력과 기술 피해 관련 능력치
        public int Intelligence { get; }
        // 체력 = 힘으로 증가
        public int Health => 100 + Strength * 20;
        // 마력 = 지능으로 증가
        public int Mana => 10 + Intelligence * 2;
        // 공격력 = 민첩성으로 증가
        public float Atk => 10 + Agility * 2;
        // 추가 공격력 = 아이템에 의한 수치 변화
        public float PlusAtk { get; set; }
        // 방어력 = 레벨로 증가
        public float Def => 5 + Level;
        // 추가 방어력 = 아이템에 의한 수치 변화
        public float PlusDef { get; set; }
        // 골드 = 상점에 사용할 돈 및 몬스터가 뱉을 돈
        public int Gold { get; set;}
        // 무구 슬롯 = 플레이어가 장착하고 있을 장비 칸. 몬스터는 공백.
        public int Weapon { get; set; }
        // 방어구 슬롯 = 플레이어가 장착하고 있을 장비 칸. 몬스터는 공백.
        public int Armor { get; set; }
        // 소지 아이템 = 플레이어가 소지중인 아이템들. 몬스터는 드롭 아이템 목록으로 사용.
        public int[] Item { get; set; }
        // 죽음 여부 = 해치웠나?
        public bool IsDead { get; set; }

        public Job(string id, string name, string occupation, int level, int exp, int Str, int Agi, int Int, float itematk, float itemdef, int gold, int weapon, int armor, int[] items, bool isDead)
        {
            Id = id;
            Name = name;
            Occupation = occupation;
            Level = level;
            Exp = exp;
            Strength = Str;
            Agility = Agi;
            Intelligence = Int;
            PlusAtk = itematk;
            PlusDef = itemdef;
            Gold = gold;
            Weapon = weapon;
            Armor = armor;
            Item = items;
            IsDead = isDead;
        }
        public int Attack(ICharacter target)
        {
            Random rand = new Random();
            var err = Atk * 0.1;
            int dps = rand.Next((int)(Atk - err), (int)(Atk + err + 1));
            dps -= (int)target.Def;
            if (dps <= 1) dps = 1;
            return dps;
        }
        // 기술 1번 = 사용 기술. 각 직업마다의 1번 기술 및 몬스터의 1번 기술. 몬스터는 없을 시 사용 못함.
        public void Skill_1(ICharacter player, ICharacter target)
        {
            if (player.Occupation == "전사")
            {

            }
            else if (player.Occupation == "마법사")
            {

            }
            else if (player.Occupation == "궁수")
            {

            }
        }
        // 기술 2번 = 사용 기술. 각 직업마다의 2번 기술 및 몬스터의 2번 기술. 플레이어의 레벨이 5를 달성하면 해금됨. 몬스터는 없을 시 사용 못함.
        public void Skill_2(ICharacter player, ICharacter target)
        {
            if(player.Level >= 5)
            {
                if(player.Occupation == "전사")
                {

                }
                else if(player.Occupation == "마법사")
                {

                }
                else if(player.Occupation == "궁수")
                {

                }
            }
        }
        // 기술 3번 = 사용 기술. 각 직업마다의 3번 기술 및 몬스터의 3번 기술. 플레이어의 레벨이 10을 달성하면 해금됨. 몬스터는 없을 시 사용 못함.
        public void Skill_3(ICharacter player, ICharacter target)
        {
            if (player.Level >= 10)
            {
                if (player.Occupation == "전사")
                {

                }
                else if (player.Occupation == "마법사")
                {

                }
                else if (player.Occupation == "궁수")
                {

                }
            }
        }
    }
}
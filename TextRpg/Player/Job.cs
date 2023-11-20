﻿using System;
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
        public int Id { get; }
        // 캐릭터 이름
        public string Name { get; }
        // 직업
        public string Occupation { get; }
        // 레벨 = 전체적인 능력치 및 방어력 증가
        public int Level { get; protected set; }

        public int MaxExp { get; protected set; }

        // 경험치
        public float Exp { get; set; }
        // 힘 = 체력 관련 능력치
        public int Strength { get; }
        // 민첩성 = 공격력 관련 능력치
        public int Agility { get; }
        // 지능 = 마력과 기술 피해 관련 능력치
        public int Intelligence { get; }
        // 체력 / 전체체력 을 위한 전체 체력
        public int MaxHealth { get; protected set; }

        // 체력 = 힘으로 증가
        public int Health { get; set; }
        // 마나/ 전체마나 를 위한 전체 마나
        public int MaxMana { get; protected set; }

        // 마력 = 지능으로 증가
        public int Mana { get; set; }
        // 공격력 = 민첩성으로 증가
        public float Atk { get; }
        // 추가 공격력 = 아이템에 의한 수치 변화
        public float PlusAtk { get; set; }
        // 방어력 = 레벨로 증가
        public float Def { get; }
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
        public bool IsDead { get; set;  }

        public Job(string name, string occupation, int level, int maxhealth, float atk, float def, int gold, bool isDead, int maxMana)
        {
            Name = name;
            Occupation = occupation;
            Level = level;
            MaxExp = 100;
            Exp = 0;
            MaxHealth = maxhealth;
            Health = maxhealth;
            Atk = atk;
            Def = def;
            Gold = gold;
            IsDead = isDead;
            MaxMana = maxMana;
            Mana = maxMana;

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
        public virtual int Skill_1(string job, float atk, int mana)
        {
            // 랜덤값
            Random rand = new Random();
            // 스킬 데미지
            int err = (int)Math.Ceiling(atk * 10 / 100);
            int skillDamage = rand.Next((int)atk + err + 2, (int)atk + err + 4);

            return skillDamage;

        }
        // 기술 2번 = 사용 기술. 각 직업마다의 2번 기술 및 몬스터의 2번 기술. 플레이어의 레벨이 5를 달성하면 해금됨. 몬스터는 없을 시 사용 못함.
        public virtual int Skill_2(string job, float atk, int mana)
        {
            return 0;

        }
        // 기술 3번 = 사용 기술. 각 직업마다의 3번 기술 및 몬스터의 3번 기술. 플레이어의 레벨이 10을 달성하면 해금됨. 몬스터는 없을 시 사용 못함.
        public virtual int Skill_3(string job, float atk, int mana)
        {
            return 0;
        }
        public void LackMana()
        {
             Console.WriteLine("마나가 부족합니다.");
        }

    }
}
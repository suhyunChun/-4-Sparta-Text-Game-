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
        public string Id { get; }
        // 캐릭터 이름
        public string Name { get; }
        // 직업
        public string Occupation { get; }
        // 레벨 = 전체적인 능력치 및 방어력 증가
        public int Level { get; set; }

        public int MaxExp { get; set; }

        // 경험치
        public int Exp { get; set; }
        // 힘 = 체력 관련 능력치
        public int Strength { get; set; }
        // 민첩성 = 공격력 관련 능력치
        public int Agility { get; set; }
        // 지능 = 마력과 기술 피해 관련 능력치
        public int Intelligence { get; set; }
        // 체력 / 전체체력 을 위한 전체 체력
        public int MaxHealth => 100 + Strength * 20;
        // 체력 = 힘으로 증가
        public int Health { get; set; }
        // 마나/ 전체마나 를 위한 전체 마나
        public int MaxMana => 10 + Intelligence * 2;
        // 마력 = 지능으로 증가
        public int Mana { get; set; }
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
        public List<int> Item { get; set; }
        public List<bool> EquippedList { get; set; }
        // 죽음 여부 = 해치웠나?
        public bool IsDead { get; set; }

        public Job(string id, string name, string occupation, int level, int exp, int maxexp, int strength, int agility, int intelligence, int health, int mana, float plusAtk, float plusDef, int gold, int weapon, int armor, List<int> items, List<bool> equippedList, bool isDead)
        {
            Id = id;
            Name = name;
            Occupation = occupation;
            Level = level;
            Exp = exp;
            Strength = strength;
            Agility = agility;
            Intelligence = intelligence;
            PlusAtk = Atk;
            PlusDef = Def;
            Health = health;
            Mana = mana;
            Gold = gold;
            Weapon = weapon;
            Armor = armor;
            Item = items;
            EquippedList = equippedList;
            IsDead = isDead;
            MaxExp = maxexp;

        }

        /// <summary>
        /// player의 현재 atk로 target을 때리거나
        /// target이 공격할 때 현재 player의 def로 막기 위해 파라미터 추가
        /// </summary>
        public int Attack(Job player, ICharacter target)
        {
            Random rand = new Random();
            var err = player.PlusAtk * 0.1;
            int dps = rand.Next((int)(player.PlusAtk - err), (int)(player.PlusAtk + err + 1));
            dps -= (int)target.Def;
            if (dps <= 1) dps = 1;
            return dps;
        }
        // 기술 1번 = 사용 기술. 각 직업마다의 1번 기술 및 몬스터의 1번 기술. 몬스터는 없을 시 사용 못함.
        public virtual int Skill_1(Job player, ICharacter target)
        {
            int dps = 0;
            int mana = 5;
            int skillFailureProbability = 1;
            if (Occupation == "전사")
            {
                // 랜덤값
                Random rand = new Random();
                // 스킬 데미지
                var err = player.PlusAtk * 0.1;
                dps = rand.Next((int)(player.PlusAtk + err + 2), (int)(player.PlusAtk + err + 4));
                dps -= (int)target.Def;
                if (dps <= 1) dps = 1;
                Mana -= mana;
            }
            else if (Occupation == "마법사")
            {
                int percent = new Random().Next(0, 10);

                if (percent >= skillFailureProbability)
                {
                    // 랜덤값
                    Random rand = new Random();
                    // 스킬 데미지
                    var err = player.PlusAtk * 0.2;
                    dps = rand.Next((int)(player.PlusAtk + err), (int)(player.PlusAtk + err + 8));
                    dps -= (int)target.Def;
                    if (dps <= 1) dps = 1;
                    Mana -= mana;
                }
                else dps = 0; 
            }
            else if (Occupation == "궁수")
            {
                // 랜덤값
                Random rand = new Random();
                // 스킬 데미지
                var err = player.PlusAtk * 0.05;
                dps = rand.Next((int)(player.PlusAtk + err + 3), (int)(player.PlusAtk + err + 4));
                dps -= (int)target.Def;
                if (dps <= 1) dps = 1;
                Mana -= mana;
            }
            return dps;
        }
        // 기술 2번 = 사용 기술. 각 직업마다의 2번 기술 및 몬스터의 2번 기술. 플레이어의 레벨이 5를 달성하면 해금됨. 몬스터는 없을 시 사용 못함.
        public virtual int Skill_2(Job player, ICharacter target)
        {
            int dps = 0;
            int mana = 10;
            if (Level >= 5)
            {
                if (Occupation == "전사")
                {

                }
                else if(Occupation == "마법사")
                {

                }
                else if(Occupation == "궁수")
                {

                }
            }
            return dps;
        }
        // 기술 3번 = 사용 기술. 각 직업마다의 3번 기술 및 몬스터의 3번 기술. 플레이어의 레벨이 10을 달성하면 해금됨. 몬스터는 없을 시 사용 못함.
        public virtual int Skill_3(Job player, ICharacter target)
        {
            int dps = 0;
            int mana = 20;
            if (Level >= 10)
            {
                if (Occupation == "전사")
                {

                }
                else if (Occupation == "마법사")
                {

                }
                else if (Occupation == "궁수")
                {

                }
            }
            return dps;
        }
        public void LackMana()
        {
             Console.WriteLine("마나가 부족합니다.");
        }
    }
}
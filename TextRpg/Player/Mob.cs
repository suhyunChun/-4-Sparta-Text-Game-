using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRpg.Item;

namespace TextRpg.Player
{
    public class Mob : ICharacter
    {
        // 캐릭터 번호
        public string Id => "monster";
        // 캐릭터 이름
        public string Name { get; }
        // 직업
        public string Occupation { get; }
        // 레벨 = 전체적인 능력치 및 방어력 증가
        public int Level { get; set; }
        // 경험치
        public float Exp { get; set; }
        // 힘 = 체력 관련 능력치

        //public float PlusExp { get; set; } Exp만 있어도 될듯??? // 나재민

        public int Strength { get; }
        // 민첩성 = 공격력 관련 능력치
        public int Agility { get; }
        // 지능 = 마력과 기술 피해 관련 능력치
        public int Intelligence { get; }
        // 체력 = 힘으로 증가
        public int Health { get; set; }
        // 마력 = 지능으로 증가
        public int Mana { get; set; }
        // 공격력 = 민첩성으로 증가
        public float Atk { get; }
        // 추가 공격력 = 아이템에 의한 수치 변화
        public float PlusAtk => 0;
        // 방어력 = 레벨로 증가
        public float Def { get; }
        // 추가 방어력 = 아이템에 의한 수치 변화
        public float PlusDef => 0;
        // 골드 = 상점에 사용할 돈 및 몬스터가 뱉을 돈
        public int Gold { get; }
        // 무구 슬롯 = 플레이어가 장착하고 있을 장비 칸. 몬스터는 공백.
        public int Weapon => 0;
        // 방어구 슬롯 = 플레이어가 장착하고 있을 장비 칸. 몬스터는 공백.
        public int Armor => 0;
        // 소지 아이템 = 플레이어가 소지중인 아이템들. 몬스터는 드롭 아이템 목록으로 사용.
        public List<int> Item { get; set; }
        // 죽음 여부 = 해치웠나?
        public bool IsDead { get; set; }

        public Mob(string name, string occupation, int level, float exp, int health, float atk, float def, bool isDead)
        {
            Name = name;
            Occupation = occupation;
            Level = level;
            Exp = exp;
            Health = health;
            Atk = atk;
            Def = def;
            Gold = 3000;
            IsDead = isDead;
        }
        public int Attack(Job player, ICharacter target)
        {
            Random rand = new Random();
            var err = Atk * 0.1;
            int dps = rand.Next((int)(Atk - err), (int)(Atk + 1));
            dps -= (int)player.PlusDef;
            if (dps <= 1) dps = 1;
            return dps;
        }

        public virtual int Skill_1(Job player, ICharacter target)
        {
            return 0;
        }

        public virtual int Skill_2(Job player, ICharacter target)
        {
            return 0;
        }

        public virtual int Skill_3(Job player, ICharacter target)
        {
            return 0;
        }
    }
}

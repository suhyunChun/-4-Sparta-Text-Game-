using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRpg.Player
{
    public class Warrior : Job
    {
        // 체력 200, 공격력 5, 방어력 15
        // 장비 장착 설정 시 아래 참고
        // string id, string name, int level, int exp, float maxexp, int Str, int Agi, int Int, int hp, int mp, float itematk, float itemdef, int gold, int weapon, int armor, List<int> items
        public Warrior(string id, string name, int level, int exp, float maxexp, int Str, int Agi, int Int, int hp, int mp, int gold, List<int> items) : base(id, name, "전사", level, exp, maxexp, Str, Agi, Int, hp, mp, 0, 0, gold, 0, 0, items, false)
        {
            if (Health <= 0)
            {
                IsDead = true;
            }

            if (Exp >= 100)
            {
                Level++;
                MaxExp += 50;
                Strength += 2;
                Agility++;
                Intelligence++;
                Health = MaxHealth;
                Mana = MaxMana;
                Exp = 0;
            }
        }

        public override int Skill_1(Job player, ICharacter target)
        {

            // 공통값
            int vitSkillResult = base.Skill_1(player, target);

            // 재정의하는 내용
            Console.WriteLine("전사의 스킬 발동!");
            Console.WriteLine("알파 스트라이크!");
            Console.WriteLine("");

            return vitSkillResult;
        }

    }

}

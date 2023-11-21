using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRpg.Player
{
    public class Mage : Job
    {
        // 체력 100, 공격력 15, 방어력 5 
        // 장비 장착 설정 시 아래 참고
        // string id, string name, int level, int exp, float maxexp, int Str, int Agi, int Int, int hp, int mp, float itematk, float itemdef, int gold, int weapon, int armor, List<int> items
        public Mage(string id, string name, int level, int exp, float maxexp, int Str, int Agi, int Int, int hp, int mp, int gold, List<int> items) : base(id, name, "마법사", level, exp, maxexp, Str, Agi, Int, hp, mp, 0, 0, gold, 0, 0, items, false)
        {
            if (Health <= 0)
            {
                IsDead = true;
            }
        }
        public override int Skill_1(ICharacter target)
        {

            // 공통값
            int vitSkillResult = base.Skill_1(target);

            // 재정의하는 내용
            if (vitSkillResult == 0)
            {
                Console.WriteLine("마법 캐스팅이 실패 했습니다!");
                Console.WriteLine("마법이 취소됩니다.");
                Console.WriteLine("");
            }

            Console.WriteLine("마법사의 스킬 발동!");
            Console.WriteLine("매직 미사일");
            Console.WriteLine("");

            return vitSkillResult;
        }

    }
}

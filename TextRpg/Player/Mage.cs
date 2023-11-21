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
        public Mage(string id, string name, List<int> items) : base(id, name, "마법사", 1, 0, 2, 2, 4, 3000, 0, 0, items, false)
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

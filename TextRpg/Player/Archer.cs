using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRpg.Player
{

    public class Archer : Job
    {

        // 체력 150, 공격력 12, 방어력 5
        public Archer(string id, string name, List<int> items) : base(id, name, "궁수", 1, 0, 2, 4, 2, 3000, 0, 0, items, false)
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
            Console.WriteLine("궁수의 스킬 발동!");
            Console.WriteLine("연발 사격!");
            Console.WriteLine("");

            return vitSkillResult;
        }

    }
}

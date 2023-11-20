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
        public Warrior(string name) : base(name, "전사", 1, 200, 5, 15, 3000, false, 100)
        {
            if (Health <= 0)
            {
                IsDead = true;
            }

        }

        public override int Skill_1(string job, float atk, int mana)
        {

            // 공통값
            int vitSkillResult = base.Skill_1(job, atk, mana);

            // 재정의하는 내용
            Console.WriteLine("전사의 스킬 발동!");
            Console.WriteLine("알파 스트라이크!");

            // 받아온 스킬 마나를 현재 마나에서 감소시켜줌
            Mana -= mana;

            return vitSkillResult;
        }

    }

}

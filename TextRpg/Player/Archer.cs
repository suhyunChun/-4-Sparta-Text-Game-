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
        public Archer(string name) : base(name, "궁수", 1, 150, 12, 5, false)
        {
            if (Health <= 0)
            {
                IsDead = true;
            }
            if (Exp >= 100)
            {
                Level++;
                Exp += 50;
            }
        }

    }
}

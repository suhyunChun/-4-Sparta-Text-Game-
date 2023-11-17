using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRpg.Player
{

    public class Archer : Job
    {
        // 체력 100, 공격력 20, 방어력 5
        public Archer(string name) : base(name, "궁수", 1, 100, 20, 10, 3000, false)
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

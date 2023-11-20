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
        public Archer(string id, string name, List<int> items) : base(id, name, "궁수", 1, 0, 2, 4, 2, 0, 0, 3000, 0, 0, items, false)
        {
            if (Health <= 0)
            {
                IsDead = true;
            }
            if (Exp >= MaxExp)
            {
                Exp += 50;
                Level++;

            }
        }

    }
}

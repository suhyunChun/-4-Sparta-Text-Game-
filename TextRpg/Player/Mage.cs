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
        public Mage(string name) : base(name, "마법사", 1, 100, 15, 7, 3000, false)
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

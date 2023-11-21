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
    }
}

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
        public Warrior(string name) : base(name, "전사", 200, 5, 15, false)
        {
            if(Health <= 0)
            {
                IsDead = true;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRpg.Player
{
    public class Job : IChracter
    {
        public string Name { get; }

        public string Occupation { get; }

        public int Level { get; protected set; }
    
        public float Exp { get; protected set; }

        public int Health { get; set; }

        public float Atk { get; }

        public float Def { get; }

        public int Gold { get; set; }

        public bool IsDead { get; protected set; }

        public Job(string name, string occupation, int level, int health, float atk, float def, int gold, bool isDead)
        {
            Name = name;
            Occupation = occupation;
            Level = level;
            Exp = 0;
            Health = health;
            Atk = atk;
            Def = def;
            Gold = gold;
            IsDead = isDead;
        }
    }
}

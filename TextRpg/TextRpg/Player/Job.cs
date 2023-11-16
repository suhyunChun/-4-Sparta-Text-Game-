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

        public int Health { get; set; }

        public float Atk { get; }

        public float Def { get; }

        public int Gold { get; }

        public bool IsDead { get; protected set; }

        public Job(string name, string occupation, int health, int atk, int def, bool isDead)
        {
            Name = name;
            Occupation = occupation;
            Health = health;
            Atk = atk;
            Def = def;
            Gold = 1500;
            IsDead = isDead;
        }
    }
}

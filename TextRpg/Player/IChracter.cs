using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRpg.Player
{
    // 캐릭터 인터페이스, 이름 / 직업 / 체력 / 공격력 / 방어력 / 소지골드 / 죽음 여부
    internal interface IChracter
    {
        public string Name { get; }
        // 직업
        public string Occupation { get; }

        public int Level { get; }

        public float Exp { get; }
        public static int Health { get; set; }
        public float Atk { get; }
        public float Def { get; }
        public int Gold { get; }
        public bool IsDead { get; }

    }
}

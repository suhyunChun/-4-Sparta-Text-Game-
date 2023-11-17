using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TextRpg.Item
{
    // 무기
    public class Weapon : Items
    {

        // 공격력
        private int Atk;

        // 무기 생성자
        public Weapon(string name, int grade, int price, int atk, bool isEquiped)
            : base(name, "무기", grade, price, false)
        {
            Atk = atk;
        }

        public void Drop()
        {
            
        }

        public void Use()
        {
   
        }


    }

    // 방어구
    public class Armor : Items
    {
        // 방어력
        private int Def;

        public void Drop()
        {
            
        }

        public void Use()
        {
            
        }

        // 방어구 생성자
        public Armor(string name, int grade, int price, int def, bool isEquiped)
            : base(name, "방어구", grade, price, false)
        {
            Def = def;
        }
    }
}

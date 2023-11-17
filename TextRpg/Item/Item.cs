using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRpg.Item
{
    public class Item : IItem
    {
        public string Name { get; }
        public string Kind { get; }
        public int Grade { get; }
        public int Price { get; }
        public bool IsEquiped { get; set; }

        // 아이템 사용
        public void Use();
        // 아이템 드랍
        public void Drop();


    }

}

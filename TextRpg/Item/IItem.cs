using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRpg.InvenShop;
using TextRpg.Player;

namespace TextRpg.Item
{
    // 아이템 인터페이스, ID값 / 이름 / 등급 / 가격 / 착용여부(필요없음) / 사용 / 드랍
    internal interface IItem
    {
        int Id { get; }
        string Name { get; }
        string Kind { get; }
        int Grade { get; }
        int Price { get; }
        bool IsEquiped { get; set; }

        // 아이템 사용
        void Use(Job player);
        // 아이템 드랍
        void Drop();
    }
}

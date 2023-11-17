using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRpg.Player
{
    // 캐릭터 인터페이스
    internal interface ICharacter
    {
        // 캐릭터 번호
        public int Id { get; }
        // 캐릭터 이름
        public string Name { get; }
        // 직업
        public string Occupation { get; }
        // 레벨 = 전체적인 능력치 및 방어력 증가
        public int Level { get; }
        // 경험치
        public float Exp { get; }
<<<<<<< HEAD
        public static int Health { get; set; }
=======
        // 힘 = 체력 관련 능력치
        public int Strength { get; }
        // 민첩성 = 공격력 관련 능력치
        public int Agility { get; }
        // 지능 = 마력과 기술 피해 관련 능력치
        public int Intelligence { get; }
        // 체력 = 힘으로 증가
        public int Health { get; set; }
        // 마력 = 지능으로 증가
        public int Mana { get; set; }
        // 공격력 = 민첩성으로 증가
>>>>>>> 9c199b94dcc9d57b397971b492788242ae3917e5
        public float Atk { get; }
        // 추가 공격력 = 아이템에 의한 수치 변화
        public float PlusAtk { get; set; }
        // 방어력 = 레벨로 증가
        public float Def { get; }
        // 추가 방어력 = 아이템에 의한 수치 변화
        public float PlusDef { get; set; }
        // 골드 = 상점에 사용할 돈 및 몬스터가 뱉을 돈
        public int Gold { get; }
        // 무구 슬롯 = 플레이어가 장착하고 있을 장비 칸. 몬스터는 공백.
        public int Weapon { get; set; }
        // 방어구 슬롯 = 플레이어가 장착하고 있을 장비 칸. 몬스터는 공백.
        public int Armor { get; set; }
        // 소지 아이템 = 플레이어가 소지중인 아이템들. 몬스터는 드롭 아이템 목록으로 사용.
        public int[] Item { get; set; }
        // 죽음 여부 = 해치웠나?
        public bool IsDead { get; }

        // 공격 = 일반 공격. 공격, 치명타, 회피 등을 체크 후 피해를 줌.
        void Attack(int atk);
    }
}

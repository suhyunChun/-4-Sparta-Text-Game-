using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRpg.Player
{
    // 캐릭터 인터페이스
    public interface ICharacter
    {
        // 캐릭터 번호
        public string Id { get; }
        // 캐릭터 이름
        public string Name { get; }
        // 직업
        public string Occupation { get; }
        // 레벨 = 전체적인 능력치 및 방어력 증가
        public int Level { get; }
        // 경험치
        public int Exp { get; }
        // 힘 = 체력 관련 능력치
        public int Strength { get; }
        // 민첩성 = 공격력 관련 능력치
        public int Agility { get; }
        // 지능 = 마력과 기술 피해 관련 능력치
        public int Intelligence { get; }
        // 체력 = 힘으로 증가
        public int Health { get; }
        // 마력 = 지능으로 증가
        public int Mana { get; }
        // 공격력 = 민첩성으로 증가
        public float Atk { get; }
        // 추가 공격력 = 아이템에 의한 수치 변화
        public float PlusAtk { get; }
        // 방어력 = 레벨로 증가
        public float Def { get; }
        // 추가 방어력 = 아이템에 의한 수치 변화
        public float PlusDef { get; }
        // 골드 = 상점에 사용할 돈 및 몬스터가 뱉을 돈
        public int Gold { get; }
        // 무구 슬롯 = 플레이어가 장착하고 있을 장비 칸. 몬스터는 공백.
        public int Weapon { get; }
        // 방어구 슬롯 = 플레이어가 장착하고 있을 장비 칸. 몬스터는 공백.
        public int Armor { get; }
        // 소지 아이템 = 플레이어가 소지중인 아이템들. 몬스터는 드롭 아이템 목록으로 사용.
        public List<int> Item { get; set; }
        // 죽음 여부 = 해치웠나?
        public bool IsDead { get; }
        // 공격 = 일반 공격. 공격, 치명타, 회피 등을 체크 후 피해를 줌.

        /// <summary>
        /// player의 현재 atk로 target을 때리거나
        /// target이 공격할 때 현재 player의 def로 막기 위해 파라미터 추가
        /// </summary>
        public int Attack(Job player, ICharacter target);
        // 기술 1번 = 사용 기술. 각 직업마다의 1번 기술 및 몬스터의 1번 기술. 몬스터는 없을 시 사용 못함.
        public int Skill_1(Job player, ICharacter target);
        // 기술 2번 = 사용 기술. 각 직업마다의 2번 기술 및 몬스터의 2번 기술. 플레이어의 레벨이 5를 달성하면 해금됨. 몬스터는 없을 시 사용 못함.
        public int Skill_2(Job player, ICharacter target);
        // 기술 3번 = 사용 기술. 각 직업마다의 3번 기술 및 몬스터의 3번 기술. 플레이어의 레벨이 10을 달성하면 해금됨. 몬스터는 없을 시 사용 못함.
        public int Skill_3(Job player, ICharacter target);
    }
}

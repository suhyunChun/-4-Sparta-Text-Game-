using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRpg
{
    class Battle
    {
        //몬스터 List


        /// <summary>
        /// 전투 시작하면 보게 되는 화면
        /// </summary>
        public void BattleScene()
        {
            // 몬스터들 나열

            // 내정보

            // 1.공격 2.도망 3.아이템사용 선택
        }

        /// <summary>
        /// 몬스터 1~4마리 랜덤 출현 (몬스터 중복 가능, 순서 상관 없음)
        /// </summary>
        public void ApperMonster()
        {

        }

        /// <summary>
        /// 1.공격 선택 시 공격할 몬스터 선택
        /// </summary>
        public void SelectMonster()
        {

        }

        /// <summary>
        /// 플레이어의 공격 결과
        /// </summary>
        public void PlayerAttackResult()
        {
            // 몬스터가 전부 죽었다면 Victory
        }

        /// <summary>
        /// 몬스터의 공격 결과
        /// </summary>
        public void MonterAttackResult()
        {
            // 플레이어 HP 0 이라면 Lose

            // 다시 BattlScene()으로
        }

        public void BattleResult(bool isVictory)
        {

        }
    }
}

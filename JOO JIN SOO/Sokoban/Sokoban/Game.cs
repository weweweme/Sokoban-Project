using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    /// <summary>
    /// 소코반 게임과 관련된 데이터 관리
    /// </summary>
    internal class Game
    {
        // 상수는 수정이 될 일 이 없기 때문에 public으로 두어도 된다
        // 벽 관련 상수
        public const int MIN_X = 0;
        public const int MAX_X = 21;
        public const int MIN_Y = 0;
        public const int MAX_Y = 16;
        public const string WALL_SYMBOL = "$";
    }
}

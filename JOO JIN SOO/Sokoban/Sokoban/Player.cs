using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban
{
    internal class Player
    {
        internal enum PlayerDirection
        {
            NONE,
            LEFT,
            RIGHT,
            UP,
            DOWN
        }

        // 클레스의 필드는 보통 숨겨둠. 건물의 전기 배선같은 의미

        private int _x = 10;
        private int _y = 5;
        private string _symbol = "P";
        private PlayerDirection _moveDirection = PlayerDirection.NONE;
        private int _pushedBoxIndex = 0;

        // 접근자
        public int GetX() => _x;
        public int GetY() => _y;
        public string GetSymbol() => _symbol;
        public PlayerDirection GetMoveDirection() => _moveDirection;

        // 설정자
        public void SetX(int newX) => _x = newX;
        public void SetY(int newY) => _y = newY;
        public void SetPushedBoxIndex(int newIndex) => _pushedBoxIndex = newIndex;

        // 접근자(Getter)와 설정자(Setter)
        // 기능 => 메소드 => Player 타입을 다루는 인터페이스(Interface)
        // 메소드는 어떤 기능을 수행함. 밖에서는 기능이 인터페이스를 담당
        public void Move(ConsoleKey key)
        {
            if (key == ConsoleKey.LeftArrow)
            {
                _x = Math.Max(Game.MIN_X + 1, _x - 1);
                _moveDirection = PlayerDirection.LEFT;
            }

            if (key == ConsoleKey.RightArrow)
            {
                _x = Math.Min(_x + 1, Game.MAX_X - 1);
                _moveDirection = PlayerDirection.RIGHT;
            }

            if (key == ConsoleKey.UpArrow)
            {
                _y = Math.Max(Game.MIN_Y + 1, _y - 1);
                _moveDirection = PlayerDirection.UP;
            }

            if (key == ConsoleKey.DownArrow)
            {
                _y = Math.Min(_y + 1, Game.MAX_Y- 1);
                _moveDirection = PlayerDirection.DOWN;
            }
        }
    }
}

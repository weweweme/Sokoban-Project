using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastSamurai
{
    enum PlayerDirection 
    {
        Default,
        Left,
        Right,
        Up,
        Down
    }

    enum PlayerAction
    {
        Default,
        Move,
        Push,
        Grab,
        RangeGrab,
        Kick
    }

    internal class Player
    {
        public int X;
        public int Y;
        public int past_X;
        public int past_Y;
        public PlayerAction playerDO;
        public PlayerDirection moveDirection;
        public int pushedBoxID;
        public int oppositePortalID_X;
        public int oppositePortalID_Y;
    }
}

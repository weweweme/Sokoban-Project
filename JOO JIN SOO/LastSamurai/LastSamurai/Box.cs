using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastSamurai
{
    enum BoxDirection
    {
        Default,
        Left,
        Right,
        Up,
        Down
    }

    struct Box
    {
        public int X;
        public int Y;
        public int past_X;
        public int past_Y;
        public BoxDirection moveDirection;
        public bool IsOnGoal;
        public int oppositePortalID_X;
        public int oppositePortalID_Y;
    }
}

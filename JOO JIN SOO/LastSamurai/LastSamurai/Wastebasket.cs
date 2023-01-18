using LastSamurai;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WasteBasket
{
    //void BoxCollidedBox()
    //{
    //    for (int collidedBoxID = 0; collidedBoxID < BOX_NUMBERS; ++collidedBoxID)
    //    {
    //        if (false == IsCollided(boxes[player.pushedBoxID].X, boxes[player.pushedBoxID].Y, boxes[collidedBoxID].X, boxes[collidedBoxID].Y))
    //            continue;

    //        if (player.pushedBoxID != collidedBoxID)
    //        {
    //            switch (player.moveDirection)
    //            {
    //                case PlayerDirection.Left:
    //                    boxes[player.pushedBoxID].X = boxes[player.pushedBoxID].X + 1;
    //                    player.X = boxes[player.pushedBoxID].X + 1;
    //                    break;
    //                case PlayerDirection.Right:
    //                    boxes[player.pushedBoxID].X = boxes[player.pushedBoxID].X - 1;
    //                    player.X = boxes[player.pushedBoxID].X - 1;
    //                    break;
    //                case PlayerDirection.Up:
    //                    boxes[player.pushedBoxID].Y = boxes[player.pushedBoxID].Y + 1;
    //                    player.Y = boxes[player.pushedBoxID].Y + 1;
    //                    break;
    //                case PlayerDirection.Down:
    //                    boxes[player.pushedBoxID].Y = boxes[player.pushedBoxID].Y - 1;
    //                    player.Y = boxes[player.pushedBoxID].Y - 1;
    //                    break;
    //            }
    //        }
    //    }
    //}

    // void PlayerEventAction(ConsoleKey inputkey)
    // {
    //     if (inputkey == ConsoleKey.Q)
    //     {
    //         // q를 누른 순간 자신의 앞을 검사
    // 
    // 
    // 
    //         switch (player.moveDirection)
    //         {
    // 
    // 
    //             case PlayerDirection.Left:
    //                 MapIdentifier checkData = mapIdentifiers[player.X - 1, player.Y];
    // 
    //                 if (checkData == MapIdentifier.Box)
    //                 {
    //                     for (int boxID = 0; boxID < BOX_NUMBERS; ++boxID)
    //                     {
    //                         if (player.X - 1 == boxes[boxID].X && player.Y == boxes[boxID].Y)
    //                         {
    //                             boxes[boxID].X = boxes[boxID].X - 1;
    //                             boxes[boxID].Y = boxes[boxID].Y;
    //                         }
    //                     }
    //                 }
    // 
    //                 break;
    // 
    //             case PlayerDirection.Right:
    // 
    // 
    //                 break;
    // 
    //             case PlayerDirection.Up:
    // 
    // 
    //                 break;
    // 
    //             case PlayerDirection.Down:
    // 
    // 
    //                 break;
    //         }
    // 
    // 
    //         player.playerDO = PlayerAction.Grab;
    // 
    // 
    //     }
    // }
}
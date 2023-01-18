using System;
using System.ComponentModel;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;
using LastSamurai;

namespace LastSamurai
{
    #region 열거형 선언
    enum MapIdentifier
    {
        Default,
        Wall,
        Player,
        Box,
        Portal,
        Goal,
        Obstacle,
        Item,
        Mine
    }
    #endregion

    class Program
    {
        static void Main()
        {
            #region Initialize Setting
            Console.Clear();
            Console.CursorVisible = false;
            Console.Title = "모든이를 쓰러뜨리고 카이저가 된 마이노";
            Console.BackgroundColor = ConsoleColor.Black;

            const int MAP_X_MAX = 31;
            const int MAP_X_MIN = 0;
            const int MAP_Y_MAX = 21;
            const int MAP_Y_MIN = 0;

            const int BOX_NUMBERS = 3;
            const int PORTAL_NUMBERS = 5;
            const int PORTAL_OPPOSITE = 2;
            const int GOAL_NUMBERS = 3;
            int goalCount = 0;
            const int OBSTACLE_NUMBERS = 15;

            int playerMoveCount = 0;

            MapIdentifier[,] mapIdentifiers = new MapIdentifier[32, 22];
            #endregion

            #region 생성자 모음
            Player player = new Player()
            {
                X = 15,
                Y = 10,
                playerDO = PlayerAction.Default,
                moveDirection = PlayerDirection.Default,
                pushedBoxID = default
            };

            Random mineCoordinate = new Random();
            

            LandMine mines = new LandMine()
            {
                 X = mineCoordinate.Next(10, 12),
                 Y = mineCoordinate.Next(10, 12)
            };


            Box[] boxes = new Box[BOX_NUMBERS]
            {
                new Box { X = 8 , Y = 7, moveDirection = BoxDirection.Default , IsOnGoal = false },
                new Box { X = 6 , Y = 5, moveDirection = BoxDirection.Default , IsOnGoal = false },
                new Box { X = 10, Y = 9, moveDirection = BoxDirection.Default , IsOnGoal = false }
            };

            Obstacle[] obstacles = new Obstacle[OBSTACLE_NUMBERS]
            {
                new Obstacle { X = 1 , Y = 4  },
                new Obstacle { X = 2 , Y = 4  },
                new Obstacle { X = 3 , Y = 4  },
                new Obstacle { X = 4 , Y = 4  },
                new Obstacle { X = 5 , Y = 4  },
                new Obstacle { X = 26, Y = 17 },
                new Obstacle { X = 27, Y = 17 },
                new Obstacle { X = 28, Y = 17 },
                new Obstacle { X = 29, Y = 17 },
                new Obstacle { X = 30, Y = 17 },
                new Obstacle { X = 1 , Y = 17 },
                new Obstacle { X = 2 , Y = 17 },
                new Obstacle { X = 3 , Y = 17 },
                new Obstacle { X = 4 , Y = 17 },
                new Obstacle { X = 5 , Y = 17 }
            };

            Wall[,] widthWalls = new Wall[MAP_X_MAX + 1, 2]
            {
                { new Wall { X = 0  , Y = MAP_Y_MIN } , new Wall { X = 0  , Y = MAP_Y_MAX } },
                { new Wall { X = 1  , Y = MAP_Y_MIN } , new Wall { X = 1  , Y = MAP_Y_MAX } },
                { new Wall { X = 2  , Y = MAP_Y_MIN } , new Wall { X = 2  , Y = MAP_Y_MAX } },
                { new Wall { X = 3  , Y = MAP_Y_MIN } , new Wall { X = 3  , Y = MAP_Y_MAX } },
                { new Wall { X = 4  , Y = MAP_Y_MIN } , new Wall { X = 4  , Y = MAP_Y_MAX } },
                { new Wall { X = 5  , Y = MAP_Y_MIN } , new Wall { X = 5  , Y = MAP_Y_MAX } },
                { new Wall { X = 6  , Y = MAP_Y_MIN } , new Wall { X = 6  , Y = MAP_Y_MAX } },
                { new Wall { X = 7  , Y = MAP_Y_MIN } , new Wall { X = 7  , Y = MAP_Y_MAX } },
                { new Wall { X = 8  , Y = MAP_Y_MIN } , new Wall { X = 8  , Y = MAP_Y_MAX } },
                { new Wall { X = 9  , Y = MAP_Y_MIN } , new Wall { X = 9  , Y = MAP_Y_MAX } },
                { new Wall { X = 10 , Y = MAP_Y_MIN } , new Wall { X = 10 , Y = MAP_Y_MAX } },
                { new Wall { X = 11 , Y = MAP_Y_MIN } , new Wall { X = 11 , Y = MAP_Y_MAX } },
                { new Wall { X = 12 , Y = MAP_Y_MIN } , new Wall { X = 12 , Y = MAP_Y_MAX } },
                { new Wall { X = 13 , Y = MAP_Y_MIN } , new Wall { X = 13 , Y = MAP_Y_MAX } },
                { new Wall { X = 14 , Y = MAP_Y_MIN } , new Wall { X = 14 , Y = MAP_Y_MAX } },
                { new Wall { X = 15 , Y = MAP_Y_MIN } , new Wall { X = 15 , Y = MAP_Y_MAX } },
                { new Wall { X = 16 , Y = MAP_Y_MIN } , new Wall { X = 16 , Y = MAP_Y_MAX } },
                { new Wall { X = 17 , Y = MAP_Y_MIN } , new Wall { X = 17 , Y = MAP_Y_MAX } },
                { new Wall { X = 18 , Y = MAP_Y_MIN } , new Wall { X = 18 , Y = MAP_Y_MAX } },
                { new Wall { X = 19 , Y = MAP_Y_MIN } , new Wall { X = 19 , Y = MAP_Y_MAX } },
                { new Wall { X = 20 , Y = MAP_Y_MIN } , new Wall { X = 20 , Y = MAP_Y_MAX } },
                { new Wall { X = 21 , Y = MAP_Y_MIN } , new Wall { X = 21 , Y = MAP_Y_MAX } },
                { new Wall { X = 22 , Y = MAP_Y_MIN } , new Wall { X = 22 , Y = MAP_Y_MAX } },
                { new Wall { X = 23 , Y = MAP_Y_MIN } , new Wall { X = 23 , Y = MAP_Y_MAX } },
                { new Wall { X = 24 , Y = MAP_Y_MIN } , new Wall { X = 24 , Y = MAP_Y_MAX } },
                { new Wall { X = 25 , Y = MAP_Y_MIN } , new Wall { X = 25 , Y = MAP_Y_MAX } },
                { new Wall { X = 26 , Y = MAP_Y_MIN } , new Wall { X = 26 , Y = MAP_Y_MAX } },
                { new Wall { X = 27 , Y = MAP_Y_MIN } , new Wall { X = 27 , Y = MAP_Y_MAX } },
                { new Wall { X = 28 , Y = MAP_Y_MIN } , new Wall { X = 28 , Y = MAP_Y_MAX } },
                { new Wall { X = 29 , Y = MAP_Y_MIN } , new Wall { X = 29 , Y = MAP_Y_MAX } },
                { new Wall { X = 30 , Y = MAP_Y_MIN } , new Wall { X = 30 , Y = MAP_Y_MAX } },
                { new Wall { X = 31 , Y = MAP_Y_MIN } , new Wall { X = 31 , Y = MAP_Y_MAX } }
            };

            Wall[,] heightWalls = new Wall[MAP_Y_MAX + 1, 2]
            {
                { new Wall { X = MAP_X_MIN , Y = 0  } , new Wall { X = MAP_X_MAX , Y = 0  } },
                { new Wall { X = MAP_X_MIN , Y = 1  } , new Wall { X = MAP_X_MAX , Y = 1  } },
                { new Wall { X = MAP_X_MIN , Y = 2  } , new Wall { X = MAP_X_MAX , Y = 2  } },
                { new Wall { X = MAP_X_MIN , Y = 3  } , new Wall { X = MAP_X_MAX , Y = 3  } },
                { new Wall { X = MAP_X_MIN , Y = 4  } , new Wall { X = MAP_X_MAX , Y = 4  } },
                { new Wall { X = MAP_X_MIN , Y = 5  } , new Wall { X = MAP_X_MAX , Y = 5  } },
                { new Wall { X = MAP_X_MIN , Y = 6  } , new Wall { X = MAP_X_MAX , Y = 6  } },
                { new Wall { X = MAP_X_MIN , Y = 7  } , new Wall { X = MAP_X_MAX , Y = 7  } },
                { new Wall { X = MAP_X_MIN , Y = 8  } , new Wall { X = MAP_X_MAX , Y = 8  } },
                { new Wall { X = MAP_X_MIN , Y = 9  } , new Wall { X = MAP_X_MAX , Y = 9  } },
                { new Wall { X = MAP_X_MIN , Y = 10 } , new Wall { X = MAP_X_MAX , Y = 10 } },
                { new Wall { X = MAP_X_MIN , Y = 11 } , new Wall { X = MAP_X_MAX , Y = 11 } },
                { new Wall { X = MAP_X_MIN , Y = 12 } , new Wall { X = MAP_X_MAX , Y = 12 } },
                { new Wall { X = MAP_X_MIN , Y = 13 } , new Wall { X = MAP_X_MAX , Y = 13 } },
                { new Wall { X = MAP_X_MIN , Y = 14 } , new Wall { X = MAP_X_MAX , Y = 14 } },
                { new Wall { X = MAP_X_MIN , Y = 15 } , new Wall { X = MAP_X_MAX , Y = 15 } },
                { new Wall { X = MAP_X_MIN , Y = 16 } , new Wall { X = MAP_X_MAX , Y = 16 } },
                { new Wall { X = MAP_X_MIN , Y = 17 } , new Wall { X = MAP_X_MAX , Y = 17 } },
                { new Wall { X = MAP_X_MIN , Y = 18 } , new Wall { X = MAP_X_MAX , Y = 18 } },
                { new Wall { X = MAP_X_MIN , Y = 19 } , new Wall { X = MAP_X_MAX , Y = 19 } },
                { new Wall { X = MAP_X_MIN , Y = 20 } , new Wall { X = MAP_X_MAX , Y = 20 } },
                { new Wall { X = MAP_X_MIN , Y = 21 } , new Wall { X = MAP_X_MAX , Y = 21 } }
            };

            Goal[] goals = new Goal[GOAL_NUMBERS]
            {
                new Goal{ X = 1 , Y = 18 },
                new Goal{ X = 1 , Y = 19 },
                new Goal{ X = 1 , Y = 20 }
            };

            Portal[,] portals = new Portal[PORTAL_NUMBERS, PORTAL_OPPOSITE]
            {
                { new Portal { X = 1 , Y = 1 } , new Portal { X = 30 , Y = 18 } },
                { new Portal { X = 1 , Y = 2 } , new Portal { X = 30 , Y = 19 } },
                { new Portal { X = 1 , Y = 3 } , new Portal { X = 30 , Y = 20 } },
                { new Portal { X = 1 , Y = 5 } , new Portal { X = 30 , Y = 16 } },
                { new Portal { X = 1, Y = 16 } , new Portal { X = 30, Y = 5 } }
            };

            #endregion


            #region Identifier LookupTable
            // 전체 식별자 룩업 테이블 구성
            mapIdentifiers[player.X, player.Y] = MapIdentifier.Player;

            mapIdentifiers[mines.X, mines.Y] = MapIdentifier.Mine;

            for (int widthWallID = 0; widthWallID < MAP_X_MAX + 1; ++widthWallID)
            {
                mapIdentifiers[widthWalls[widthWallID, 0].X, widthWalls[widthWallID, 0].Y] = MapIdentifier.Wall;
                mapIdentifiers[widthWalls[widthWallID, 1].X, widthWalls[widthWallID, 1].Y] = MapIdentifier.Wall;
            }
            for (int heightWallID = 0; heightWallID < MAP_Y_MAX + 1; ++heightWallID)
            {
                mapIdentifiers[heightWalls[heightWallID, 0].X, heightWalls[heightWallID, 0].Y] = MapIdentifier.Wall;
                mapIdentifiers[heightWalls[heightWallID, 1].X, heightWalls[heightWallID, 1].Y] = MapIdentifier.Wall;
            }

            for (int boxID = 0; boxID < BOX_NUMBERS; ++boxID)
            {
                mapIdentifiers[boxes[boxID].X, boxes[boxID].Y] = MapIdentifier.Box;
            }

            for (int obstacleID = 0; obstacleID < OBSTACLE_NUMBERS; ++obstacleID)
            {
                mapIdentifiers[obstacles[obstacleID].X, obstacles[obstacleID].Y] = MapIdentifier.Obstacle;
            }

            for (int goalID = 0; goalID < GOAL_NUMBERS; ++goalID)
            {
                mapIdentifiers[goals[goalID].X, goals[goalID].Y] = MapIdentifier.Goal;
            }
            #endregion

            while (true)
            {
                MapReset(); 

                Render();

                if (goalCount == GOAL_NUMBERS)
                {
                    Console.Clear();
                    Console.WriteLine("클리어를 축하합니다");
                    return;
                }

                // Process input
                ConsoleKey key = Console.ReadKey().Key;

                Update(key);

                AfterUpdate();

            }


            void MapReset()
            #region MapCheck
            {
                // 전체 과거좌표 초기화
                player.past_X = player.X;
                player.past_Y = player.Y;

                for (int boxID = 0; boxID < BOX_NUMBERS; ++boxID)
                {
                    boxes[boxID].past_X = boxes[boxID].X;
                    boxes[boxID].past_Y = boxes[boxID].Y;
                }
            }
            #endregion

            void Render()
            #region Render
            {
                
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                ObjectRender(40, 5 , $"Player Action     :  {player.playerDO}");
                ObjectRender(40, 6 , $"Player Move Count :  {playerMoveCount}");

                Console.ForegroundColor = ConsoleColor.White;
                ObjectRender(40, 8 , $"☆ 조작법 ☆");
                ObjectRender(40, 9 , $" ↑↓←→ = 이동키");
                ObjectRender(40, 10, $"   Q      = 레인지 그랩(미구현)");
                ObjectRender(40, 11, $"   W      = 뒤로 끌기(미구현)");
                ObjectRender(40, 12, $"   E      = 되돌리기(미구현)");
                ObjectRender(40, 13, $"   R      = 킥(미구현)");

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                ObjectRender(40, 15, $"★ 오브젝트 ★");
                ObjectRender(40, 16, $"P = Player");
                ObjectRender(40, 17, $"B = Box");
                ObjectRender(40, 18, $"$ = Portal");
                ObjectRender(40, 19, $"& = Obstacle");
                ObjectRender(40, 20, $"* = Wall");

                Console.ForegroundColor = ConsoleColor.Blue;
                for (int goalID = 0; goalID < GOAL_NUMBERS; ++goalID)
                {
                    ObjectRender(goals[goalID].X, goals[goalID].Y, "G");
                }

                Console.ForegroundColor = ConsoleColor.Cyan;
                for (int wallWidthID = 0; wallWidthID < MAP_Y_MAX + 1; ++wallWidthID)
                {
                    ObjectRender(MAP_X_MAX, wallWidthID, "*");
                    ObjectRender(MAP_X_MIN, wallWidthID, "*");
                }
                for (int wallHeightID = 0; wallHeightID < MAP_X_MAX; ++wallHeightID)
                {
                    ObjectRender(wallHeightID, MAP_Y_MAX, "*");
                    ObjectRender(wallHeightID, MAP_Y_MIN, "*");
                }

                Console.ForegroundColor = ConsoleColor.Red;
                for (int boxID = 0; boxID < BOX_NUMBERS; ++boxID)
                {
                    if (true == boxes[boxID].IsOnGoal)
                        ObjectRender(boxes[boxID].X, boxes[boxID].Y, "@");
                    else
                        ObjectRender(boxes[boxID].X, boxes[boxID].Y, "B");
                }

                Console.ForegroundColor = ConsoleColor.Green;
                for (int obstacleID = 0; obstacleID < OBSTACLE_NUMBERS; ++obstacleID)
                {
                    ObjectRender(obstacles[obstacleID].X, obstacles[obstacleID].Y, "&");
                }

                Console.ForegroundColor = ConsoleColor.DarkBlue;
                for (int portalID = 0; portalID < PORTAL_NUMBERS; ++portalID)
                {
                    ObjectRender(portals[portalID, 0].X, portals[portalID, 0].Y, "$");
                    ObjectRender(portals[portalID, 1].X, portals[portalID, 1].Y, "$");
                }

                Console.ForegroundColor = ConsoleColor.Magenta;
                ObjectRender(player.X, player.Y, "C");

            }

            

            void ObjectRender(int x, int y, string symbol)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(symbol);
            }
            #endregion

            void Update(ConsoleKey key)
            #region Update
            {
                MovePlayer(key);
                BoxCollidedPlayer();
                PlayerIntoPortal();
                BoxIntoPortal();

                goalCount = 0;
                for (int boxID = 0; boxID < BOX_NUMBERS; ++boxID)
                {
                    boxes[boxID].IsOnGoal = false;
                    for (int goalID = 0; goalID < GOAL_NUMBERS; ++goalID)
                    {
                        if (IsCollided(boxes[boxID].X, boxes[boxID].Y, goals[goalID].X, goals[goalID].Y))
                        {
                            boxes[boxID].IsOnGoal = true;
                            ++goalCount;
                        }
                    }
                }
            }

            int a = 5;
            

            void MoveToRightOfTarget(out int objX, in int targetX) => objX = targetX - 1;
            void MoveToLeftOfTarget(out int objX, in int targetX) => objX = targetX + 1;
            void MoveToWidthOfTargetY(out int objY, in int targetY) => objY = targetY;

            void MoveToDownOfTarget(out int objY, in int targetY) => objY = targetY - 1;
            void MoveToUpOfTarget(out int objY, in int targetY) => objY = targetY + 1;
            void MoveToHeightOfTargetX(out int objX, in int targetX) => objX = targetX;

            #region PlayerUpdate
            void MovePlayer(ConsoleKey inputkey)
            {
                if (inputkey == ConsoleKey.LeftArrow)
                {
                    MoveToRightOfTarget(out player.X, in player.X); 
                    player.moveDirection = PlayerDirection.Left;
                }
                if (inputkey == ConsoleKey.RightArrow)
                {
                    MoveToLeftOfTarget(out player.X, in player.X);
                    player.moveDirection = PlayerDirection.Right;
                }
                if (inputkey == ConsoleKey.UpArrow)
                {
                    MoveToDownOfTarget(out player.Y, in player.Y);
                    player.moveDirection = PlayerDirection.Up;
                }
                if (inputkey == ConsoleKey.DownArrow)
                {
                    MoveToUpOfTarget(out player.Y, in player.Y); 
                    player.moveDirection = PlayerDirection.Down;
                }

                player.playerDO = PlayerAction.Move;
            }

            void OnCollision(Action action)
            {
                action();
            }

            

            void PlayerToCollidedObject(PlayerDirection playerMoveDirection, ref int objX, ref int objY, in int collidedX, in int collidedY)
            {
                switch (playerMoveDirection)
                {
                    case PlayerDirection.Left:
                        MoveToRightOfTarget(out objX, in collidedX);

                        break;

                    case PlayerDirection.Right:
                        MoveToLeftOfTarget(out objX, in collidedX);

                        break;

                    case PlayerDirection.Up:
                        MoveToDownOfTarget(out objY, in collidedY);

                        break;

                    case PlayerDirection.Down:
                        MoveToUpOfTarget(out objY, in collidedY);

                        break;
                }
            }

            void BoxToCollidedObject(PlayerDirection playerMoveDirection, ref int boxX, ref int boxY, in int collidedX, in int collidedY)
            {
                switch (playerMoveDirection)
                {
                    case PlayerDirection.Left:
                        MoveToRightOfTarget(out boxX, in collidedX);

                        break;

                    case PlayerDirection.Right:
                        MoveToLeftOfTarget(out boxX, in collidedX);

                        break;

                    case PlayerDirection.Up:
                        MoveToDownOfTarget(out boxY, in collidedY);

                        break;

                    case PlayerDirection.Down:
                        MoveToUpOfTarget(out boxY, in collidedY);

                        break;
                }
            }




            void PlayerIntoPortal()
            {
                MapIdentifier checkData = default;

                for (int searchPortalID = 0; searchPortalID < PORTAL_NUMBERS; ++searchPortalID)
                {
                    for (int searchOppositePortalID = 0; searchOppositePortalID < PORTAL_OPPOSITE; ++searchOppositePortalID)
                    {
                        if (false == IsCollided(player.X, player.Y, portals[searchPortalID, searchOppositePortalID].X, portals[searchPortalID, searchOppositePortalID].Y))
                            continue;

                        player.oppositePortalID_X = portals[searchPortalID, (searchOppositePortalID + 1) % 2].X;
                        player.oppositePortalID_Y = portals[searchPortalID, (searchOppositePortalID + 1) % 2].Y;

                        switch (player.moveDirection)
                        {
                            case PlayerDirection.Left:
                                MoveToRightOfTarget(out player.X, portals[searchPortalID, (searchOppositePortalID + 1) % 2].X);
                                MoveToWidthOfTargetY(out player.Y, portals[searchPortalID, (searchOppositePortalID + 1) % 2].Y);

                                checkData = mapIdentifiers[player.oppositePortalID_X - 1, player.oppositePortalID_Y];

                                if (checkData != MapIdentifier.Default)
                                {
                                    Rollback(ref boxes[player.pushedBoxID].X, ref boxes[player.pushedBoxID].Y, boxes[player.pushedBoxID].past_X, boxes[player.pushedBoxID].past_Y);
                                    Rollback(ref player.X, ref player.Y, player.past_X, player.past_Y);
                                }

                                break;
                            case PlayerDirection.Right:
                                MoveToLeftOfTarget(out player.X, portals[searchPortalID, (searchOppositePortalID + 1) % 2].X);
                                MoveToWidthOfTargetY(out player.Y, portals[searchPortalID, (searchOppositePortalID + 1) % 2].Y);

                                checkData = mapIdentifiers[player.oppositePortalID_X + 1, player.oppositePortalID_Y];

                                if (checkData != MapIdentifier.Default)
                                {
                                    Rollback(ref boxes[player.pushedBoxID].X, ref boxes[player.pushedBoxID].Y, boxes[player.pushedBoxID].past_X, boxes[player.pushedBoxID].past_Y);
                                    Rollback(ref player.X, ref player.Y, player.past_X, player.past_Y);
                                }

                                break;
                            case PlayerDirection.Up:
                                MoveToDownOfTarget(out player.Y, portals[searchPortalID, (searchOppositePortalID + 1) % 2].Y);
                                MoveToHeightOfTargetX(out player.X, portals[searchPortalID, (searchOppositePortalID + 1) % 2].X);

                                checkData = mapIdentifiers[player.oppositePortalID_X, player.oppositePortalID_Y - 1];

                                if (checkData != MapIdentifier.Default)
                                {
                                    Rollback(ref boxes[player.pushedBoxID].X, ref boxes[player.pushedBoxID].Y, boxes[player.pushedBoxID].past_X, boxes[player.pushedBoxID].past_Y);
                                    Rollback(ref player.X, ref player.Y, player.past_X, player.past_Y);
                                }

                                break;
                            case PlayerDirection.Down:
                                MoveToUpOfTarget(out player.Y, portals[searchPortalID, (searchOppositePortalID + 1) % 2].Y);
                                MoveToHeightOfTargetX(out player.X, portals[searchPortalID, (searchOppositePortalID + 1) % 2].X);

                                checkData = mapIdentifiers[player.oppositePortalID_X, player.oppositePortalID_Y + 1];

                                if (checkData != MapIdentifier.Default)
                                {
                                    Rollback(ref boxes[player.pushedBoxID].X, ref boxes[player.pushedBoxID].Y, boxes[player.pushedBoxID].past_X, boxes[player.pushedBoxID].past_Y);
                                    Rollback(ref player.X, ref player.Y, player.past_X, player.past_Y);
                                }

                                break;
                        }

           

        
                    }

                    

                    

                }

                
            }
            #endregion

            #region BoxUpdate
            void BoxCollidedPlayer()
            {
                for (int pushedBoxID = 0; pushedBoxID < BOX_NUMBERS; ++pushedBoxID)
                {
                    if (false == IsCollided(player.X, player.Y, boxes[pushedBoxID].X, boxes[pushedBoxID].Y))
                        continue;
                    
                    player.playerDO = PlayerAction.Push;
                    player.pushedBoxID = pushedBoxID;

                    OnCollision(() =>
                    {
                        BoxToCollidedObject(player.moveDirection, ref boxes[pushedBoxID].X, ref boxes[pushedBoxID].Y, in player.X, in player.Y);
                    });
                }
            }

            void BoxIntoPortal()
            {
                MapIdentifier checkData = default;

                for (int searchPortalID = 0; searchPortalID < PORTAL_NUMBERS; ++searchPortalID)
                {
                    for (int searchOppositePortalID = 0; searchOppositePortalID < PORTAL_OPPOSITE; ++searchOppositePortalID)
                    {
                        if (false == IsCollided(boxes[player.pushedBoxID].X, boxes[player.pushedBoxID].Y, portals[searchPortalID, searchOppositePortalID].X, portals[searchPortalID, searchOppositePortalID].Y))
                            continue;

                        boxes[player.pushedBoxID].oppositePortalID_X = portals[searchPortalID, (searchOppositePortalID + 1) % 2].X;
                        boxes[player.pushedBoxID].oppositePortalID_Y = portals[searchPortalID, (searchOppositePortalID + 1) % 2].Y;

                        switch (player.moveDirection)
                        {
                            case PlayerDirection.Left:
                                MovedCoordinate(ref player.X, ref player.Y, portals[searchPortalID, (searchOppositePortalID + 1) % 2].X - 1, portals[searchPortalID, (searchOppositePortalID + 1) % 2].Y);
                                MovedCoordinate(ref boxes[player.pushedBoxID].X, ref boxes[player.pushedBoxID].Y, player.X - 1, player.Y);

                                checkData = mapIdentifiers[boxes[player.pushedBoxID].oppositePortalID_X - 1, boxes[player.pushedBoxID].oppositePortalID_Y];
                                
                                if (checkData != MapIdentifier.Default)
                                {
                                    Rollback(ref boxes[player.pushedBoxID].X, ref boxes[player.pushedBoxID].Y, boxes[player.pushedBoxID].past_X, boxes[player.pushedBoxID].past_Y);
                                    Rollback(ref player.X, ref player.Y, player.past_X, player.past_Y);
                                }

                                break;
                            case PlayerDirection.Right:
                                MovedCoordinate(ref player.X, ref player.Y, portals[searchPortalID, (searchOppositePortalID + 1) % 2].X + 1, portals[searchPortalID, (searchOppositePortalID + 1) % 2].Y);
                                MovedCoordinate(ref boxes[player.pushedBoxID].X, ref boxes[player.pushedBoxID].Y, player.X + 1, player.Y);

                                checkData = mapIdentifiers[boxes[player.pushedBoxID].oppositePortalID_X + 1, boxes[player.pushedBoxID].oppositePortalID_Y];

                                if (checkData != MapIdentifier.Default)
                                {
                                    Rollback(ref boxes[player.pushedBoxID].X, ref boxes[player.pushedBoxID].Y, boxes[player.pushedBoxID].past_X, boxes[player.pushedBoxID].past_Y);
                                    Rollback(ref player.X, ref player.Y, player.past_X, player.past_Y);
                                }

                                break;
                            case PlayerDirection.Up:
                                MovedCoordinate(ref player.X, ref player.Y, portals[searchPortalID, (searchOppositePortalID + 1) % 2].X, portals[searchPortalID, (searchOppositePortalID + 1) % 2].Y - 1);
                                MovedCoordinate(ref boxes[player.pushedBoxID].X, ref boxes[player.pushedBoxID].Y, player.X, player.Y - 1);

                                checkData = mapIdentifiers[boxes[player.pushedBoxID].oppositePortalID_X, boxes[player.pushedBoxID].oppositePortalID_Y - 1];

                                if (checkData != MapIdentifier.Default)
                                {
                                    Rollback(ref boxes[player.pushedBoxID].X, ref boxes[player.pushedBoxID].Y, boxes[player.pushedBoxID].past_X, boxes[player.pushedBoxID].past_Y);
                                    Rollback(ref player.X, ref player.Y, player.past_X, player.past_Y);
                                }

                                break;
                            case PlayerDirection.Down:
                                MovedCoordinate(ref player.X, ref player.Y, portals[searchPortalID, (searchOppositePortalID + 1) % 2].X, portals[searchPortalID, (searchOppositePortalID + 1) % 2].Y + 1);
                                MovedCoordinate(ref boxes[player.pushedBoxID].X, ref boxes[player.pushedBoxID].Y, player.X, player.Y + 1);

                                checkData = mapIdentifiers[boxes[player.pushedBoxID].oppositePortalID_X, boxes[player.pushedBoxID].oppositePortalID_Y + 1];

                                if (checkData != MapIdentifier.Default)
                                {
                                    Rollback(ref boxes[player.pushedBoxID].X, ref boxes[player.pushedBoxID].Y, boxes[player.pushedBoxID].past_X, boxes[player.pushedBoxID].past_Y);
                                    Rollback(ref player.X, ref player.Y, player.past_X, player.past_Y);
                                }

                                break;
                        }
                    }
                }

                #endregion

            }

            // 단순 좌표 이동 함수
            void MovedCoordinate(ref int x1, ref int y1, int x2, int y2)
            {
                x1 = x2;
                y1 = y2;
            }

            // 과거로 가는 함수
            void Rollback(ref int x1, ref int y1, int x2, int y2)
            {
                x1 = x2;
                y1 = y2;
            }

            // 충돌확인 함수
            bool IsCollided(int x1, int y1, int x2, int y2)
            {
                if (x1 == x2 && y1 == y2)
                    return true;
                else
                    return false;
            }
            #endregion

            void AfterUpdate()
            #region AfterUpdate
            {
                PlayerScan();
                BoxScan();

                MapClear();
                MapIdentifierUpdate();
            }

            // 플레이어 충돌 스캔
            void PlayerScan()
            #region PlayerScan
            {
                MapIdentifier checkData = mapIdentifiers[player.X, player.Y];

                switch (checkData)
                {
                    case MapIdentifier.Wall:
                        player.X = player.past_X;
                        player.Y = player.past_Y;

                        break;

                    case MapIdentifier.Obstacle:
                        player.X = player.past_X;
                        player.Y = player.past_Y;

                        break;

                    case MapIdentifier.Mine:
                        Environment.Exit("5);

                        break;

                }
            }
            #endregion

            // 박스 충돌 스캔
            void BoxScan()
            #region BoxScan
            {
                // 어떤 행동으로 박스가 밀렸는가에 따라 다른 결과값을 반환할 수 있지 않을까?
                // if (player.playerDO == PlayerAction.Push)
                    
                MapIdentifier checkData = mapIdentifiers[boxes[player.pushedBoxID].X, boxes[player.pushedBoxID].Y];

                switch (checkData)
                {
                    case MapIdentifier.Wall:
                        Rollback(ref boxes[player.pushedBoxID].X, ref boxes[player.pushedBoxID].Y, boxes[player.pushedBoxID].past_X, boxes[player.pushedBoxID].past_Y);
                        Rollback(ref player.X, ref player.Y, player.past_X, player.past_Y);

                        break;

                    case MapIdentifier.Player:
                        Rollback(ref boxes[player.pushedBoxID].X, ref boxes[player.pushedBoxID].Y, boxes[player.pushedBoxID].past_X, boxes[player.pushedBoxID].past_Y);
                        Rollback(ref player.X, ref player.Y, player.past_X, player.past_Y);

                        break;

                    case MapIdentifier.Box:
                        for (int collidedboxID = 0; collidedboxID < BOX_NUMBERS; ++collidedboxID)
                        {
                            for (int targetBoxID = 0; targetBoxID < BOX_NUMBERS; ++targetBoxID)
                            {
                                if (false == IsCollided(boxes[collidedboxID].X, boxes[collidedboxID].Y, boxes[targetBoxID].X, boxes[targetBoxID].Y))
                                    continue;

                                if (collidedboxID != targetBoxID)
                                {
                                    Rollback(ref boxes[collidedboxID].X, ref boxes[collidedboxID].Y, boxes[collidedboxID].past_X, boxes[collidedboxID].past_Y);
                                    Rollback(ref player.X, ref player.Y, player.past_X, player.past_Y);
                                    
                                }
                            }
                        }

                        break;

                    case MapIdentifier.Obstacle:
                        Rollback(ref boxes[player.pushedBoxID].X, ref boxes[player.pushedBoxID].Y, boxes[player.pushedBoxID].past_X, boxes[player.pushedBoxID].past_Y);
                        Rollback(ref player.X, ref player.Y, player.past_X, player.past_Y);
                        --playerMoveCount;

                        break;
                }
            }

            #endregion

            // 업데이트 이후 맵 쓰레기값 청소
            void MapClear()
            {
                mapIdentifiers[player.past_X, player.past_Y] = MapIdentifier.Default;

                for (int boxID = 0; boxID < BOX_NUMBERS; ++boxID)
                {
                    mapIdentifiers[boxes[boxID].past_X, boxes[boxID].past_Y] = MapIdentifier.Default;
                }
            }

            // 업데이트 이후 맵 데이터 갱신
            void MapIdentifierUpdate()
            {
                mapIdentifiers[player.X, player.Y] = MapIdentifier.Player;

                for (int boxID = 0; boxID < BOX_NUMBERS; ++boxID)
                {
                    mapIdentifiers[boxes[boxID].X, boxes[boxID].Y] = MapIdentifier.Box;
                }

                for (int obstacleID = 0; obstacleID < OBSTACLE_NUMBERS; ++obstacleID)
                {
                    mapIdentifiers[obstacles[obstacleID].X, obstacles[obstacleID].Y] = MapIdentifier.Obstacle;
                }

                ++playerMoveCount;
            }
            #endregion

        }
    }
}
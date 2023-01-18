namespace Sokoban
{
    class Program
    {
        static void Main()
        {
            Console.ResetColor();   // 컬러 초기화
            Console.CursorVisible = false;  // 커서 숨기기
            Console.Title = "마이노이 라비린스 월.mk2";  // 타이틀 설정
            Console.BackgroundColor = ConsoleColor.Green;   // 배경색 설정
            Console.ForegroundColor = ConsoleColor.Black;   // 글꼴색 설정
            Console.Clear();    // 출력된 내용 지우기

            // player는 player 클래스의 인스턴스(Instance)다
            Player player = new Player();
            Renderer renderer = new Renderer();


            #region 상수 모음
            // 박스 관련 상수
            const int FirstBox_X = 5;
            const int FirstBox_Y = 5;
            const int SecondBox_X = 9;
            const int SecondBox_Y = 9;
            const int ThirdBox_X = 3;
            const int ThirdBox_Y = 3;
            const string boxSymbol = "O";
            const int BOX_COUNT = 3;



            // 장애물 관련 상수
            const int FIRST_OBTICLE_X = 12;
            const int FIRST_OBTICLE_Y = 8;
            const int SECOND_OBTICLE_X = 13;
            const int SECOND_OBTICLE_Y = 8;
            const int THIRD_OBTICLE_X = 12;
            const int THIRD_OBTICLE_Y = 7;
            const int FOURTH_OBTICLE_X = 13;
            const int FOURTH_OBTICLE_Y = 7;

            const int FIFTH_OBTICLE_X = 2;
            const int FIFTH_OBTICLE_Y = 10;
            const int SIXTH_OBTICLE_X = 3;
            const int SIXTH_OBTICLE_Y = 10;
            const int SEVEN_OBTICLE_X = 4;
            const int SEVEN_OBTICLE_Y = 10;
            const int EIGHTH_OBTICLE_X = 5;
            const int EIGHTH_OBTICLE_Y = 10;
            const int NINTH_OBTICLE_X = 6;
            const int NINTH_OBTICLE_Y = 10;
            const int TENTH_OBTICLE_X = 7;
            const int TENTH_OBTICLE_Y = 10;
            const string obticleSymbol = "W";
            const int OBTICLE_COUNT = 10;

            // 골 관련 상수
            const int FIRST_GOAL_X = 14;
            const int FIRST_GOAL_Y = 7;
            const int SECOND_GOAL_X = 2;
            const int SECOND_GOAL_Y = 4;
            const int THIRD_GOAL_X = 4;
            const int THIRD_GOAL_Y = 5;
            const string goalSymbol = "G";
            const int GOAL_COUNT = 3;
            int goalCount = 0;

            // 인 게임 변수모음


            int[] boxX = { FirstBox_X, SecondBox_X, ThirdBox_X };
            int[] boxY = { FirstBox_Y, SecondBox_Y, ThirdBox_Y };

            int[] obticleX = { FIRST_OBTICLE_X, SECOND_OBTICLE_X, THIRD_OBTICLE_X, FOURTH_OBTICLE_X, FIFTH_OBTICLE_X, SIXTH_OBTICLE_X, SEVEN_OBTICLE_X, EIGHTH_OBTICLE_X, NINTH_OBTICLE_X, TENTH_OBTICLE_X };
            int[] obticleY = { FIRST_OBTICLE_Y, SECOND_OBTICLE_Y, THIRD_OBTICLE_Y, FOURTH_OBTICLE_Y, FIFTH_OBTICLE_Y, SIXTH_OBTICLE_Y, SEVEN_OBTICLE_Y, EIGHTH_OBTICLE_Y, NINTH_OBTICLE_Y, TENTH_OBTICLE_Y };

            int[] goalX = { FIRST_GOAL_X, SECOND_GOAL_X, THIRD_GOAL_X };
            int[] goalY = { FIRST_GOAL_Y, SECOND_GOAL_Y, THIRD_GOAL_Y };

            bool[] isBoxOnGoal = new bool[GOAL_COUNT];
            #endregion


            while (true)
            {
                Render();

                ProcessInput(out ConsoleKey key);

                Update(key);

                #region GameClear
                if (goalCount == GOAL_COUNT)
                {
                    Console.Clear();
                    Console.WriteLine("축하드립니다!");
                    return;
                }
                #endregion
                void Render()
                #region Render
                {
                    // 이전 프레임 지우기
                    Console.Clear();
                    // 플레이어 렌더
                    renderer.Render(player.GetX(), player.GetY(), player.GetSymbol());

                    // 골 렌더
                    RenderEntityReapeat(goalSymbol, goalX, goalY);

                    // 장애물 렌더
                    RenderEntityReapeat(obticleSymbol, obticleX, obticleY);

                    // 박스 출력
                    for (int i = 0; i < BOX_COUNT; ++i)
                    {
                        Console.SetCursorPosition(boxX[i], boxY[i]);

                        if (isBoxOnGoal[i])
                        {
                            Console.Write("@");
                        }
                        else
                        {
                            Console.Write(boxSymbol);
                        }
                    }

                    // 벽 구현
                    for (int i = 1; i < Game.MAX_Y; ++i)
                    {
                        RenderWallWidth(Game.WALL_SYMBOL, Game.MIN_X, Game.MAX_X, i);
                    }
                    for (int i = 0; i - 1 < Game.MAX_X; ++i)
                    {
                        Console.SetCursorPosition(i, Game.MIN_Y);
                        Console.Write(Game.WALL_SYMBOL);

                        Console.SetCursorPosition(i, Game.MAX_Y);
                        Console.Write(Game.WALL_SYMBOL);
                    }

                }
                #endregion
                void RenderEntityReapeat(in string symbol, in int[] EntitiX, in int[] EntitiY)
                {
                    for (int i = 0; i < EntitiX.Length; ++i)
                    {
                        Console.SetCursorPosition(EntitiX[i], EntitiY[i]);
                        Console.Write(symbol);
                    }
                }
                void RenderWallWidth(in string symbol, in int wallLeft, in int wallRight, in int count)
                {
                    Console.SetCursorPosition(wallLeft, count);
                    Console.Write(symbol);

                    Console.SetCursorPosition(wallRight, count);
                    Console.Write(symbol);
                }


                void ProcessInput(out ConsoleKey inputKey)
                #region ProcessInput
                {
                    inputKey = Console.ReadKey().Key;
                }
                #endregion

                void Update(ConsoleKey key)
                #region Update
                {
                    #region 플레이어 업데이트
                    player.Move(key);


                    // 플레이어가 장애물을 만난 상황
                    for (int i = 0; i < obticleX.Length; ++i)
                    {
                        if (false == IsCollided(player.GetX(), obticleX[i], player.GetY(), obticleY[i]))
                        {
                            continue;
                        }

                        switch (player.GetMoveDirection())
                        {
                            case Player.PlayerDirection.LEFT:
                                player.SetX(obticleX[i] + 1);
                                break;
                            case Player.PlayerDirection.RIGHT:
                                player.SetX(obticleX[i] - 1);
                                break;
                            case Player.PlayerDirection.UP:
                                player.SetY(obticleY[i] + 1);
                                break;
                            case Player.PlayerDirection.DOWN:
                                player.SetY(obticleY[i] - 1);
                                break;
                        }
                        break;
                    }

                    // 플레이어와 박스가 만난 상황
                    for (int i = 0; i < BOX_COUNT; ++i)
                    {
                        if (false == IsCollided(player.GetX(), boxX[i], player.GetY(), boxY[i]))
                        {
                            continue;
                        }

                        switch (player.GetMoveDirection())
                        {
                            case Player.PlayerDirection.LEFT:
                                boxX[i] = Math.Max(1, boxX[i] - 1);
                                player.SetX(boxX[i] + 1);
                                break;
                            case Player.PlayerDirection.RIGHT:
                                boxX[i] = Math.Min(boxX[i] + 1, 20);
                                player.SetX(boxX[i] - 1);
                                break;
                            case Player.PlayerDirection.UP:
                                boxY[i] = Math.Max(1, boxY[i] - 1);
                                player.SetY(boxY[i] + 1);
                                break;
                            case Player.PlayerDirection.DOWN:
                                boxY[i] = Math.Min(boxY[i] + 1, 15);
                                player.SetY(boxY[i] - 1);
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine($"[Error] 플레이어 이동 방향 데이터가 오류입니다. : {player.GetMoveDirection()}");
                                return;
                        }
                        #endregion
                    }

                    #region 박스 업데이트

                    // 박스가 장애물을 만난 상황
                    for (int i = 0; i < BOX_COUNT; ++i)
                    {
                        for (int j = 0; j < OBTICLE_COUNT; ++j)
                        {
                            if (false == IsCollided(boxX[i], obticleX[j], boxY[i], obticleY[j]))
                            {
                                continue;
                            }

                            switch (player.GetMoveDirection())
                            {
                                case Player.PlayerDirection.LEFT:
                                    boxX[i] = boxX[i] + 1;
                                    player.SetX(boxX[i] + 1);
                                    break;
                                case Player.PlayerDirection.RIGHT:
                                    boxX[i] = boxX[i] - 1;
                                    player.SetX(boxX[i] - 1);
                                    break;
                                case Player.PlayerDirection.UP:
                                    boxY[i] = boxY[i] + 1;
                                    player.SetY(boxY[i] + 1);
                                    break;
                                case Player.PlayerDirection.DOWN:
                                    boxY[i] = boxY[i] - 1;
                                    player.SetY(boxY[i] - 1);
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine($"[Error] 플레이어 이동 방향 데이터가 오류입니다. : {player.GetMoveDirection}");
                                    return;
                            }
                            break;
                        }
                    }

                    // 박스와 박스가 만난 상황
                    for (int i = 0; i < BOX_COUNT; ++i)
                    {
                        for (int j = 0; j < BOX_COUNT; ++j)
                        {
                            if (i != j && boxX[i] == boxX[j] && boxY[i] == boxY[j])
                            {
                                switch (player.GetMoveDirection())
                                {
                                    case Player.PlayerDirection.LEFT:
                                        boxX[i] = boxX[i] + 1;
                                        player.SetX(player.GetX() + 1);
                                        break;
                                    case Player.PlayerDirection.RIGHT:
                                        boxX[i] = boxX[i] - 1;
                                        player.SetX(player.GetX() - 1);
                                        break;
                                    case Player.PlayerDirection.UP:
                                        boxY[i] = boxY[i] + 1;
                                        player.SetY(player.GetY() + 1);
                                        break;
                                    case Player.PlayerDirection.DOWN:
                                        boxY[i] = boxY[i] - 1;
                                        player.SetY(player.GetY() - 1);
                                        break;
                                    default:
                                        Console.Clear();
                                        Console.WriteLine($"[Error] 플레이어 이동 방향 데이터가 오류입니다. : {player.GetMoveDirection}");
                                        return;
                                }
                                break;
                            }
                        }
                    }




                    // 박스가 골에 들어간 상황
                    goalCount = 0;
                    for (int i = 0; i < BOX_COUNT; ++i)
                    {
                        isBoxOnGoal[i] = false;

                        for (int j = 0; j < GOAL_COUNT; ++j)
                        {
                            if (goalX[j] == boxX[i] && goalY[j] == boxY[i])
                            {
                                ++goalCount;
                                isBoxOnGoal[i] = true;
                                break;
                            }
                        }
                    }
                    #endregion



                }
                #endregion

                bool IsCollided(int x1, int x2, int y1, int y2)
                {
                    if (x1 == x2 && y1 == y2)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }

            }
        }
    }
}


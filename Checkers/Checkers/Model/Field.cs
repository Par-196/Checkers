using Checkers.Model;
using Checkers.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Permissions;

namespace Checkers.Model
{
    public class Field
    {
        public Cell[,] Cells { get; set; }

        public Field(Cell[,] cells, User firstPlayer, User secondPlayer)
        {
            bool cellType = true;

            for (int x = 0; x < cells.GetLength(0); x++)
            {
                cellType = !cellType;
                for (int y = 0; y < cells.GetLength(1); y++)
                {
                    cellType = !cellType;
                    if (cellType)
                    {
                        cells[x, y] = new Cell(TypeCell.WhiteCell);
                    }
                    else
                    {
                        cells[x, y] = new Cell(TypeCell.BlackCell);
                    }
                }
            }
            PutCheckersOnField(cells, firstPlayer);
            PutCheckersOnField(cells, secondPlayer);

        }

        public void PutCheckersOnField(Cell[,] cells, User user)
        {
            int checker = 0;
            int x = 0;

            if (!user.CheckerColorWhite)
            {
                x = 5;
            }
            for (; x < cells.GetLength(0); x++)
            {
                for (int y = 0; y < cells.GetLength(1); y++)
                {
                    for (int i = 0; i < cells[x, y].Point.GetLength(0); i++)
                    {
                        for (int j = 0; j < cells[x, y].Point.GetLength(1); j++)
                        {
                            if (cells[x, y].Point[i, j].Type == TypeCell.BlackCell && checker < 12)
                            {
                                cells[x, y].Point[i, j].Type = user.Checker[checker].Points[i, j].Type;
                                cells[x, y].IsCheckerHere = true;
                                if (user.CheckerColorWhite)
                                {
                                    cells[x, y].IsWhiteChecker = true;
                                }
                                else
                                {
                                    cells[x, y].IsWhiteChecker = false;
                                }
                            }
                        }
                    }
                    if (cells[x, y].Point[0, 0].Type == TypeCell.BlackCell)
                    {
                        checker++;
                    }
                }
            }
        }

        public void DrawField(Cell[,] cells)
        {
            Console.WriteLine("\n             A          B          C          D          E          F          G          H\n");

            for (int i = 0; i < cells[0, 0].Point.GetLength(1) * 8 + 9; i++)
            {
                if (i > 6)
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("\u2584");
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();

            for (int x = 0; x < cells.GetLength(0); x++)
            {
                for (int i = 0; i < cells[x, 0].Point.GetLength(0); i++)
                {

                    Console.Write(i == cells[x, 0].Point.GetLength(0) / 2 ? $"   {x}   " : $"       ");
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.Write($" ");

                    for (int y = 0; y < cells.GetLength(1); y++)
                    {
                        for (int j = 0; j < cells[x, y].Point.GetLength(1); j++)
                        {
                            Console.Write(cells[x, y].Point[i, j]);
                        }
                    }

                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.Write($" ");
                    Console.ResetColor();
                    Console.WriteLine();

                }

            }

            for (int i = 0; i < cells[0, 0].Point.GetLength(1) * 8 + 9; i++)
            {
                if (i > 6)
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("\u2580");
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(" ");
                }
            }
        }

        public bool ChooseAChecker(Cell[,] cells, User user, int xChecker, int yChecker, List<int[]> listOfPossibleMovesToBeatAChecker, bool posible)
        {
            if (cells[xChecker, yChecker].IsCheckerHere)
            {
                if (cells[xChecker, yChecker].IsWhiteChecker == user.CheckerColorWhite)
                {
                    if (posible == true)
                    {
                        bool userXAndYAreInTheArray = false;
                        foreach (var array in listOfPossibleMovesToBeatAChecker)
                        {
                            if (xChecker == array[0] && yChecker == array[1])
                            {
                                userXAndYAreInTheArray = true;
                            }
                        }
                        if(userXAndYAreInTheArray)
                        {
                            return true;
                        }
                        else
                        {
                            Console.SetCursorPosition(106, 10);
                            Console.Write("You must beat an opponent's checker.");
                            Console.SetCursorPosition(106, 12);
                            Console.Write("Choose the checker you want to beat.");
                            Console.SetCursorPosition(106, 14);
                            Console.ReadLine();
                            return false;
                        }
                    }
                    else if (!IsTheCheckerBlocked(cells, user, xChecker, yChecker))
                    {
                        PossibleCheckerMoves(cells, user, xChecker, yChecker);
                        Console.Clear();
                        DrawField(cells);
                        return true;
                    }
                    else
                    {
                        Console.Clear();
                        DrawField(cells);
                        Console.SetCursorPosition(106, 4);
                        Console.Write("You can't choose this checker, it's blocked");
                        Console.SetCursorPosition(106, 6);
                        Console.ReadLine();
                        return false;
                    }
                }
                else
                {
                    Console.SetCursorPosition(106, 10);
                    Console.Write("This checker is not yours.");
                    Console.SetCursorPosition(106, 12);
                    Console.Write("Choose another one.");
                    Console.SetCursorPosition(106, 14);
                    Console.ReadLine();
                    return false;
                }
            }
            else
            {
                Console.SetCursorPosition(106, 10);
                Console.Write("There is no checker on this cell.");
                Console.SetCursorPosition(106, 12);
                Console.Write("Choose another one.");
                Console.SetCursorPosition(106, 14);
                Console.ReadLine();
                return false;
            }
        }

        public bool IsTheCheckerBlocked(Cell[,] cells, User user, int xChecker, int yChecker)
        {
            int xDirection = user.CheckerColorWhite ? 1 : -1;
            int forwardX = xChecker + xDirection;

            int left = yChecker - 1;
            int right = yChecker + 1;

            cells[xChecker, yChecker].SelectedChecker(user);

            if (xChecker != 8  || xChecker != 0 || forwardX <= cells.GetLength(0))
            {
                if (left >= 0)
                {
                    if (right < 8)
                    {
                        if (!cells[forwardX, left].IsCheckerHere || !cells[forwardX, right].IsCheckerHere)
                        {
                            if (!cells[forwardX, left].IsCheckerHere)
                            {
                                cells[forwardX, left].PosibleMoveCell();
                                cells[forwardX, right].ImpossibleMove(cells, forwardX, right);
                                return false;
                            }
                            else
                            {
                                cells[forwardX, right].PosibleMoveCell();
                                cells[forwardX, left].ImpossibleMove(cells, forwardX, left);
                                return false;
                            }
                        }
                        if (cells[forwardX, left].IsCheckerHere && cells[forwardX, right].IsCheckerHere)
                        {
                            cells[forwardX, left].ImpossibleMove(cells, forwardX, left);
                            cells[forwardX, right].ImpossibleMove(cells, forwardX, right);
                            return true;
                        }

                    }
                    if (cells[forwardX, left].IsCheckerHere)
                    {
                        cells[forwardX, left].ImpossibleMove(cells, forwardX, left);
                        return true;
                    }
                }
                else if (right < 8)
                {
                    if (cells[forwardX, right].IsCheckerHere)
                    {
                        cells[forwardX, right].ImpossibleMove(cells, forwardX, right);
                        return true;
                    }
                }
            }
            else
            {
                Console.WriteLine("You have gone out of bounds");
            }
            return false;
        }

        public void PossibleCheckerMoves(Cell[,] cells, User user, int xChecker, int yChecker)
        {

            int xDirection = user.CheckerColorWhite ? 1 : -1;
            int forwardX = xChecker + xDirection;

            int left = yChecker - 1;
            int right = yChecker + 1;

            cells[xChecker, yChecker].SelectedChecker(user);

            if (xChecker != 8 || xChecker != 0 || forwardX <= cells.GetLength(0))
            {
                if (left >= 0)
                {
                    if (right < 8)
                    {
                        if (!cells[forwardX, left].IsCheckerHere && !cells[forwardX, right].IsCheckerHere)
                        {
                            cells[forwardX, right].PosibleMoveCell();
                            cells[forwardX, left].PosibleMoveCell();
                        }
                    }
                    else if (!cells[forwardX, left].IsCheckerHere)
                    {
                        cells[forwardX, left].PosibleMoveCell();
                    }
                }
                else if (right < 8)
                {
                    if (!cells[forwardX, right].IsCheckerHere)
                    {
                        cells[forwardX, right].PosibleMoveCell();
                    }
                }
            }
            else
            {
                Console.WriteLine("You have gone out of bounds");
                Console.ReadLine();
            }
        }

        public (bool, List<int[]>) IsItPossibleToBeatACheckers(Cell[,] cells, User user, List<int[]> listOfPossibleMovesToBeatAChecker, bool posible)
        {
            for (int xChecker = 0; xChecker < cells.GetLength(0); xChecker++)
            {
                for (int yChecker = 0; yChecker < cells.GetLength(1); yChecker++)
                {
                    for (int x = -1; x < 2; x++)
                    {
                        int enemyCheckerX = xChecker + x;
                        int enemyCheckerLeft = yChecker - 1;
                        int enemyCheckerRight = yChecker + 1;

                        int xPlaceToHit = enemyCheckerX + x;
                        int leftPlaceToHit = yChecker - 2;
                        int rightPlaceToHit = yChecker + 2;
                        if (cells[xChecker, yChecker].IsWhiteChecker == user.CheckerColorWhite &&
                            cells[xChecker, yChecker].IsCheckerHere)
                        {
                            if (enemyCheckerX >= 0 && xPlaceToHit >= 0 && enemyCheckerX < 8 && xPlaceToHit < 8 && x != 0)
                            {
                                if (enemyCheckerLeft >= 0 && leftPlaceToHit >= 0)
                                {
                                    if (cells[enemyCheckerX, enemyCheckerLeft].IsCheckerHere &&
                                        cells[enemyCheckerX, enemyCheckerLeft].IsWhiteChecker != user.CheckerColorWhite)
                                    {
                                        if (!cells[xPlaceToHit, leftPlaceToHit].IsCheckerHere)
                                        {
                                            cells[xPlaceToHit, leftPlaceToHit].PosibleMoveCell();
                                            listOfPossibleMovesToBeatAChecker.Add(new int[] { xChecker, yChecker, enemyCheckerX, enemyCheckerLeft, xPlaceToHit, leftPlaceToHit });
                                            posible = true;
                                        }
                                    }
                                }
                                if (enemyCheckerRight < 8 && rightPlaceToHit < 8)
                                {
                                    if (cells[enemyCheckerX, enemyCheckerRight].IsCheckerHere &&
                                        cells[enemyCheckerX, enemyCheckerRight].IsWhiteChecker != user.CheckerColorWhite)
                                    {
                                        if (!cells[xPlaceToHit, rightPlaceToHit].IsCheckerHere)
                                        {
                                            cells[xPlaceToHit, rightPlaceToHit].PosibleMoveCell();
                                            listOfPossibleMovesToBeatAChecker.Add(new int[] { xChecker, yChecker, enemyCheckerX, enemyCheckerRight, xPlaceToHit, rightPlaceToHit });
                                            posible = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return (posible, listOfPossibleMovesToBeatAChecker);
        }

        public bool MoveTheChecker(Cell[,] cells, User user, int xChecker, int yChecker, int xMoveChecker, int yMoveChecker, List<int[]> listOfPossibleMovesToBeatAChecker, bool posible)
        {
            if (cells[xMoveChecker, yMoveChecker].IsCheckerHere == true ||
                cells[xMoveChecker, yMoveChecker] == cells[xChecker, yChecker] ||
                cells[xMoveChecker, yMoveChecker].Point[0, 0].Type == TypeCell.WhiteCell ||
                cells[xMoveChecker, yMoveChecker].Point[0, 0].Type == TypeCell.BlackCell)
            {
                Console.SetCursorPosition(106, 10);
                Console.Write("You cannot place a checker here,");
                Console.SetCursorPosition(106, 12);
                Console.Write("possible moves are highlighted in green.");
                Console.SetCursorPosition(106, 14);
                Console.ReadKey();
                return false;
            }
            else if (posible)
            {
                foreach (var array in listOfPossibleMovesToBeatAChecker)
                {
                    if (array[0] == xChecker && array[1] == yChecker && array[4] == xMoveChecker && array[5] == yMoveChecker)
                    {
                        cells[xChecker, yChecker].DeleteAChecker();
                        cells[xChecker, yChecker].IsCheckerHere = false;
                        cells[array[2], array[3]].DeleteAChecker();
                        cells[array[2], array[3]].IsCheckerHere = false;
                        cells[xMoveChecker, yMoveChecker].DrawAChecker(user);
                        cells[xMoveChecker, yMoveChecker].IsCheckerHere = true;
                        cells[xMoveChecker, yMoveChecker].IsWhiteChecker = user.CheckerColorWhite;
                        Console.Clear();
                        ClearField(cells, user);
                        DrawField(cells);
                        return true;
                    } 
                }
                Console.SetCursorPosition(106, 10);
                Console.Write("You can't move like that,");
                Console.SetCursorPosition(106, 12);
                Console.Write("another checker is responsible for this move.");
                Console.SetCursorPosition(106, 14);
                Console.ReadKey();
                return false;
            }
            else if (cells[xMoveChecker, yMoveChecker].Point[0, 0].Type == TypeCell.PossibleMoveCell)
            {
                cells[xChecker, yChecker].DeleteAChecker();
                cells[xChecker, yChecker].IsCheckerHere = false;
                cells[xMoveChecker, yMoveChecker].DrawAChecker(user);
                cells[xMoveChecker, yMoveChecker].IsCheckerHere = true;
                cells[xMoveChecker, yMoveChecker].IsWhiteChecker = user.CheckerColorWhite;
                Console.Clear();
                ClearField(cells, user);
                DrawField(cells);
                return true;
            }
            return false;
        }
        
        public void ClearField(Cell[,] cells, User user)
        {
            for (int x = 0; x < cells.GetLength(0); x++)
            {
                for (int y = 0; y < cells.GetLength(1); y++)
                {
                    cells[x, y].ClearCell(user);
                }
            }
        }
    }
}

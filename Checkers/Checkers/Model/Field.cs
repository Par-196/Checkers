using Checkers.Model;
using Checkers.Model.Enums;
using System;
using System.ComponentModel;
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
                                user.Checker[checker].X = x;
                                user.Checker[checker].Y = y;
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

        public bool ChooseAChecker(Cell[,] cells, User user, int xChecker, int yChecker)
        {
            if (cells[xChecker, yChecker].IsCheckerHere)
            {
                if (cells[xChecker, yChecker].IsWhiteChecker == user.CheckerColorWhite)
                {
                    if (!IsTheCheckerBlocked(cells, user, xChecker, yChecker))
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
            if (xChecker != 7 || xChecker + 1 < cells.GetLength(0) || xChecker != 0 || xChecker - 1 < cells.GetLength(0))
            {
                if (yChecker - 1 >= 0)
                {
                    if (yChecker + 1 < 8)
                    {
                        if (cells[xChecker, yChecker].IsWhiteChecker && cells[xChecker + 1, yChecker - 1].IsCheckerHere && cells[xChecker + 1, yChecker + 1].IsCheckerHere)
                        {
                            cells[xChecker, yChecker].SelectedChecker(user);
                            cells[xChecker + 1, yChecker + 1].ImpossibleMove(user);
                            cells[xChecker + 1, yChecker - 1].ImpossibleMove(user);
                            return true;
                        }
                        else if (!cells[xChecker, yChecker].IsWhiteChecker && cells[xChecker - 1, yChecker - 1].IsCheckerHere && cells[xChecker - 1, yChecker + 1].IsCheckerHere)
                        {
                            cells[xChecker, yChecker].SelectedChecker(user);
                            cells[xChecker - 1, yChecker + 1].ImpossibleMove(user);
                            cells[xChecker - 1, yChecker - 1].ImpossibleMove(user);
                            return true;
                        }
                    }
                    if (cells[xChecker, yChecker].IsWhiteChecker && cells[xChecker + 1, yChecker - 1].IsCheckerHere)
                    {
                        cells[xChecker, yChecker].SelectedChecker(user);
                        cells[xChecker + 1, yChecker - 1].ImpossibleMove(user);
                        return true;
                    }
                    else if (!cells[xChecker, yChecker].IsWhiteChecker && cells[xChecker - 1, yChecker - 1].IsCheckerHere)
                    {
                        cells[xChecker, yChecker].SelectedChecker(user);
                        cells[xChecker - 1, yChecker - 1].ImpossibleMove(user);
                        return true;
                    }
                }
                else if (yChecker + 1 < 8)
                {
                    if (cells[xChecker, yChecker].IsWhiteChecker && cells[xChecker + 1, yChecker + 1].IsCheckerHere)
                    {
                        cells[xChecker, yChecker].SelectedChecker(user);
                        cells[xChecker + 1, yChecker + 1].ImpossibleMove(user);
                        return true;
                    }
                    else if (!cells[xChecker, yChecker].IsWhiteChecker && cells[xChecker - 1, yChecker + 1].IsCheckerHere)
                    {
                        cells[xChecker, yChecker].SelectedChecker(user);
                        cells[xChecker - 1, yChecker + 1].ImpossibleMove(user);
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
            if (xChecker != 7 || xChecker + 1 < cells.GetLength(0) || xChecker != 0 || xChecker - 1 < cells.GetLength(0))
            {
                if (yChecker - 1 >= 0)
                {
                    if (yChecker + 1 < 8)
                    {
                        if (!cells[xChecker + 1, yChecker - 1].IsCheckerHere && !cells[xChecker + 1, yChecker + 1].IsCheckerHere)
                        {
                            cells[xChecker, yChecker].SelectedChecker(user);
                            cells[xChecker + 1, yChecker + 1].PosibleMoveCell(user);
                            cells[xChecker + 1, yChecker - 1].PosibleMoveCell(user);
                            
                        }
                        else if (!cells[xChecker - 1, yChecker - 1].IsCheckerHere && !cells[xChecker - 1, yChecker + 1].IsCheckerHere)
                        {
                            cells[xChecker, yChecker].SelectedChecker(user);
                            cells[xChecker - 1, yChecker + 1].PosibleMoveCell(user);
                            cells[xChecker - 1, yChecker - 1].PosibleMoveCell(user);
                            
                        }
                    }
                    if (!cells[xChecker + 1, yChecker - 1].IsCheckerHere)
                    {
                        cells[xChecker, yChecker].SelectedChecker(user);
                        cells[xChecker + 1, yChecker - 1].PosibleMoveCell(user);
                        
                    }
                    else if (!cells[xChecker - 1, yChecker - 1].IsCheckerHere)
                    {
                        cells[xChecker, yChecker].SelectedChecker(user);
                        cells[xChecker - 1, yChecker - 1].PosibleMoveCell(user);
                        
                    }
                }
                else if (yChecker + 1 < 8)
                {
                    if (!cells[xChecker + 1, yChecker + 1].IsCheckerHere)
                    {
                        cells[xChecker, yChecker].SelectedChecker(user);
                        cells[xChecker + 1, yChecker + 1].PosibleMoveCell(user);
                        
                    }
                    else if (!cells[xChecker - 1, yChecker + 1].IsCheckerHere)
                    {
                        cells[xChecker, yChecker].SelectedChecker(user);
                        cells[xChecker - 1, yChecker + 1].PosibleMoveCell(user);
                        
                    }
                }
            }
            else
            {
                Console.WriteLine("You have gone out of bounds");
            }
        }

        public void ClearField(Cell[,] cells, User user)
        {
            for (int x = 0; x < cells.GetLength(0); x++)
            {
                for (int y = 0; y < cells.GetLength(1); y++)
                {
                    cells[x, y].ClearCell(cells, user);
                }
            }
        }
    }
}

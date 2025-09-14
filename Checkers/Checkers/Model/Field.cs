using Checkers.Model;
using Checkers.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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
                                cells[x, y].DrawSelectedImposibleSimpeChecker(cells, x, y, TypeCell.BlackCell);
                                cells[x, y].IsCheckerHere = true;
                                if (user.CheckerColorWhite)
                                {
                                    cells[x, y].IsWhiteChecker = true;
                                    cells[x, y].King = true;
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
                                cells[xChecker, yChecker].DrawSelectedImposibleSimpeChecker(cells, xChecker, yChecker, TypeCell.SelectedChecker);
                                userXAndYAreInTheArray = true;
                            }
                        }
                        if (userXAndYAreInTheArray)
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
                        if (cells[xChecker, yChecker].King)
                        {
                            PossibleCheckerMovesForKing(cells, user, xChecker, yChecker);
                            Console.Clear();
                            DrawField(cells);
                            return true;
                        }
                        else
                        {
                            PossibleCheckerMoves(cells, user, xChecker, yChecker);
                            Console.Clear();
                            DrawField(cells);
                            return true;
                        }
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

            cells[xChecker, yChecker].DrawSelectedImposibleSimpeChecker(cells, xChecker, yChecker, TypeCell.SelectedChecker);


            if (cells[xChecker, yChecker].King)
            {
                int countBlockedChecker = 0;
                for (int x = -1 + xChecker; x < xChecker + 2; x++)
                {
                    for (int y = -1 + yChecker; y < yChecker + 2; y++)
                    {
                        if (x >= 0 && x < 8)
                        {
                            if (y >= 0 && y < 8)
                            {
                                if (cells[x, y].IsCheckerHere && x != xChecker && y != yChecker)
                                {
                                    cells[x, y].DrawSelectedImposibleSimpeChecker(cells, x, y, TypeCell.ImpossibleMove);
                                    countBlockedChecker++;
                                }
                                if (countBlockedChecker == 4)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
                return false;
            }

            if (xChecker != 8 || xChecker != 0 || forwardX <= cells.GetLength(0))
            {
                if (left >= 0)
                {
                    if (right < 8)
                    {
                        if (!cells[forwardX, left].IsCheckerHere || !cells[forwardX, right].IsCheckerHere)
                        {
                            if (!cells[forwardX, left].IsCheckerHere)
                            {
                                cells[forwardX, left].DeleteAndDrawaPossibleMoveCell(TypeCell.PossibleMoveCell);
                                cells[forwardX, right].DrawSelectedImposibleSimpeChecker(cells, forwardX, right, TypeCell.ImpossibleMove);
                                return false;
                            }
                            else
                            {
                                cells[forwardX, right].DeleteAndDrawaPossibleMoveCell(TypeCell.PossibleMoveCell);
                                cells[forwardX, left].DrawSelectedImposibleSimpeChecker(cells, forwardX, left, TypeCell.ImpossibleMove);
                                return false;
                            }
                        }
                        else if (cells[forwardX, left].IsCheckerHere && cells[forwardX, right].IsCheckerHere)
                        {
                            cells[forwardX, left].DrawSelectedImposibleSimpeChecker(cells, forwardX, left, TypeCell.ImpossibleMove);
                            cells[forwardX, right].DrawSelectedImposibleSimpeChecker(cells, forwardX, right, TypeCell.ImpossibleMove);
                            return true;
                        }

                    }
                    if (cells[forwardX, left].IsCheckerHere)
                    {
                        cells[forwardX, left].DrawSelectedImposibleSimpeChecker(cells, forwardX, left, TypeCell.ImpossibleMove);
                        return true;
                    }
                }
                else if (right < 8)
                {
                    if (cells[forwardX, right].IsCheckerHere)
                    {
                        cells[forwardX, right].DrawSelectedImposibleSimpeChecker(cells, forwardX, right, TypeCell.ImpossibleMove);
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

        public void PossibleCheckerMovesForKing(Cell[,] cells, User user, int xChecker, int yChecker)
        {

            int rows = cells.GetLength(0);
            int cols = cells.GetLength(1);

            int[,] directions = new int[,]
            {
                { 1,  1 },
                { 1, -1 },
                { -1, 1 },
                { -1, -1 }
            };

            for (int d = 0; d < directions.GetLength(0); d++)
            {
                int dx = directions[d, 0];
                int dy = directions[d, 1];

                int x = xChecker + dx;
                int y = yChecker + dy;

                while (x >= 0 && x < rows && y >= 0 && y < cols)
                {
                    if (cells[x, y].IsCheckerHere)
                        break;

                    cells[x, y].DeleteAndDrawaPossibleMoveCell(TypeCell.PossibleMoveCell);

                    x += dx;
                    y += dy;
                }
            }
        }

        public void PossibleCheckerMoves(Cell[,] cells, User user, int xChecker, int yChecker)
        {

            int xDirection = user.CheckerColorWhite ? 1 : -1;
            int forwardX = xChecker + xDirection;

            int left = yChecker - 1;
            int right = yChecker + 1;

            cells[xChecker, yChecker].DrawSelectedImposibleSimpeChecker(cells, xChecker, yChecker, TypeCell.SelectedChecker);

            if (xChecker != 8 || xChecker != 0 || forwardX <= cells.GetLength(0))
            {
                if (left >= 0)
                {
                    if (right < 8)
                    {
                        if (!cells[forwardX, left].IsCheckerHere && !cells[forwardX, right].IsCheckerHere)
                        {
                            cells[forwardX, right].DeleteAndDrawaPossibleMoveCell(TypeCell.PossibleMoveCell);
                            cells[forwardX, left].DeleteAndDrawaPossibleMoveCell(TypeCell.PossibleMoveCell);
                        }
                    }
                    else if (!cells[forwardX, left].IsCheckerHere)
                    {
                        cells[forwardX, left].DeleteAndDrawaPossibleMoveCell(TypeCell.PossibleMoveCell);
                    }
                }
                else if (right < 8)
                {
                    if (!cells[forwardX, right].IsCheckerHere)
                    {
                        cells[forwardX, right].DeleteAndDrawaPossibleMoveCell(TypeCell.PossibleMoveCell);
                    }
                }
            }
            else
            {
                Console.WriteLine("You have gone out of bounds");
                Console.ReadLine();
            }
        }

        public (bool, List<int[]>) IsItPossibleToBeatACheckers(Cell[,] cells, User user, List<int[]> listOfPossibleMovesToBeatAChecker, bool possible)
        {
            for (int xChecker = 0; xChecker < cells.GetLength(0); xChecker++)
            {
                for (int yChecker = 0; yChecker < cells.GetLength(1); yChecker++)
                {
                    if (cells[xChecker, yChecker].IsCheckerHere && cells[xChecker, yChecker].IsWhiteChecker == user.CheckerColorWhite)
                    {
                        if (cells[xChecker, yChecker].King)
                        {
                            bool topLeft = false;
                            bool topRight = false;
                            bool botLeft = false;
                            bool botRight = false;

                            int topEnemyCheckerX = xChecker - 1;
                            int botEnemyCheckerX = xChecker + 1;

                            int topXPlaceToHit = xChecker - 2;
                            int botXPlaceToHit = xChecker + 2;

                            int enemyCheckerLeft = yChecker - 1;
                            int enemyCheckerRight = yChecker + 1;

                            int leftPlaceToHit = yChecker - 2;
                            int rightPlaceToHit = yChecker + 2;

                            while (!(topLeft && topRight && botLeft && botRight))
                            {
                                if (topEnemyCheckerX >= 0 && topXPlaceToHit >= 0 && topEnemyCheckerX < 8 && topXPlaceToHit < 8)
                                {
                                    if (enemyCheckerLeft >= 0 && leftPlaceToHit >= 0 && enemyCheckerLeft < 8 && leftPlaceToHit < 8 && !topLeft)
                                    {
                                        if (cells[topEnemyCheckerX, enemyCheckerLeft].IsCheckerHere &&
                                            cells[topEnemyCheckerX, enemyCheckerLeft].IsWhiteChecker != cells[xChecker, yChecker].IsWhiteChecker)
                                        {
                                            bool drawGreenCell = true;
                                            while (drawGreenCell)
                                            {
                                                if (topXPlaceToHit >= 0 && leftPlaceToHit >= 0 && !cells[topXPlaceToHit, leftPlaceToHit].IsCheckerHere)
                                                {
                                                    cells[topXPlaceToHit, leftPlaceToHit].DeleteAndDrawaPossibleMoveCell(TypeCell.PossibleMoveCell);
                                                    listOfPossibleMovesToBeatAChecker.Add(new int[] { xChecker, yChecker, topEnemyCheckerX, enemyCheckerLeft, topXPlaceToHit, leftPlaceToHit });
                                                    possible = true;
                                                    topXPlaceToHit--;
                                                    leftPlaceToHit--;
                                                }
                                                else
                                                {
                                                    drawGreenCell = false;
                                                    topLeft = true;
                                                }
                                            }
                                            
                                        }
                                        else 
                                        {
                                            topLeft = true;
                                        }
                                    }
                                    else
                                    {
                                        topLeft = true;
                                    }
                                    if (enemyCheckerRight >= 0 && rightPlaceToHit >= 0 && enemyCheckerRight < 8 && rightPlaceToHit < 8 && !topRight)
                                    {
                                        if (cells[topEnemyCheckerX, enemyCheckerRight].IsCheckerHere &&
                                                cells[topEnemyCheckerX, enemyCheckerRight].IsWhiteChecker != cells[xChecker, yChecker].IsWhiteChecker)
                                        {
                                            bool drawGreenCell = true;
                                            while (drawGreenCell)
                                            {
                                                if (topXPlaceToHit >= 0 && rightPlaceToHit < 8 && !cells[topXPlaceToHit, rightPlaceToHit].IsCheckerHere)
                                                {
                                                    cells[topXPlaceToHit, rightPlaceToHit].DeleteAndDrawaPossibleMoveCell(TypeCell.PossibleMoveCell);
                                                    listOfPossibleMovesToBeatAChecker.Add(new int[] { xChecker, yChecker, topEnemyCheckerX, enemyCheckerRight, topXPlaceToHit, rightPlaceToHit });
                                                    possible = true;
                                                    topXPlaceToHit--;
                                                    rightPlaceToHit++;
                                                }
                                                else 
                                                {
                                                    drawGreenCell = false;
                                                    topRight = true;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            topRight = true;
                                        }
                                    }
                                    else
                                    {
                                        topRight = true;
                                    }
                                }
                                else
                                {
                                    topLeft = true;
                                    topRight = true;
                                }
                                if (botEnemyCheckerX >= 0 && botXPlaceToHit >= 0 && botEnemyCheckerX < 8 && botXPlaceToHit < 8)
                                {
                                    if (enemyCheckerLeft >= 0 && leftPlaceToHit >= 0 && enemyCheckerLeft < 8 && leftPlaceToHit < 8 && !botLeft)
                                    {
                                        if (cells[botEnemyCheckerX, enemyCheckerLeft].IsCheckerHere &&
                                                cells[botEnemyCheckerX, enemyCheckerLeft].IsWhiteChecker != cells[xChecker, yChecker].IsWhiteChecker)
                                        {
                                            bool drawGreenCell = true;
                                            while (drawGreenCell)
                                            {
                                                if (botXPlaceToHit < 8 && leftPlaceToHit >= 0 && !cells[botXPlaceToHit, leftPlaceToHit].IsCheckerHere)
                                                {
                                                    cells[botXPlaceToHit, leftPlaceToHit].DeleteAndDrawaPossibleMoveCell(TypeCell.PossibleMoveCell);
                                                    listOfPossibleMovesToBeatAChecker.Add(new int[] { xChecker, yChecker, botEnemyCheckerX, enemyCheckerLeft, botXPlaceToHit, leftPlaceToHit });
                                                    possible = true;
                                                    botXPlaceToHit++;
                                                    leftPlaceToHit--;
                                                }
                                                else
                                                {
                                                    drawGreenCell = false;
                                                    botLeft = true;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            botLeft = true;
                                        }
                                    }
                                    else
                                    {
                                        botLeft = true;
                                    }
                                    if (enemyCheckerRight >= 0 && rightPlaceToHit >= 0 && enemyCheckerRight < 8 && rightPlaceToHit < 8 && !botRight)
                                    {
                                        if (cells[botEnemyCheckerX, enemyCheckerRight].IsCheckerHere &&
                                                cells[botEnemyCheckerX, enemyCheckerRight].IsWhiteChecker != cells[xChecker, yChecker].IsWhiteChecker)
                                        {
                                            bool drawGreenCell = true;
                                            while (drawGreenCell)
                                            {
                                                if (botXPlaceToHit < 8 && rightPlaceToHit < 8 && !cells[botXPlaceToHit, rightPlaceToHit].IsCheckerHere)
                                                {
                                                    cells[botXPlaceToHit, rightPlaceToHit].DeleteAndDrawaPossibleMoveCell(TypeCell.PossibleMoveCell);
                                                    listOfPossibleMovesToBeatAChecker.Add(new int[] { xChecker, yChecker, botEnemyCheckerX, enemyCheckerRight, botXPlaceToHit, rightPlaceToHit });
                                                    possible = true;
                                                    botXPlaceToHit++;
                                                    rightPlaceToHit++;
                                                }
                                                else
                                                {
                                                    drawGreenCell = false;
                                                    botRight = true;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            botRight = true;
                                        }
                                    }
                                    else
                                    {
                                        botRight = true;
                                    }
                                }
                                else
                                {
                                    botLeft = true;
                                    botRight = true;
                                }
                                topEnemyCheckerX--;
                                topXPlaceToHit--;
                                botEnemyCheckerX++;
                                botXPlaceToHit++;

                                enemyCheckerLeft -= 1;
                                leftPlaceToHit -= 1;
                                enemyCheckerRight += 1;
                                rightPlaceToHit += 1;
                            }
                        }
                        else if (!cells[xChecker, yChecker].King)
                        {
                            for (int x = -1; x < 2; x++)
                            {
                                int enemyCheckerX = xChecker + x;
                                int leftEnemyChecker = yChecker - 1;
                                int rightEnemyChecker = yChecker + 1;

                                int xPlaceToHit = enemyCheckerX + x;
                                int leftPlaceToHit = yChecker - 2;
                                int rightPlaceToHit = yChecker + 2;

                                if (cells[xChecker, yChecker].IsWhiteChecker == user.CheckerColorWhite &&
                                    cells[xChecker, yChecker].IsCheckerHere)
                                {
                                    if (enemyCheckerX >= 0 && xPlaceToHit >= 0 && enemyCheckerX < 8 && xPlaceToHit < 8 && x != 0)
                                    {
                                        if (leftEnemyChecker >= 0 && leftPlaceToHit >= 0)
                                        {
                                            if (cells[enemyCheckerX, leftEnemyChecker].IsCheckerHere &&
                                                cells[enemyCheckerX, leftEnemyChecker].IsWhiteChecker != cells[xChecker, yChecker].IsWhiteChecker)
                                            {
                                                if (!cells[xPlaceToHit, leftPlaceToHit].IsCheckerHere)
                                                {
                                                    cells[xPlaceToHit, leftPlaceToHit].DeleteAndDrawaPossibleMoveCell(TypeCell.PossibleMoveCell);
                                                    listOfPossibleMovesToBeatAChecker.Add(new int[] { xChecker, yChecker, enemyCheckerX, leftEnemyChecker, xPlaceToHit, leftPlaceToHit });
                                                    possible = true;
                                                }
                                            }
                                        }
                                        if (rightEnemyChecker < 8 && rightPlaceToHit < 8)
                                        {
                                            if (cells[enemyCheckerX, rightEnemyChecker].IsCheckerHere &&
                                                cells[enemyCheckerX, rightEnemyChecker].IsWhiteChecker != cells[xChecker, yChecker].IsWhiteChecker)
                                            {
                                                if (!cells[xPlaceToHit, rightPlaceToHit].IsCheckerHere)
                                                {
                                                    cells[xPlaceToHit, rightPlaceToHit].DeleteAndDrawaPossibleMoveCell(TypeCell.PossibleMoveCell);
                                                    listOfPossibleMovesToBeatAChecker.Add(new int[] { xChecker, yChecker, enemyCheckerX, rightEnemyChecker, xPlaceToHit, rightPlaceToHit });
                                                    possible = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }


                }
            }
            return (possible, listOfPossibleMovesToBeatAChecker);
        }



        public (bool, bool) MoveTheChecker(Cell[,] cells, User user, int xChecker, int yChecker, int xMoveChecker, int yMoveChecker, List<int[]> listOfPossibleMovesToBeatAChecker, bool posible)
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
                return (posible, false);
            }
            else if (posible)
            {
                foreach (var array in listOfPossibleMovesToBeatAChecker)
                {
                    if (array[0] == xChecker && array[1] == yChecker && array[4] == xMoveChecker && array[5] == yMoveChecker)
                    {
                        cells[xMoveChecker, yMoveChecker].DrawSelectedImposibleSimpeChecker(cells, xMoveChecker, yMoveChecker, TypeCell.BlackCell);
                        cells[xMoveChecker, yMoveChecker].IsCheckerHere = true;
                        cells[xMoveChecker, yMoveChecker].King = cells[xChecker, yChecker].King;
                        cells[xMoveChecker, yMoveChecker].IsWhiteChecker = user.CheckerColorWhite;
                        cells[xChecker, yChecker].DeleteAndDrawaPossibleMoveCell(TypeCell.BlackCell);
                        cells[xChecker, yChecker].IsCheckerHere = false;
                        cells[xChecker, yChecker].King = false;
                        cells[xChecker, yChecker].IsWhiteChecker = false;
                        cells[array[2], array[3]].DeleteAndDrawaPossibleMoveCell(TypeCell.BlackCell);
                        cells[array[2], array[3]].IsCheckerHere = false;
                        cells[array[2], array[3]].King = false;
                        cells[xChecker, yChecker].IsWhiteChecker = false;
                        posible = false;
                        (posible, listOfPossibleMovesToBeatAChecker) = IsItPossibleToBeatACheckers(cells, user, listOfPossibleMovesToBeatAChecker, posible);
                        if (!posible)
                        {
                            if ((user.CheckerColorWhite && xMoveChecker == 7 ) || (!user.CheckerColorWhite && xMoveChecker == 0) 
                                && !cells[xMoveChecker, yMoveChecker].IsCheckerHere)
                            {
                                cells[xMoveChecker, yMoveChecker].King = true;
                            }
                            cells[xMoveChecker, yMoveChecker].DrawSelectedImposibleSimpeChecker(cells, xMoveChecker, yMoveChecker, TypeCell.BlackCell);
                        }
                        ClearField(cells, user);
                        Console.Clear();
                        DrawField(cells);
                        return (posible, true);
                    }
                }
                Console.SetCursorPosition(106, 10);
                Console.Write("You can't move like that,");
                Console.SetCursorPosition(106, 12);
                Console.Write("another checker is responsible for this move.");
                Console.SetCursorPosition(106, 14);
                Console.ReadKey();
                return (posible, false);
            }
            else if (cells[xMoveChecker, yMoveChecker].Point[0, 0].Type == TypeCell.PossibleMoveCell)
            {

                cells[xMoveChecker, yMoveChecker].DrawSelectedImposibleSimpeChecker(cells, xMoveChecker, yMoveChecker, TypeCell.BlackCell);
                cells[xMoveChecker, yMoveChecker].IsCheckerHere = true;
                cells[xMoveChecker, yMoveChecker].King = cells[xChecker, yChecker].King;
                cells[xMoveChecker, yMoveChecker].IsWhiteChecker = user.CheckerColorWhite;
                cells[xChecker, yChecker].DeleteAndDrawaPossibleMoveCell(TypeCell.BlackCell);
                cells[xChecker, yChecker].IsCheckerHere = false;
                cells[xChecker, yChecker].King = false;
                cells[xChecker, yChecker].IsWhiteChecker = false;
                if (!posible)
                {
                    if ((user.CheckerColorWhite && xMoveChecker == 7) || (!user.CheckerColorWhite && xMoveChecker == 0)
                                && !cells[xMoveChecker, yMoveChecker].IsCheckerHere)
                    {
                        cells[xMoveChecker, yMoveChecker].King = true;
                    }
                    cells[xMoveChecker, yMoveChecker].DrawSelectedImposibleSimpeChecker(cells, xMoveChecker, yMoveChecker, TypeCell.BlackCell);
                }
                Console.Clear();
                ClearField(cells, user);
                DrawField(cells);
                return (posible, true);
            }
            return (posible, false);
        }
        
        public void ClearField(Cell[,] cells, User user)
        {
            for (int x = 0; x < cells.GetLength(0); x++)
            {
                for (int y = 0; y < cells.GetLength(1); y++)
                {
                    cells[x, y].ClearCell(cells, x, y);
                }
            }
        }
    }
}

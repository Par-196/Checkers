using Checkers.Model;
using Checkers.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Model
{
    public class Cell
    {
        public Point[,] Point { get; set; }

        public bool IsCheckerHere { get; set; }

        public bool IsWhiteChecker { get; set; }

        public bool King { get; set; }

        public Cell(TypeCell type)
        {
            Point = new Point[5, 11];

            for (int x = 0; x < Point.GetLength(0); x++)
            {
                for (int y = 0; y < Point.GetLength(1); y++)
                {
                    Point[x, y] = new Point(x, y, type);
                }
            }
        }

        public void DrawSelectedImposibleSimpeChecker(Cell[,] cells, int xChecker, int yCkecker, TypeCell type)
        {
            for (int x = 0; x < Point.GetLength(0); x++)
            {
                for (int y = 0; y < Point.GetLength(1); y++)
                {
                    if (cells[xChecker, yCkecker].King)
                    {
                        if (x == 1 && y == 5 ||
                            x == 2 && y == 4 ||
                            x == 2 && y == 6 ||
                            x == 3 && y == 5)
                        {
                            if (cells[xChecker, yCkecker].IsWhiteChecker)
                            {
                                Point[x, y].Type = TypeCell.WhiteCheckersCell;
                            }
                            else
                            {
                                Point[x, y].Type = TypeCell.GrayCheckersCell;
                            }
                        }
                        else
                        {
                            Point[x, y].Type = type;
                        }
                    }
                    else
                    {
                        if (x == Point.GetLength(0) / 2 && y == Point.GetLength(1) / 2)
                        {
                            if (cells[xChecker, yCkecker].IsWhiteChecker)
                            {
                                Point[x, y].Type = TypeCell.WhiteCheckersCell;
                            }
                            else
                            {
                                Point[x, y].Type = TypeCell.GrayCheckersCell;
                            }
                        }
                        else
                        {
                            Point[x, y].Type = type;
                        }
                    }
                }
            }
        }

        public void DeleteAndDrawaPossibleMoveCell(TypeCell type)
        {
            for (int x = 0; x < Point.GetLength(0); x++)
            {
                for (int y = 0; y < Point.GetLength(1); y++)
                {
                    Point[x, y].Type = type;
                }
            }
        }

        public void ClearCell(Cell[,] cells, int xChecker ,int yCkecker)
        {
            for (int x = 0; x < Point.GetLength(0); x++)
            {
                for (int y = 0; y < Point.GetLength(1); y++)
                {
                    if (cells[xChecker, yCkecker].IsCheckerHere)
                    {
                        if (cells[xChecker, yCkecker].King)
                        {
                            if (x == 1 && y == 5 ||
                                x == 2 && y == 4 ||
                                x == 2 && y == 6 ||
                                x == 3 && y == 5)
                            {
                                if (cells[xChecker, yCkecker].IsWhiteChecker)
                                {
                                    Point[x, y].Type = TypeCell.WhiteCheckersCell;
                                } 
                                else
                                {
                                    Point[x, y].Type = TypeCell.GrayCheckersCell;
                                }
                            }
                            else
                            {
                                Point[x, y].Type = TypeCell.BlackCell;
                            }
                        }
                        else
                        {
                            if (x == Point.GetLength(0) / 2 && y == Point.GetLength(1) / 2)
                            {
                                if (cells[xChecker, yCkecker].IsWhiteChecker)
                                {
                                    Point[x, y].Type = TypeCell.WhiteCheckersCell;
                                }
                                else
                                {
                                    Point[x, y].Type = TypeCell.GrayCheckersCell;
                                }
                            }
                            else
                            {
                                Point[x, y].Type = TypeCell.BlackCell;
                            }
                        }
                    }
                    else if (cells[xChecker, yCkecker].Point[x, y].Type == TypeCell.ImpossibleMove ||
                        cells[xChecker, yCkecker].Point[x, y].Type == TypeCell.PossibleMoveCell ||
                        cells[xChecker, yCkecker].Point[x, y].Type == TypeCell.SelectedChecker) 
                    {
                        Point[x, y].Type = TypeCell.BlackCell;
                    }
                }
            }
        }
    }
}

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

        public void SelectedChecker(User user)
        {
            for (int x = 0; x < Point.GetLength(0); x++)
            {
                for (int y = 0; y < Point.GetLength(1); y++)
                {
                    if (x == Point.GetLength(0) / 2 && y == Point.GetLength(1) / 2)
                    {
                        if (user.CheckerColorWhite)
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
                        Point[x, y].Type = TypeCell.SelectedChecker;
                    }
                }
            }
        }

        public void PosibleMoveCell(User user)
        {
            for (int x = 0; x < Point.GetLength(0); x++)
            {
                for (int y = 0; y < Point.GetLength(1); y++)
                {
                    Point[x, y].Type = TypeCell.PossibleMoveCell;
                }
            }
        }

        public void ImpossibleMove(User user)
        {
            for (int x = 0; x < Point.GetLength(0); x++)
            {
                for (int y = 0; y < Point.GetLength(1); y++)
                {
                    if (x == Point.GetLength(0) / 2 && y == Point.GetLength(1) / 2)
                    {
                        if (user.CheckerColorWhite)
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
                        Point[x, y].Type = TypeCell.ImpossibleMove;
                    }
                }
            }
        }

        public void ClearCell(Cell[,] cells, User user)
        {
            for (int x = 0; x < Point.GetLength(0); x++)
            {
                for (int y = 0; y < Point.GetLength(1); y++)
                {
                    if (Point[x, y].Type == TypeCell.SelectedChecker || Point[x, y].Type == TypeCell.ImpossibleMove)
                    {
                        if (x == Point.GetLength(0) / 2 && y == Point.GetLength(1) / 2)
                        {
                            if (user.CheckerColorWhite)
                            {
                                Point[x, y].Type = TypeCell.WhiteCheckersCell;
                            }
                            else
                            {
                                Point[x, y].Type = TypeCell.GrayCheckersCell;
                            }
                        }
                        Point[x, y].Type = TypeCell.BlackCell;
                    }
                    else if (Point[x, y].Type == TypeCell.PossibleMoveCell)
                    {
                        Point[x, y].Type = TypeCell.BlackCell;
                    }
                }
            }
        }
    }
}

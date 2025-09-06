using Checkers.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void SelectedChecker()
        {
            for (int x = 0; x < Point.GetLength(0); x++)
            {
                for (int y = 0; y < Point.GetLength(1); y++)
                {
                    Point[x, y].Type = TypeCell.SelectedChecker;
                }
            }
        }

        public void PosibleMoveCell()
        {
            for (int x = 0; x < Point.GetLength(0); x++)
            {
                for (int y = 0; y < Point.GetLength(1); y++)
                {
                    Point[x, y].Type = TypeCell.PossibleMoveCell;
                }
            }
        }

        public void ImpossibleMove()
        {
            for (int x = 0; x < Point.GetLength(0); x++)
            {
                for (int y = 0; y < Point.GetLength(1); y++)
                {
                    Point[x, y].Type = TypeCell.ImpossibleMove;
                }
            }
        }
    }
}

    

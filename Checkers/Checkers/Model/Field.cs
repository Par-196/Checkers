using Checkers.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Model
{
    public class Field
    {
        public Cell[,] Cells { get; set; }

        public Field()
        {
            
            Cell[,] cells = new Cell[24, 40];
            int countCells = 0;
            int countRow = 0;
            bool isBlack = false;

            for (int x = 0; x < 24; x++)
            {
                for (int y = 0; y < 40; y++)
                {
                    if ((countCells >= 5 && countCells <= 9) || (countCells >= 15 && countCells <= 19) ||
                        (countCells >= 25 && countCells <= 29) || (countCells >= 35 && countCells <= 39))
                    {
                        isBlack = true;
                    }
                    else
                    {
                        isBlack = false;
                    }
                    countCells = WhiteAndBlackCellOutput(isBlack, cells, x, y, countCells);
                }
                if ((x >= 2 && x <= 4) || (x >= 8 && x <= 10) || (x >= 14 && x <= 16) || (x >= 20 && x <= 22))
                {
                    isBlack = !isBlack;
                    countCells = 5;
                }
                else
                {
                    countCells = 0;
                }

                Console.WriteLine();
            }
        }

        public int WhiteAndBlackCellOutput(bool isBlack, Cell[,] cells, int x, int y, int countCells)
        {
            if (isBlack == false)
            {
                Console.Write(cells[x, y] = new Cell(new Point(x, y), TypeCell.WhiteCell));
                Console.ResetColor();
                countCells++;
                return countCells;
            }
            else if (isBlack == true)
            {
                Console.Write(cells[x, y] = new Cell(new Point(x, y), TypeCell.BlackCell));
                Console.ResetColor();
                countCells++;
                return countCells;
            }
            return 0;
        }
    }
}

using Checkers.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Model
{
    public class Field
    {
        public Cell[,] Cells { get; set; }
        public Player Player { get; set; }

        public Field(Player player1, Player player2)
        {

            Cell[,] cells = new Cell[40, 88];
            int countCells = 0;
            int countRows = 0;
            bool isBlack = true;
            int numb = 8;
            Console.WriteLine("\n           A          B          C          D          E          F          G          H\n");

            for (int x = 0; x < 95; x++)
            {
                if (x > 4)
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

            for (int x = 0; x < 40; x++)
            {
                countRows++;
                if (x == 2 || x == 7 || x == 12 || x == 17 || x == 22 || x == 27 || x == 32 || x == 37)
                {
                    Console.Write($"  {numb--}  ");
                }
                else
                {
                    Console.Write("     ");
                }
                Console.BackgroundColor = ConsoleColor.DarkCyan;
                Console.Write(" ");
                Console.ResetColor();

                for (int y = 0; y < 88; y++)
                {
                    if (countCells % 11 == 0)
                    {
                        isBlack = !isBlack;
                    }
                    countCells = WhiteAndBlackCellOutput(isBlack, cells, x, y, countCells);
                }
                if (countRows % 5 == 0)
                {
                    isBlack = !isBlack;
                    countCells = 11;
                }
                else
                {
                    countCells = 0;
                }
                Console.BackgroundColor = ConsoleColor.DarkCyan;
                Console.Write(" ");
                Console.ResetColor();

                Console.WriteLine();
            }

            for (int x = 0; x < 95; x++)
            {
                if (x > 4)
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
            Console.WriteLine();
            
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

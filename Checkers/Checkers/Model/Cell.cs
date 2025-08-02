using Checkers.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Model
{
    public class Cell
    {
        public Point Point { get; set; }
        public TypeCell Type { get; set; }

        public Cell(Point point, TypeCell type)
        {
            Point = point;
            Type = type;    
        }

        
        public override string ToString()
        {
            switch (Type)
            {
                case TypeCell.BlackCell:
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        return " ";
                    }
                case TypeCell.WhiteCell:
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        return " ";
                    }
                case TypeCell.RedCell:
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        return " ";
                    }
                case TypeCell.GreenCell:
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        return " ";
                    }
                case TypeCell.PossibleMoveCell:
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        return " ";
                    }
                case TypeCell.GrayCheckersCell:
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        return " ";
                    }
                case TypeCell.WhiteCheckersCell:
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        return " ";
                    }
            }
            return "Error";
        }
    }
}

    

using Checkers.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Model
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public TypeCell Type { get; set; }

        public Point(int x, int y, TypeCell type)
        {
            X = x;
            Y = y;
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
                case TypeCell.ImpossibleMove:
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        return " ";
                    }
                case TypeCell.PossibleMoveCell:
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        return " ";
                    }
                case TypeCell.SelectedChecker:
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        return " "; 
                    }
                case TypeCell.GrayCheckersCell:
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
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

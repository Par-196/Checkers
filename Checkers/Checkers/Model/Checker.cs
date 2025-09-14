using Checkers.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Model
{
    public class Checker
    {
        public Point[,] Points { get; set; }

        public bool White { get; set; }

        public Checker(bool white) 
        {
            Points = new Point[5, 11];

            White = white;

            CreateChecker();
        }

        public void CreateChecker()
        {
            for (int x = 0; x < Points.GetLength(0); x++)
            {
                for (int y = 0; y < Points.GetLength(1); y++)
                {
                    
                    if (x == Points.GetLength(0) / 2 && y == Points.GetLength(1) / 2)
                    {
                        if (White)
                        {
                            Points[x, y] = new Point(x, y, TypeCell.WhiteCheckersCell);
                        }
                        else
                        {
                            Points[x, y] = new Point(x, y, TypeCell.GrayCheckersCell);
                        }
                    }
                    else
                    {
                        Points[x, y] = new Point(x, y, TypeCell.BlackCell);
                    }
                }
            }
        }
    }
}

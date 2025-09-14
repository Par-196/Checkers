using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Model
{
    public class Player : User 
    {
        public Player ( string name, bool checkerColorWhite) 
            : base(name, checkerColorWhite)
        {
            Name = name;

            CheckerColorWhite = checkerColorWhite;

            Checker = new Checker[12];

            CreateCheckersArrray();
        }

        public override void CreateCheckersArrray()
        {
            for (int i = 0; i < Checker.Length; i++)
            {
                if (CheckerColorWhite)
                {
                    Checker[i] = new Checker(true);
                }
                else
                {
                    Checker[i] = new Checker(false);
                }
            }
        }

    }
}

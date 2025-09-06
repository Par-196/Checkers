using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Model
{
    public class Bot : User
    {
        public Bot(string name, bool checkerColorWhite)
            : base(name, checkerColorWhite)
        {
            Name = name;

            CheckerColorWhite = checkerColorWhite;

            CreateCheckersArrray();
        }

        public override void CreateCheckersArrray()
        {
            Checker = new Checker[12];

            for (int i = 0; i < Checker.Length; i++)
            {
                if (CheckerColorWhite)
                {
                    Checker[i] = new Checker(true, false);
                }
                else
                {
                    Checker[i] = new Checker(false, false);
                }
            }
        }

    }
}

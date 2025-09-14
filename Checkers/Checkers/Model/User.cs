using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Model
{
    public abstract class User
    {
        public string Name { get; set; }
        public bool CheckerColorWhite { get; set; }
        public Checker[] Checker { get; set; }
        
        public User(string name, bool checkerColorWhite)
        {
            Name = name;
            CheckerColorWhite = checkerColorWhite;
        }

        public abstract void CreateCheckersArrray();
    }
}

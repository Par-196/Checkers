using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Model
{
    public class Game
    {
        public Field Field { get; set; }

        public Game()
        {
            Field = new Field();
        }

        public void StartGame()
        {
        }
    }
}

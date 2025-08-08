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

        public Player player { get; set; }

        public Game()
        {
        }

        public void StartGame(Player firstPlayer, Player secondPlayer)
        {
            Field field = new Field(firstPlayer, secondPlayer);
        }
    }
}

using Checkers.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(200, 60);
            Game game = new Game();

            Player firstPlayer = new Player();
            Player secondPlayer = new Player();

            

            game.StartGame(firstPlayer, secondPlayer);
            Console.ReadLine();
        }
    }
}

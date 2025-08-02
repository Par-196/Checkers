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
            Game game = new Game();
            game.StartGame();
            Console.ReadLine();
        }
    }
}

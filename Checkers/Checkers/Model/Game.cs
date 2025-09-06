using Checkers.Model.Enums;
using System;
using System.ComponentModel;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;
using System.Security.Policy;

namespace Checkers.Model
{
    public class Game
    {
        public Field Field { get; set; }

        public Game()
        {
        }

        public void StartGame(Player firstPlayer, Player secondPlayer)
        {
            
            Cell[,] cells = new Cell[8, 8];
            bool gameEnd = false;
            Field = new Field(cells, firstPlayer, secondPlayer);
            Field.DrawField(cells);
            bool firstPlayerMove = true;
            bool drawOffered = false;
            bool doStep = false;
            while (!gameEnd)
            {
                if (!firstPlayer.CheckerColorWhite)
                {
                    firstPlayerMove = false;
                }
                switch (firstPlayerMove)
                {
                    case true:
                        {
                            if (drawOffered)
                            {
                                drawOffered = AcceptOrRejectOfferADraw(firstPlayer);
                                gameEnd = true ? drawOffered == true : gameEnd;
                                continue;
                            }
                            do
                            {
                                OutputMenuInGame(firstPlayer);
                                Enum.TryParse(Console.ReadLine(), out GameMenu gameMenu);
                                switch (gameMenu)
                                {
                                    case GameMenu.DoStep:
                                        {
                                            int xChecker = int.MinValue;
                                            int yChecker = int.MinValue;
                                            (xChecker, yChecker) = ChooseYourChecker(cells, firstPlayer, xChecker, yChecker);
                                            
                                            // Дочекатись ходу гравця
                                            // Перемістити обрану шашку



                                            //Виберіть шашку
                                            // Виберіть куди її поставити
                                            doStep = true;
                                        }
                                        break;
                                    case GameMenu.Surrender:
                                        {
                                            gameEnd = Surrender(firstPlayer, secondPlayer);
                                        }
                                        break;
                                    case GameMenu.OfferADraw:
                                        {
                                            if (!drawOffered)
                                            {
                                                drawOffered = OfferADraw(secondPlayer);
                                            }
                                            else
                                            {
                                                TheDrawOfferHasAlreadyBeenSent();
                                            }
                                        }
                                        break;
                                }
                                ClearMenuInGame();
                            }
                            while (!doStep && !gameEnd);
                            doStep = false;
                            firstPlayerMove = !firstPlayerMove;
                        }
                        break;
                    case false:
                        {
                            if (drawOffered)
                            {
                                drawOffered = AcceptOrRejectOfferADraw(secondPlayer);
                                gameEnd = true ? drawOffered == true : gameEnd;
                                continue;
                            }
                            do
                            {
                                OutputMenuInGame(secondPlayer);
                                Enum.TryParse(Console.ReadLine(), out GameMenu gameMenu);
                                switch (gameMenu)
                                {
                                    case GameMenu.DoStep:
                                        {
                                            int xChecker = int.MinValue;
                                            int yChecker = int.MinValue;
                                            (xChecker, yChecker) = ChooseYourChecker(cells, secondPlayer, xChecker, yChecker);
                                            doStep = true;
                                        }
                                        break;
                                    case GameMenu.Surrender:
                                        {
                                            gameEnd = Surrender(secondPlayer, firstPlayer);
                                        }
                                        break;
                                    case GameMenu.OfferADraw:
                                        {
                                            if (!drawOffered)
                                            {
                                                drawOffered = OfferADraw(firstPlayer);
                                            }
                                            else
                                            {
                                                TheDrawOfferHasAlreadyBeenSent();
                                            }
                                        }
                                        break;
                                }
                                ClearMenuInGame();
                            }
                            while (!doStep && !gameEnd);
                            doStep = false;
                            firstPlayerMove = !firstPlayerMove;
                        }
                        break;
                }
            }
        }
        public void StartGameWithBot(Player firstPlayer, Bot bot)
        {
            Cell[,] cells = new Cell[8, 8];
            bool gameEnd = false;
            Field = new Field(cells, firstPlayer, bot);
            bool firstPlayerMove = true;
            bool drawOffered = false;
            bool doStep = false;
            while (!gameEnd)
            {
                if (!firstPlayer.CheckerColorWhite)
                {
                    firstPlayerMove = false;
                }
                switch (firstPlayerMove)
                {
                    case true:
                        {
                            if (drawOffered)
                            {
                                drawOffered = AcceptOrRejectOfferADraw(firstPlayer);
                                gameEnd = true ? drawOffered == true : gameEnd;
                                continue;
                            }
                            do
                            {
                                OutputMenuInGame(firstPlayer);
                                Enum.TryParse(Console.ReadLine(), out GameMenu gameMenu);
                                switch (gameMenu)
                                {
                                    case GameMenu.DoStep:
                                        {
                                            
                                            doStep = true;
                                        }
                                        break;
                                    case GameMenu.Surrender:
                                        {
                                            gameEnd = Surrender(firstPlayer, bot);
                                        }
                                        break;
                                    case GameMenu.OfferADraw:
                                        {
                                            if (!drawOffered)
                                            {
                                                drawOffered = OfferADraw(bot);
                                            }
                                            else
                                            {
                                                TheDrawOfferHasAlreadyBeenSent();
                                            }
                                        }
                                        break;
                                }
                                ClearMenuInGame();
                            }
                            while (!doStep && !gameEnd);
                            doStep = false;
                            firstPlayerMove = !firstPlayerMove;
                        }
                        break;
                    case false:
                        {
                            if (drawOffered)
                            {
                                drawOffered = AcceptOrRejectOfferADraw(bot);
                                gameEnd = true ? drawOffered == true : gameEnd;
                                continue;
                            }
                            do
                            {
                                OutputMenuInGame(bot);
                                Enum.TryParse(Console.ReadLine(), out GameMenu gameMenu);
                                switch (gameMenu)
                                {
                                    case GameMenu.DoStep:
                                        {

                                        }
                                        break;
                                    case GameMenu.Surrender:
                                        {
                                            gameEnd = Surrender(bot, firstPlayer);
                                        }
                                        break;
                                    case GameMenu.OfferADraw:
                                        {
                                            if (!drawOffered)
                                            {
                                                drawOffered = OfferADraw(firstPlayer);
                                            }
                                            else
                                            {
                                                TheDrawOfferHasAlreadyBeenSent();
                                            }
                                        }
                                        break;
                                }
                                ClearMenuInGame();
                            }
                            while (!doStep && !gameEnd);
                            doStep = false;
                            firstPlayerMove = !firstPlayerMove;
                        }
                        break;

                }
            }
        }

        //---------------------------------------------------------------------------------

        public void OutputMenuInGame(User user)
        {
            Console.SetCursorPosition(106, 4);
            Console.Write($"Player's move - {user.Name}");
            Console.SetCursorPosition(106, 6);
            Console.Write($" --- MENU IN GAME --- ");
            Console.SetCursorPosition(106, 7);
            Console.Write($"1. Do Step");
            Console.SetCursorPosition(106, 8);
            Console.Write($"2. Surrender");
            Console.SetCursorPosition(106, 9);
            Console.Write($"3. Offer a Draw");
            Console.SetCursorPosition(106, 11);
        }

        public bool Surrender(User firstPlayer, User secondPlayer)
        {
            Console.SetCursorPosition(106, 11);
            Console.WriteLine($"The {firstPlayer.Name} surrendered.");
            Console.SetCursorPosition(106, 13);
            Console.WriteLine($"Win - {secondPlayer.Name}");
            Console.SetCursorPosition(106, 15);
            Console.WriteLine("Press enter to continue ");
            Console.SetCursorPosition(106, 17);
            Console.ReadLine();
            return true; 
        }

        public bool OfferADraw(User user)
        {
            Console.SetCursorPosition(106, 11);
            Console.WriteLine($"Do you want to offer a draw - {user.Name} ?");
            Console.SetCursorPosition(106, 13);
            Console.WriteLine($"1. yes");
            Console.SetCursorPosition(106, 14);
            Console.WriteLine($"2. no");
            Console.SetCursorPosition(106, 16);
            Enum.TryParse(Console.ReadLine(), out OfferADrawMenu offerADraw);
            switch (offerADraw)
            {
                case OfferADrawMenu.Yes:
                    {
                        return true;
                    }
                case OfferADrawMenu.No:
                    {
                        return false;
                    }
            }
            return false;
        }

        public void TheDrawOfferHasAlreadyBeenSent()
        {
            Console.SetCursorPosition(106, 11);
            Console.Write("You have already offered a draw.");
            Console.SetCursorPosition(106, 12);
            Console.Write("Wait for the opponent's response.");
            Console.SetCursorPosition(106, 14);
            Console.Write("Press enter to continue.");
            Console.SetCursorPosition(106, 15);
            Console.ReadLine();    
        }
        
        public bool AcceptOrRejectOfferADraw(User user)
        {
            Console.SetCursorPosition(106, 4);
            Console.WriteLine($"Do you accept a draw - {user.Name} ?");
            Console.SetCursorPosition(106, 6);
            Console.WriteLine($"1. yes");
            Console.SetCursorPosition(106, 8);
            Console.WriteLine($"2. no");
            Console.SetCursorPosition(106, 10);
            Enum.TryParse(Console.ReadLine(), out OfferADrawMenu offerADraw);
            switch (offerADraw)
            {
                case OfferADrawMenu.Yes:
                    {
                        Console.SetCursorPosition(106, 12);
                        Console.WriteLine($"The {user.Name} accepted a draw.");
                        Console.SetCursorPosition(106, 14);
                        Console.WriteLine($"Draw!");
                        Console.SetCursorPosition(106, 16);
                        Console.WriteLine("Press enter to continue ");
                        Console.SetCursorPosition(106, 17);
                        Console.ReadLine();
                        return true;
                    }
                case OfferADrawMenu.No:
                    {
                        ClearMenuInGame();
                        return false;
                    }
            }
            return false;
        }

        public void ClearMenuInGame()
        {
            for (int x = 4; x < 30; x++)
            {
                for (int y = 106; y < 150; y++)
                {
                    
                    Console.SetCursorPosition(y, x);
                    Console.Write(" ");
                }
            }
        }

        public (int, int) ChooseYourChecker(Cell[,] cells, User user, int xCheacker, int yCheacker)
        {
            // Вибрати шашку +
            // Перевірити кординати шашки, чи вони в межах поля +
            // Перевірити чи шашка належить гравцю +
            // Перевірити чи шашка може ходити, тобто чи навколо неї нема інших шашок
            // Підсвітити шашку +
            // Підсвітити можливі ходи
            // Підсвітити неможливі ходи
            string checkerColor = "";
            if (user.CheckerColorWhite)
            {
                checkerColor = "White";
            }
            else
            {
                checkerColor = "Black";
            }

            do
            {
                do
                {
                    ClearMenuInGame();

                    Console.SetCursorPosition(106, 4);
                    Console.Write($"Choose your checker : {checkerColor}");

                    Console.SetCursorPosition(106, 6);
                    Console.Write("X - ");
                    Console.SetCursorPosition(110, 6);
                    var xUserInput = Console.ReadKey(true).KeyChar;
                    Console.WriteLine(
                    char.IsDigit(xUserInput)
                    ? $"{xCheacker = int.Parse(xUserInput.ToString())}"
                    : "You should input only a number");


                    Console.SetCursorPosition(106, 8);
                    Console.Write("Y - ");
                    Console.SetCursorPosition(110, 8);
                    var yUserInput = Console.ReadKey(true).KeyChar;
                    Console.WriteLine(
                    char.IsDigit(yUserInput)
                    ? $"{yCheacker = int.Parse(yUserInput.ToString())}"
                    : "You should input only a number");

                    Console.SetCursorPosition(106, 10);
                    Console.ReadLine();

                } while (xCheacker < -1 || xCheacker > 8 || yCheacker < -1 || yCheacker > 8);
                Field.ChooseAChecker(cells, user, xCheacker, yCheacker);
            } while (!Field.ChooseAChecker(cells, user, xCheacker, yCheacker));




            Console.Clear();
            Field.DrawField(cells);








            return (xCheacker, yCheacker);
        }
    }
}

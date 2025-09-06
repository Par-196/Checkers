using Checkers.Model;
using Checkers.Model.Enums;
using System;

namespace Checkers
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(160, 48);
            Player firstPlayer = new Player("Par196", true);
            Player secondPlayer = new Player("Pasta298", false);
            Bot bot = new Bot("Bot", false);
            Game game = new Game();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n\n\t1. Start game with player\n" +
                    "\t2. Play with Bot\n" +
                    "\t3. Settings\n" +
                    "\t4. Rules\n" +
                    "\t5. Exit");
                Console.SetCursorPosition(8, 8);
                Enum.TryParse(Console.ReadLine(), out MeinMenu meinMenu);
                Console.Clear();
                switch (meinMenu)
                {
                    case MeinMenu.StartGameWithPlayer:
                        {
                            game.StartGame(firstPlayer, secondPlayer);
                            Console.Clear();
                        }
                        break;
                    case MeinMenu.PlayWithBot:
                        {
                            game.StartGameWithBot(firstPlayer, bot);
                            Console.Clear();
                        }
                        break;
                    case MeinMenu.Settings:
                        {
                            bool exitSettings = false;
                            do 
                            {
                                Console.WriteLine($"\n\n\tFirst player - Name: {firstPlayer.Name} - White checkers: {firstPlayer.CheckerColorWhite}");
                                Console.WriteLine($"\tSecond player - Name: {secondPlayer.Name} - White checkers: {secondPlayer.CheckerColorWhite}");
                                Console.WriteLine($"\tBot - Name: {bot.Name} - White checkers: {bot.CheckerColorWhite}");
                                Console.WriteLine("\n\tSelect player to change settings:\n");
                                Console.WriteLine("\t1. First player\n" +
                                    "\t2. Second player\n" +
                                    "\t3. Bot\n" +
                                    "\t4. Exit\n");
                                Console.SetCursorPosition(8, 13);
                                Enum.TryParse(Console.ReadLine(), out SettingsMenu settingsMenu);
                                if (settingsMenu == SettingsMenu.Exit)
                                {
                                    exitSettings = true;
                                    continue;
                                }
                                Console.WriteLine("\n\t1. Change Name\n" +
                                            "\t2. Change side\n");
                                Console.SetCursorPosition(8, 18);
                                Enum.TryParse(Console.ReadLine(), out SettingsPlayerMenu settingsPlayerMenu);
                                switch (settingsMenu)
                                {
                                    case SettingsMenu.FirstPlayer:
                                        {
                                            switch (settingsPlayerMenu)
                                            {
                                                case SettingsPlayerMenu.ChangeName:
                                                    {
                                                        Console.WriteLine("\n\tEnter new name");
                                                        Console.SetCursorPosition(8, 21);
                                                        firstPlayer.Name = Console.ReadLine();
                                                    }
                                                    break;
                                                case SettingsPlayerMenu.ChangeSide:
                                                    {
                                                        firstPlayer.CheckerColorWhite = !firstPlayer.CheckerColorWhite;
                                                        secondPlayer.CheckerColorWhite = !secondPlayer.CheckerColorWhite;
                                                        bot.CheckerColorWhite = !bot.CheckerColorWhite;
                                                    }
                                                    break;
                                            }
                                            Console.Clear();
                                        }
                                        break;
                                    case SettingsMenu.SecondPlayer:
                                        {
                                            switch (settingsPlayerMenu)
                                            {
                                                case SettingsPlayerMenu.ChangeName:
                                                    {
                                                        Console.WriteLine("\n\tEnter new name");
                                                        Console.SetCursorPosition(8, 21);
                                                        secondPlayer.Name = Console.ReadLine();
                                                    }
                                                    break;
                                                case SettingsPlayerMenu.ChangeSide:
                                                    {
                                                        firstPlayer.CheckerColorWhite = !firstPlayer.CheckerColorWhite;
                                                        secondPlayer.CheckerColorWhite = !secondPlayer.CheckerColorWhite;
                                                        bot.CheckerColorWhite = !bot.CheckerColorWhite;
                                                    }
                                                    break;
                                            }
                                            Console.Clear();
                                        }
                                        break;
                                    case SettingsMenu.Bot:
                                        {
                                            switch (settingsPlayerMenu)
                                            {
                                                case SettingsPlayerMenu.ChangeName:
                                                    {
                                                        Console.WriteLine("\n\tEnter new name");
                                                        Console.SetCursorPosition(8, 21);
                                                        bot.Name = Console.ReadLine();
                                                    }
                                                    break;
                                                case SettingsPlayerMenu.ChangeSide:
                                                    {
                                                        firstPlayer.CheckerColorWhite = !firstPlayer.CheckerColorWhite;
                                                        secondPlayer.CheckerColorWhite = !secondPlayer.CheckerColorWhite;
                                                        bot.CheckerColorWhite = !bot.CheckerColorWhite;
                                                    }
                                                    break;
                                            }
                                        }
                                        break;
                                }
                                Console.Clear();
                            }
                            while (!exitSettings);
                            Console.Clear();

                        }
                        break;
                    case MeinMenu.Rules:
                        {
                            Console.WriteLine("\n\n\tRules of Checkers:\n" +
                                "\n\t1. The game is played on an 8x8 board with alternating light and dark squares.\n" +
                                "\n\t2. Each player starts with 12 pieces placed on the dark squares of the three rows closest to them.\n" +
                                "\n\t3. Players take turns moving one piece at a time. Regular pieces can only move diagonally forward to an adjacent unoccupied dark square.\n" +
                                "\n\t4. If an opponent's piece is diagonally adjacent and the square immediately beyond it is empty, a player must jump over the opponent's piece,\n" +
                                "\n\t   capturing it. Multiple jumps are allowed in a single turn if possible.\n" +
                                "\n\t5. When a piece reaches the farthest row from its starting position, it is crowned and becomes a king. \n" +
                                "\n\t   Kings can move diagonally both forward and backward.\n" +
                                "\n\t6. The game ends when a player captures all of the opponent's pieces or blocks them from making any legal moves.\n" +
                                "\n\t7. If neither player can force a win, the game is declared a draw.\n" +
                                "\n\n\tPress enter to go to the main menu.");
                            Console.SetCursorPosition(8, 25);
                            Console.ReadLine();
                            Console.Clear();
                        }
                        break;
                    case MeinMenu.Exit:
                        {
                            exit = true;
                        }
                        break;
                }
            }
            Console.ReadLine();
        }
    }
}

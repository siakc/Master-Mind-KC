//SiaMasterMind
//Copyright (C) 2010  SiavoshKC

//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Text;

namespace MasterMind
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Mastermind by SiavoshKC\nPress F10 to exit");
            string[] tryOptions = {"Easy = 45 tries","Medium = 30 tries","Hard = 15 tries","Impossible = 3 tries"};
            string[] charNumOptions = {"3 characters","4 characters","5 characters","6 characters"};
            
            do
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                uint tries = 0;
                uint charNum = 0;
                Console.WriteLine("\n\nSelect the game difficulity: ");
                switch (Menu(tryOptions))
                {
                    case '1':
                        tries = 45;
                        break;
                    case '2': 
                        tries = 30;
                        break;
                    case '3':
                        tries = 15;
                        break;
                    case '4':
                        tries = 3;
                        break;
                }
                Console.WriteLine("\nSelect the number of characters to be guessed:");
                switch (Menu(charNumOptions))
                {
                    case '1':
                        charNum = 3;
                        break;
                    case '2': 
                        charNum = 4;
                        break;
                    case '3':
                        charNum = 5;
                        break;
                    case '4':
                        charNum = 6;
                        break;
                }


                Console.WriteLine('\n');
                

                ShowResults(tries, GameCore.PlayGameNRounds(tries, charNum));
                
            } while (AskForRestart());

            while (Console.KeyAvailable);
            
            Console.WriteLine("\nPress a key to exit...");
            Console.ReadKey(true);
        }
        static bool AskForRestart()
        {
            Console.Write("Play again? (Y / Any key) ");
            return (Console.ReadKey().KeyChar == 'y');

        }

        static int Menu(string[] options)
        {
            int numOfOptions = 0;
            foreach (string opt in options)
            {
                Console.WriteLine("{0}) {1}", numOfOptions + 1, opt);
                ++numOfOptions;
            }
            Console.Write("\nSelect one of the options: ");
            ConsoleKeyInfo key = new ConsoleKeyInfo();
            do
            {
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.F10)
                {
                    Environment.Exit(Environment.ExitCode);
                }
            }
            while (!(key.KeyChar > '0' && key.KeyChar < (numOfOptions + 1 + '0')));
            Console.WriteLine(key.KeyChar);
            return key.KeyChar;


        }
        static void ShowResults(uint tries, uint num)
        {
            if ((num > 0) && (num < tries / 3))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("You Won! You are very smart.");

            }
            else if ((num > tries / 3) && (num <= (tries / 3) * 2))
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("You Won! Good work.");

            }
            else if ((num > (tries / 3) * 2) && (num <= tries))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("You Won! But you should guess faster.");

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You Lost! The string was {0}", GameCore.secretString);

            }
        }

                      
    }
}

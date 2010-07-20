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
    static class GameCore
    {
        private static Random rnd = new Random();
        private static StringBuilder randomString = new StringBuilder();

        public static string secretString 
        {
            get{return randomString.ToString();}
        }

        private static string GetUserGuessString(uint charNum)
        {
            StringBuilder userGuess = new StringBuilder();
            while (userGuess.Length < charNum)
            {
                ConsoleKeyInfo userGuessChar = Console.ReadKey(true);
                if (userGuessChar.Key == ConsoleKey.F10)
                    Environment.Exit(Environment.ExitCode);

                else if (userGuessChar.Key == ConsoleKey.Backspace && userGuess.Length > 0)
                {
                  
                    Console.Write(userGuessChar.KeyChar);
                    userGuess.Remove(userGuess.Length - 1, 1);
                    Console.Write(' ');
                    Console.Write(userGuessChar.KeyChar);
                    
                }
                else if (userGuessChar.KeyChar >= 'a' && userGuessChar.KeyChar <= 'z')
                {
                    
                    Console.Write(userGuessChar.KeyChar);
                    userGuess.Append(userGuessChar.KeyChar);
                }
            }
            return userGuess.ToString();            
        }

        private static string MakeRandomString(uint charNumber)
        {
            randomString.Remove(0, randomString.Length);
            for (int i = 0; i < charNumber; ++i )
                randomString.Append(Convert.ToChar((rnd.Next('a','z'))));

            return randomString.ToString();
        }

        private static void CompareGuessAndSecret(string userGuessString, string secretString, out uint missPlaced, out uint correctPlaced)
        {
            missPlaced = correctPlaced = 0;
			StringBuilder sUserGuessString = new StringBuilder(userGuessString);
			StringBuilder sSecretString = new StringBuilder(secretString);

			for (int pos = 0; pos < sSecretString.Length; ++pos)
				if (sSecretString[pos] == sUserGuessString[pos])
				{
					sSecretString[pos] = '_';
					sUserGuessString[pos] = '_';
					correctPlaced++;
				}
			for (int pos = 0; pos < sSecretString.Length; ++pos)
				if (sUserGuessString[pos] == '_') continue;
				else
				{
					for (int j = 0; j < sSecretString.Length; ++j)
						if (sUserGuessString[pos] == sSecretString[j])
						{
							sSecretString[j] = '_';
							missPlaced++;
							break;
						}
				}

        }
 
        public static uint PlayGameNRounds(uint roundsLimit, uint charNum)
        {
            uint guessTry;
            uint missPlaced;
            uint correctPlaced;
            string userGuess;
            string secretString = MakeRandomString(charNum);
			//secretString = Console.ReadLine();			//Unrem this to give manual number
			//Console.WriteLine(randomString.ToString());	//Unrem it to fake the game.
            Console.WriteLine("Try---Guess-----------Correct Placed--------Miss Placed");
            for (guessTry = 0; guessTry < roundsLimit; ++guessTry)
            {
                Console.Write("{0,-6:##:}", guessTry + 1);
                userGuess = GetUserGuessString(charNum);
                CompareGuessAndSecret(userGuess, secretString,out missPlaced, out correctPlaced);
                Console.WriteLine("{0,18}{1,20}", correctPlaced, missPlaced);
                if (correctPlaced == charNum) break;
            }

            return guessTry + 1;
        }
    }
}

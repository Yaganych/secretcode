using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace secretcode_SolomatinKiril
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu();
        }
        static void Menu()
        {
            //Menu
            Console.Title = "Solomatin Kiril, \"SecretCode\"";
            string Square = "■";

            Console.WriteLine("╔══════════════════Kiril Solomatin══════════════════╗");
            Console.WriteLine("║                                                   ║");
            Console.WriteLine("║        Bienvenue dans le jeu: Secret Code         ║");
            Console.WriteLine("║                                                   ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════╝");
            Console.WriteLine();

            Console.WriteLine("Un code secret composé de 4 chiffres est généré.\nÀ toi de le découvrir en 10 essais maximum !");
            Console.WriteLine(); 

            Console.WriteLine("À chaque essai, tu reçois un indice selon le niveau choisi.");
            Console.WriteLine();

            Console.WriteLine("Pour les niveaux 1 et 3 avec indices visibles :");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(Square + " ");
            Console.ResetColor();
            Console.WriteLine(": chiffre bien placé");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(Square + " ");
            Console.ResetColor();
            Console.WriteLine(": chiffre correct mais mal placé");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(Square + " ");
            Console.ResetColor();
            Console.WriteLine(": chiffre absent");
            Console.WriteLine();

            Console.WriteLine("Exemple :\nCode secret : 1234 (caché)\nVotre essai : 1325\nIndice");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(Square);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(Square);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(Square);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(Square);
            Console.ResetColor();
            Console.WriteLine(" (1 bien placé, 2 mal placés, 1 absent)");
            Console.WriteLine();

            Console.WriteLine("Pour les niveaux 2 et 4 avec indices discrets :\nExemple :\nCode secret : 5413 (caché)\nVotre essai : 1234\nIndice\t:");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("0 bien placé(s), 3 mal placé(s)");
            Console.ResetColor();
            Console.WriteLine();

            Console.WriteLine("Appuie sur une touche pour commencer...  ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.ResetColor();

            Console.ReadKey();
            Console.Clear();
            //level selection
            bool valueOk = false;
            int levelNumber = 0;

            Console.WriteLine("=== SECRET CODE ===");
            Console.WriteLine();

            Console.WriteLine("Choisi un niveau :");
            Console.WriteLine("1. Débutant       (1 à 6, sans doublons, indices visibles)");
            Console.WriteLine("2. Intermédiaire  (1 à 6, sans doublons, indices discrets)");
            Console.WriteLine("3. Avancé         (1 à 8, avec doublons, indices visibles)");
            Console.WriteLine("4. Expert         (1 à 9, avec doublons, indices discrets)");
            Console.WriteLine();

            Console.Write("Votre choix (1-4) :");

            while (!valueOk || levelNumber < 1 || levelNumber > 4)
            {
                valueOk = int.TryParse(Console.ReadLine(), out levelNumber);

                if (valueOk && levelNumber >= 1 && levelNumber <= 4)
                {
                    if (levelNumber == 1)
                    {
                        Console.Clear();
                        Console.WriteLine("=== SECRET CODE Niveau 1 ===");

                        Debutant();
                    }

                    if (levelNumber == 2)
                    {
                        Console.Clear();
                        Console.WriteLine("=== SECRET CODE Niveau 2 ===");
                        Intermediate();
                    }

                    if (levelNumber == 3)
                    {
                        Console.Clear();
                        Console.WriteLine("=== SECRET CODE Niveau 3 ===");
                        Advanced();
                    }

                    if (levelNumber == 4)
                    {
                        Console.Clear();
                        Console.WriteLine("=== SECRET CODE Niveau 4 ===");
                        Expert();
                    }
                }

                else
                {
                    Console.WriteLine("Choix invalide. Essaie encore.");
                }
            }
        }
        static void Debutant()
        {
            List<int> hiddenNumberList = new List<int>();
            List<int> userNumberList = new List<int>();
            bool validInput = false;
            bool thereIsNotDouble = true;
            int userNumber;

            int numberOfAttempts = 1;

            RandomNumberGenerator(hiddenNumberList, 6, false);

            foreach (int number in hiddenNumberList)
            {
                Console.Write(number);
            }

            Console.WriteLine("=== SECRET CODE Niveau 1 ===\n");
            Console.WriteLine("Essais : \n\n");

            while (numberOfAttempts <= 10)
            {
                Console.WriteLine("Essai {0}/10 : ", numberOfAttempts);
                Console.Write("Entre 4 chiffres entre 1 et 6 (ex: 1234) : ");
                validInput = int.TryParse(Console.ReadLine(), out userNumber);

                foreach (char number in Convert.ToString(userNumber))
                {
                    userNumberList.Add(int.Parse(number.ToString()));
                }

                if ((userNumberList.Count == 4) && validInput)
                {
                    for (int i = 0; i < userNumberList.Count - 1; i++)
                    {

                        for (int j = i + 1; j < userNumberList.Count; j++)
                        {

                            if (userNumberList[i] == userNumberList[j])
                            {
                                thereIsNotDouble = false;
                                break;
                            }
                            else
                            {
                                thereIsNotDouble = true;
                            }
                        }
                        if (!thereIsNotDouble)
                            break;
                    }
                }

                if (userNumberList.Count != 4 || !validInput)
                    Console.WriteLine("Tu n'as pas donné 4 chiffres ! essaie de nouveau");

                if (thereIsNotDouble && validInput && hiddenNumberList.SequenceEqual(userNumberList))
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Félicitations, le nombre " + userNumber + " est un numéro secret");
                    Console.ResetColor();
                    break;
                }

                else if (!thereIsNotDouble)
                {
                    Console.WriteLine("Pas de doublons autorisés à ce niveau.");
                }

                if (thereIsNotDouble && validInput && !(hiddenNumberList.SequenceEqual(userNumberList)) && userNumberList.Count == 4)
                {
                    Console.CursorLeft = 1;
                    Console.Write("{0}: ", numberOfAttempts);
                    foreach (int n in userNumberList)
                    {
                        Console.Write(n);
                    }
                    Console.WriteLine();
                    Console.CursorLeft = 4;
                    for (int i = 0; i < hiddenNumberList.Count; i++)
                    {
                        if (userNumberList[i].Equals(hiddenNumberList[i]))
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write("■");
                            Console.ResetColor();
                        }

                        else if (hiddenNumberList.Contains(userNumberList[i]))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("■");
                            Console.ResetColor();
                        }

                        else
                        {
                            Console.Write("■");
                        }
                    }
                    Console.WriteLine();
                    numberOfAttempts++;
                }

                userNumberList.Clear();
            }

            Console.WriteLine();

            endGame(numberOfAttempts, hiddenNumberList);
        }

        static void Intermediate()
        {
            List<int> hiddenNumberList = new List<int>();
            List<int> userNumberList = new List<int>();
            bool validInput = false;
            bool thereIsNotDouble = true;
            int userNumber;
            int correctPlace = 0;
            int correctNumber = 0;

            int numberOfAttempts = 1;

            RandomNumberGenerator (hiddenNumberList, 6, false);

            foreach (int number in hiddenNumberList)
            {
                Console.Write(number);
            }

            Console.WriteLine("=== SECRET CODE Niveau 2 ===\n");
            Console.WriteLine("Essais : \n\n");

            while (numberOfAttempts <= 10)
            {
                Console.WriteLine("Essai {0}/10 : ", numberOfAttempts);
                Console.Write("Entre 4 chiffres entre 1 et 6 (ex: 1234) : ");
                validInput = int.TryParse(Console.ReadLine(), out userNumber);

                foreach (char number in Convert.ToString(userNumber))
                {
                    userNumberList.Add(int.Parse(number.ToString()));
                }

                if ((userNumberList.Count == 4) && validInput)
                {
                    for (int i = 0; i < userNumberList.Count - 1; i++)
                    {

                        for (int j = i + 1; j < userNumberList.Count; j++)
                        {

                            if (userNumberList[i] == userNumberList[j])
                            {
                                thereIsNotDouble = false;
                                break;
                            }
                            else
                            {
                                thereIsNotDouble = true;
                            }
                        }
                        if (!thereIsNotDouble)
                            break;
                    }
                }

                if (userNumberList.Count != 4 || !validInput)
                    Console.WriteLine("Tu n'as pas donné 4 chiffres ! essaie de nouveau");

                if (thereIsNotDouble && validInput && hiddenNumberList.SequenceEqual(userNumberList))
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Félicitations, le nombre " + userNumber + " est un numéro secret");
                    Console.ResetColor();
                    break;
                }

                else if (!thereIsNotDouble)
                {
                    Console.WriteLine("Pas de doublons autorisés à ce niveau.");
                }

                if (thereIsNotDouble && validInput && !(hiddenNumberList.SequenceEqual(userNumberList)) && userNumberList.Count == 4)
                {
                    correctNumber = 0;
                    correctPlace = 0;
                    List<int> findNumbers = new List<int>();

                    for (int i = 0; i < hiddenNumberList.Count; i++)
                    {
                        if (hiddenNumberList[i] == userNumberList[i])
                        {
                            correctPlace++;
                            findNumbers.Add(userNumberList[i]);
                        }
                    }

                    for (int i = 0; i < hiddenNumberList.Count; i++)
                    {
                        if (!findNumbers.Contains(userNumberList[i]) && hiddenNumberList.Contains(userNumberList[i]))
                        {
                            correctNumber++;
                        }
                    }

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("{0} bien placé(s), {1} mal placé(s) \n", correctPlace, correctNumber);
                    Console.ResetColor();
                    numberOfAttempts++;
                }
                userNumberList.Clear();
            }

            Console.WriteLine();

            endGame(numberOfAttempts, hiddenNumberList);
        }

        static void Advanced()
        {
            List<int> hiddenNumberList = new List<int>();
            List<int> userNumberList = new List<int>();
            bool validInput = false;
            int userNumber;

            int numberOfAttempts = 1;

            RandomNumberGenerator(hiddenNumberList, 8, true);

            foreach (int n in hiddenNumberList)
            {
                Console.Write(n);
            }

            Console.WriteLine("=== SECRET CODE Niveau 3 ===\n");
            Console.WriteLine("Essais : \n\n");

            while (numberOfAttempts <= 10)
            {
                List<int?> colorValue = new List<int?>() { null, null, null, null };
                List<int> notFoundsNumbers = new List<int>(hiddenNumberList);
                foreach (int number in hiddenNumberList)
                {
                    Console.Write(number);
                }
                Console.WriteLine();

                Console.WriteLine();
                Console.WriteLine("Essai {0}/10 : ", numberOfAttempts);
                Console.Write("Entre 4 chiffres entre 1 et 9 (ex: 1234) : ");
                validInput = int.TryParse(Console.ReadLine(), out userNumber);

                foreach (char number in Convert.ToString(userNumber))
                {
                    userNumberList.Add(int.Parse(number.ToString()));
                }

                if (userNumberList.Count != 4 || !validInput)
                    Console.WriteLine("Tu n'as pas donné 4 chiffres ! essaie de nouveau");

                if (validInput && hiddenNumberList.SequenceEqual(userNumberList))
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Félicitations, le nombre " + userNumber + " est un numéro secret");
                    Console.ResetColor();
                    break;
                }

                if (validInput && !(hiddenNumberList.SequenceEqual(userNumberList)) && userNumberList.Count == 4)
                {
                    Console.CursorLeft = 1;
                    Console.Write("{0}: ", numberOfAttempts);
                    foreach (int n in userNumberList)
                    {
                        Console.Write(n);
                    }
                    Console.WriteLine();
                    Console.CursorLeft = 4;

                    for (int i = 0; i < hiddenNumberList.Count; i++)
                    {
                        if (userNumberList[i].Equals(hiddenNumberList[i]))
                        {
                            colorValue[i] = 2;
                            notFoundsNumbers.Remove(hiddenNumberList[i]);
                        }
                    }

                    for (int j = 0; j < hiddenNumberList.Count; j++)
                    {
                        if (notFoundsNumbers.Contains(userNumberList[j]) && colorValue[j] != 2)
                        {
                            colorValue[j] = 1;
                            notFoundsNumbers.Remove(userNumberList[j]);
                        }
                        else if (colorValue[j] != 2)
                        {
                            colorValue[j] = 0;
                        }
                    }

                    for (int i = 0; i < hiddenNumberList.Count; i++)
                    {
                        if (colorValue[i] == 2)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write("■");
                            Console.ResetColor();
                        }
                        else if (colorValue[i] == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("■");
                            Console.ResetColor();
                        }
                        else if (colorValue[i] == 0)
                        {
                            Console.Write("■");
                        }
                    }
                    Console.WriteLine();
                    numberOfAttempts++;
                }

                userNumberList.Clear();
            }

            Console.WriteLine();

            endGame(numberOfAttempts, hiddenNumberList);
        }

        static void Expert()
        {
            Console.Title = "Expert";

            List<int> userListNumber = new List<int>();
            List<int> hiddenNumberList = new List<int>();

            RandomNumberGenerator(hiddenNumberList, 9, true);

            int userNumber;
            bool validInput = false;
            bool numberGuessed = false;
            int numberOfAttempts = 1;
            int correctPlace = 0;
            int correctNumber = 0;

            foreach (int number in hiddenNumberList)
            {
                Console.Write(number);
            }
            Console.WriteLine();

            Console.WriteLine("Essais: \n");

            while (!numberGuessed && numberOfAttempts <= 10)
            {
                Console.WriteLine(numberOfAttempts + "/10 :");
                Console.Write("Ente 4 chiffres entre 1 et 9 (ex: 1234) :");
                validInput = int.TryParse(Console.ReadLine(), out userNumber);

                foreach (char number in Convert.ToString(userNumber))
                {
                    userListNumber.Add(int.Parse(number.ToString()));
                }

                List<int> findNumbers = new List<int>();

                if (validInput && userListNumber.Count == 4)
                {
                    if (userListNumber.SequenceEqual(hiddenNumberList))
                    {
                        Console.WriteLine("Félicitations, le nombre " + userNumber + " est un numéro secret");
                        numberGuessed = true;
                    }

                    else
                    {
                        for (int i = 0; i < hiddenNumberList.Count; i++)
                        {
                            if (hiddenNumberList[i] == userListNumber[i])
                            {
                                correctPlace++;
                                findNumbers.Add(userListNumber[i]);
                            }
                        }

                        for (int j = 0; j < hiddenNumberList.Count; j++)
                        {
                            if (hiddenNumberList.Contains(userListNumber[j]) && !findNumbers.Contains(userListNumber[j]))
                            {
                                correctNumber++;
                            }
                        }


                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("{0} bien placé(s), {1} mal placé(s) \n", correctPlace, correctNumber);
                        Console.ResetColor();
                        correctNumber = 0;
                        correctPlace = 0;
                    }

                    numberOfAttempts++;
                    userListNumber.Clear();
                    findNumbers.Clear();
                }

                else if (!(userListNumber.Count == 4))
                {
                    Console.WriteLine("Tu n'as pas donné 4 chiffres ! essaie de nouveau");
                    userListNumber.Clear();
                    findNumbers.Clear();
                }

                else
                {
                    Console.WriteLine("Tu n'as pas entré un nombre! essaie de nouveau");
                    userListNumber.Clear();
                    findNumbers.Clear();
                }
            }
            Console.WriteLine();

            endGame(numberOfAttempts, hiddenNumberList);


        }

        static void endGame(int numberOfAttempts, List<int> hiddenNumber)
        {
            if (numberOfAttempts > 10)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Perdu ! Le code était : ");
                Console.ResetColor();
                foreach (int number in hiddenNumber)
                {
                    Console.Write(number);
                }
                Console.ResetColor();
                Console.WriteLine();
            }

            ConsoleKeyInfo chose = new ConsoleKeyInfo();
            bool validKey = false;

            while (!validKey)
            {
                Console.WriteLine("Veux-tu recommencer ? (o / n):");
                chose = Console.ReadKey();
                if (chose.Key == ConsoleKey.O || chose.Key == ConsoleKey.N)
                {
                    validKey = true;
                    break;
                }
                else
                {
                    Console.WriteLine("Choix invalide. Saisi 'o' pour recommencer ou 'n' pour quitter.");
                }
                chose = new ConsoleKeyInfo();
            }

            if (chose.Key == ConsoleKey.O)
            {
                Menu();
            }

            else if (chose.Key == ConsoleKey.N)
            {
                Environment.Exit(0);
            }
        }

        static void RandomNumberGenerator (List<int> hiddenNumberList, int maxValue, bool allowDuplicates)
        {
            Random random = new Random();
            while (hiddenNumberList.Count < 4)
            {
                int number = random.Next(1, maxValue + 1);
                if (!allowDuplicates)
                {
                    if (!(hiddenNumberList.Contains(number)))
                    {
                        hiddenNumberList.Add(number);
                    }
                }
                else
                {
                    hiddenNumberList.Add(number);
                }
            }
        }
    }


}



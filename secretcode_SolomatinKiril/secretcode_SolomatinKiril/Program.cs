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
            Console.Title = "Solomatin Kiril, \"SecretCode\" I319";
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
            Rules();
        }

        //Affichage des niveaux et des régles
        static void Rules()
        {  
            Console.WriteLine("=== SECRET CODE ===");
            Console.WriteLine();

            Console.WriteLine("Choisi un niveau :");
            Console.WriteLine("1. Débutant       (1 à 6, sans doublons, indices visibles)");
            Console.WriteLine("2. Intermédiaire  (1 à 6, sans doublons, indices discrets)");
            Console.WriteLine("3. Avancé         (1 à 8, avec doublons, indices visibles)");
            Console.WriteLine("4. Expert         (1 à 9, avec doublons, indices discrets)");
            Console.WriteLine();
            LevelSelection();
        }

        //Sélection d'un niveau 
        static void LevelSelection()
        {
            bool valueOk = false;
            int levelNumber = 0;
            Console.Write("Votre choix (1-4) : ");
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

        //Niveau Debutant
        static void Debutant()
        {
            List<int> hiddenNumberList = new List<int>();
            List<int> userNumberList = new List<int>();
            bool thereIsNotDouble = false;
            int userNumber = 0;

            int numberOfAttempts = 1;
            const int MAX_ATTEMPTS = 10;
            const int NUMBER_LENGTH = 4;
            const int MIN_NUMBER = 1;
            const int MAX_NUMBER = 6;

            RandomNumberGenerator(hiddenNumberList, 6, false);

            foreach (int number in hiddenNumberList)
            {
                Console.Write(number);
            }

            Console.WriteLine("=== SECRET CODE Niveau 1 ===\n");
            Console.WriteLine("Essais : \n\n");

            while (numberOfAttempts <= MAX_ATTEMPTS)
            {
                Console.WriteLine("Essai {0}/10 : ", numberOfAttempts);
                Console.Write("Entre 4 chiffres entre 1 et 6 (ex: 1234) : ");

                if (UserInputIsValid(userNumber, userNumberList, MIN_NUMBER, MAX_NUMBER, NUMBER_LENGTH))
                {
                    ThereIsNotDouble(userNumberList, ref thereIsNotDouble);
                }

                else
                    thereIsNotDouble = false;

                if (thereIsNotDouble && hiddenNumberList.SequenceEqual(userNumberList))
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Félicitations, le nombre " + userNumber + " est un numéro secret");
                    Console.ResetColor();
                    break;
                }

                if (thereIsNotDouble && !(hiddenNumberList.SequenceEqual(userNumberList)))
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
                    Console.WriteLine();
                    numberOfAttempts++;
                }

                userNumberList.Clear();
            }

            Console.WriteLine();

            EndGame(numberOfAttempts, hiddenNumberList);
        }

        //Niveau Intermediate
        static void Intermediate()
        {
            List<int> hiddenNumberList = new List<int>();
            List<int> userNumberList = new List<int>();
            bool validInput = false;
            bool thereIsNotDouble = true;
            int userNumber = 0;
            int correctPlace = 0;
            int correctNumber = 0;

            int numberOfAttempts = 1;
            const int MAX_ATTEMPTS = 10;
            const int NUMBER_LENGTH = 4;
            const int MIN_NUMBER = 1;
            const int MAX_NUMBER = 6;

            RandomNumberGenerator(hiddenNumberList, MAX_NUMBER, false);

            foreach (int number in hiddenNumberList)
            {
                Console.Write(number);
            }

            Console.WriteLine("=== SECRET CODE Niveau 2 ===\n");
            Console.WriteLine("Essais : \n\n");

            while (numberOfAttempts <= MAX_ATTEMPTS)
            {
                Console.WriteLine("Essai {0}/10 : ", numberOfAttempts);
                Console.Write("Entre 4 chiffres entre 1 et 6 (ex: 1234) : ");

                if (UserInputIsValid(userNumber, userNumberList, MIN_NUMBER, MAX_NUMBER, NUMBER_LENGTH))
                {
                    ThereIsNotDouble(userNumberList, ref thereIsNotDouble);
                }

                else
                    thereIsNotDouble = false;

                if (thereIsNotDouble && hiddenNumberList.SequenceEqual(userNumberList))
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Félicitations, le nombre " + userNumber + " est un numéro secret");
                    Console.ResetColor();
                    break;
                }

                if (thereIsNotDouble && !(hiddenNumberList.SequenceEqual(userNumberList)))
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

            EndGame(numberOfAttempts, hiddenNumberList);
        }

        //Niveau Advanced
        static void Advanced()
        {
            List<int> hiddenNumberList = new List<int>();
            List<int> userNumberList = new List<int>();
            bool validInput = false;
            int userNumber;

            int numberOfAttempts = 1;
            const int MAX_ATTEMPTS = 10;
            const int NUMBER_LENGTH = 4;

            RandomNumberGenerator(hiddenNumberList, 8, true);

            foreach (int n in hiddenNumberList)
            {
                Console.Write(n);
            }

            Console.WriteLine("=== SECRET CODE Niveau 3 ===\n");
            Console.WriteLine("Essais : \n\n");

            while (numberOfAttempts <= MAX_ATTEMPTS)
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

                if (userNumberList.Count != NUMBER_LENGTH || !validInput)
                    Console.WriteLine("Tu n'as pas donné 4 chiffres ! essaie de nouveau");

                if (validInput && hiddenNumberList.SequenceEqual(userNumberList))
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Félicitations, le nombre " + userNumber + " est un numéro secret");
                    Console.ResetColor();
                    break;
                }

                if (validInput && !(hiddenNumberList.SequenceEqual(userNumberList)) && userNumberList.Count == NUMBER_LENGTH)
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

            EndGame(numberOfAttempts, hiddenNumberList);
        }

        //Niveau Expert
        static void Expert()
        {
            Console.Title = "Expert";

            List<int> userNumberList = new List<int>();
            List<int> hiddenNumberList = new List<int>();

            RandomNumberGenerator(hiddenNumberList, 9, true);

            int userNumber;
            bool validInput = false;
            bool numberGuessed = false;

            int numberOfAttempts = 1;
            const int MAX_ATTEMPTS = 10;
            const int NUMBER_LENGTH = 4;

            int correctPlace = 0;
            int correctNumber = 0;

            foreach (int number in hiddenNumberList)
            {
                Console.Write(number);
            }
            Console.WriteLine();

            Console.WriteLine("Essais: \n");

            while (!numberGuessed && numberOfAttempts <= MAX_ATTEMPTS)
            {
                Console.WriteLine(numberOfAttempts + "/10 :");
                Console.Write("Ente 4 chiffres entre 1 et 9 (ex: 1234) :");
                validInput = int.TryParse(Console.ReadLine(), out userNumber);

                foreach (char number in Convert.ToString(userNumber))
                {
                    userNumberList.Add(int.Parse(number.ToString()));
                }

                List<int> notFoundsNumbers = new List<int>(hiddenNumberList);

                if (validInput && userNumberList.Count == NUMBER_LENGTH)
                {
                    if (userNumberList.SequenceEqual(hiddenNumberList))
                    {
                        Console.WriteLine("Félicitations, le nombre " + userNumber + " est un numéro secret");
                        numberGuessed = true;
                    }

                    else
                    {
                        for (int i = 0; i < hiddenNumberList.Count; i++)
                        {
                            if (hiddenNumberList[i].Equals(userNumberList[i]))
                            {
                                correctPlace++;
                                notFoundsNumbers.Remove(hiddenNumberList[i]);
                            }
                        }

                        for (int j = 0; j < hiddenNumberList.Count; j++)
                        {
                            if (notFoundsNumbers.Contains(userNumberList[j]) && !hiddenNumberList[j].Equals(userNumberList[j]))
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
                    userNumberList.Clear();
                }

                else if (userNumberList.Count != NUMBER_LENGTH)
                {
                    Console.WriteLine("Tu n'as pas donné 4 chiffres ! essaie de nouveau");
                    userNumberList.Clear();
                }

                else
                {
                    Console.WriteLine("Tu n'as pas entré un nombre! essaie de nouveau");
                    userNumberList.Clear();
                }
            }
            Console.WriteLine();

            EndGame(numberOfAttempts, hiddenNumberList);


        }

        //Méthode qui finit le joue et peut le recommencer ou sortir de la programe
        static void EndGame(int numberOfAttempts, List<int> hiddenNumber)
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

        //Méthode qui genere un nombre aleatoire 
        static void RandomNumberGenerator(List<int> hiddenNumberList, int maxValue, bool allowDuplicates)
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

        static bool UserInputIsValid(int userNumber, List<int> userNumberList, int minNumber, int maxNumber, int numberLength)
        {
            bool validInput = false;
            validInput = int.TryParse(Console.ReadLine(), out userNumber);

            if (!validInput)
            {
                Console.WriteLine("Tu n'as pas entré un nombre! Essaie de nouveau");
                Console.WriteLine();
                return false;
            }

            foreach (char number in Convert.ToString(userNumber))
            {
                userNumberList.Add(int.Parse(number.ToString()));
            }

            foreach (int n in userNumberList)
            {
                if (n < minNumber || n > maxNumber)
                {
                    Console.WriteLine("Chiffres entre {0} et {1}", minNumber, maxNumber);
                    Console.WriteLine();
                    return false;
                }
            }

            if (userNumberList.Count != numberLength)
            {
                Console.WriteLine("Saisie invalide. Réessaie.");
                Console.WriteLine();
                return false;
            }

            return true;
        }

        static void ThereIsNotDouble(List <int> userNumberList, ref bool thereIsNotDouble)
        {
            for (int i = 0; i < userNumberList.Count - 1; i++)
            {
                for (int j = i + 1; j < userNumberList.Count; j++)
                {

                    if (userNumberList[i] == userNumberList[j])
                    {
                        Console.WriteLine("Pas de doublons autorisés à ce niveau.");
                        Console.WriteLine();
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
    }
}



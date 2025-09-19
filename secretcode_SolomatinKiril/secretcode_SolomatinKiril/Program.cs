using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
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

            Console.WriteLine("Votre choix (1-4) :");

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
            Console.ReadLine();
        }

        static void Intermediate()
        {
            List<int> hiddenNumberList = new List<int>();
            List<int> userNumberList = new List<int>();
            Random random = new Random();
            bool validInput = false;
            bool thereIsNotDouble = true;
            int userNumber;

            int numberOfAttempts = 1;

            
            while (hiddenNumberList.Count < 4)
            {
                int number = random.Next(1, 6);
                if (!(hiddenNumberList.Contains(number)))
                {
                    hiddenNumberList.Add(number);
                }
                
            }
            foreach (int number in hiddenNumberList)
            {
                Console.Write(number);
            }

            Console.WriteLine("=== SECRET CODE Niveau 2 ===\n");
            Console.WriteLine("Essais : \n\n");

            while(numberOfAttempts < 11)
            {
                Console.WriteLine("Essai {0}/10 : ", numberOfAttempts);
                Console.Write("Entre 4 chiffres entre 1 et 6 (ex: 1234) : ");
                validInput = int.TryParse(Console.ReadLine(), out userNumber);

                foreach (char n in Convert.ToString(userNumber))
                {
                    userNumberList.Add(n);  
                }

                if (userNumberList.Count == 4)
                {
                    for (int i = 0; i < userNumberList.Count-1; i++)
                    {
                        for(int j = 0; j < userNumberList.Count-1; j++)
                        {
                            if(userNumberList[i] == userNumberList[j])
                            {
                                thereIsNotDouble = false;
                                break;
                            }
                            else
                            {
                                thereIsNotDouble |= true;
                            }
                        }
                    }
                }

                if (thereIsNotDouble)
                {
                    Console.WriteLine("xxx");
                    numberOfAttempts++;
                }

                else if (!thereIsNotDouble)
                {
                    Console.WriteLine("Pas de doublons autorisés à ce niveau.");
                }
                    
            }
            

            Console.ReadLine();
        }

        static void Advanced()
        {
            Console.ReadLine();
        }

        static void Expert()
        {
            Console.Title = "Expert";
            Random random = new Random();

            List<int> userListNumber = new List<int>();
            List<int> hiddenNumber = new List<int>();

            int firstNumber = random.Next(1, 10);
            hiddenNumber.Add(firstNumber);
            int secondeNumber = random.Next(1, 10);
            hiddenNumber.Add(secondeNumber);
            int thirdNumber = random.Next(1, 10);
            hiddenNumber.Add(thirdNumber);
            int fourdNumber = random.Next(1, 10);
            hiddenNumber.Add(fourdNumber);

            int userNumber;
            bool validInput = false;
            bool numberGuessed = false;
            int numberOfAttempts = 10;
            int correctPlace = 0;
            int correctNumber = 0;

            foreach (int number in hiddenNumber)
            {
                Console.Write(number);
            }
            Console.WriteLine();
            
            Console.WriteLine("Essais: \n");

            while (!numberGuessed && numberOfAttempts<=10)
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
                    if (userListNumber.SequenceEqual(hiddenNumber))
                    {
                        Console.WriteLine("Félicitations, le nombre " + userNumber + " est un numéro secret");
                        numberGuessed = true;
                    }

                    else
                    {   
                        for (int i = 0; i < hiddenNumber.Count; i++)
                        {
                            if (hiddenNumber[i] == userListNumber[i])
                            {
                                correctPlace++;
                                findNumbers.Add(userListNumber[i]);
                            }
                        }

                        for (int j = 0; j < hiddenNumber.Count; j++)
                        {
                            if (hiddenNumber.Contains(userListNumber[j]) && !findNumbers.Contains(userListNumber[j]))
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
            if (numberOfAttempts > 10)
            {
                Console.ForegroundColor= ConsoleColor.Red;
                Console.Write("Perdu ! Le code était : ");
                Console.ResetColor();
                foreach (int number in hiddenNumber)
                {
                    Console.Write(number);
                    
                }
                Console.ResetColor();
                Console.WriteLine();

                

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
            }
        }
    }



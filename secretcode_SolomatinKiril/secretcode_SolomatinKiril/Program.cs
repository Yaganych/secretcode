using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace secretcode_SolomatinKiril
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Menu
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
                    if ( levelNumber == 1 )
                    {
                        Debutant();
                    }

                    if (levelNumber == 2)
                    {
                        Intermediate();
                    }

                    if (levelNumber == 3)
                    {
                        Advanced();
                    }

                    if (levelNumber == 4)
                    {
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
            Console.Clear();
            Console.WriteLine("=== SECRET CODE Niveau 1 ===");

            Random random = new Random();
            int hiddenNumber = random.Next(1234, 6544);
            int userNumber;
            bool validInput = false;
            bool numberGuessed = false;

            Console.WriteLine(hiddenNumber);

            while (!numberGuessed)
            {
                Console.Write("Ente 4 chiffres entre 1 et 6 (ex: 1234) : ");

                validInput = int.TryParse(Console.ReadLine(), out userNumber);

                if (validInput && userNumber != hiddenNumber)
                {
                    Console.WriteLine(userNumber + " Malheureusement, ce n'est pas un numéro secret");
                }
                
                else if (validInput && userNumber == hiddenNumber)
                {
                    Console.WriteLine("Félicitations, le nombre " + userNumber + " est un numéro secret");
                    numberGuessed = true;
                }

                else if (!validInput)
                {
                    Console.WriteLine("Tu n'as pas entré un nombre! essaie de nouveau");
                }

            }
            Console.ReadLine();
        }

        static void Intermediate()
        {
            Console.Clear();
            Console.WriteLine("=== SECRET CODE Niveau 2 ===");
            Console.ReadLine();
        }

        static void Advanced()
        {
            Console.Clear();
            Console.WriteLine("=== SECRET CODE Niveau 3 ===");
            Console.ReadLine();
        }

        static void Expert()
        {
            Console.Clear();
            Console.WriteLine("=== SECRET CODE Niveau 4 ===");
            Console.ReadLine();
        }
    }
}

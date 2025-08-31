using System;
using System.Collections.Generic;
using System.Linq;
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
            string levelNumber;

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
            Console.WriteLine("→ 0 bien placé(s), 3 mal placé(s)");
            Console.ResetColor();
            Console.WriteLine();

            Console.WriteLine("Appuie sur une touche pour commencer...  ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.ResetColor();

            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("=== SECRET CODE ===");
            Console.WriteLine();

            Console.WriteLine("Choisi un niveau :");
            Console.WriteLine("1. Débutant       (1 à 6, sans doublons, indices visibles)");
            Console.WriteLine("2. Intermédiaire  (1 à 6, sans doublons, indices discrets)");
            Console.WriteLine("3. Avancé         (1 à 8, avec doublons, indices visibles)");
            Console.WriteLine("4. Expert         (1 à 9, avec doublons, indices discrets)");
            Console.WriteLine();

            Console.WriteLine("Votre choix (1-4) :");

            levelNumber = Console.ReadLine();

            Console.WriteLine(levelNumber);

            Console.ReadLine();
        }
            
    }
}

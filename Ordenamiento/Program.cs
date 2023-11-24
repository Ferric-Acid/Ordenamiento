

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Ordenamiento
{
    internal class Program
    {
        public static void KeyContinue()
        {
            Console.WriteLine("\n[Presiona cualquier tecla para continuar.]\n");
            Console.ReadKey();
        }

        public static void Menu()
        {
            Console.Clear();
            Console.WriteLine("Bienvenido a OPTIMAL_ORDER. Elige la opción a la que desees acceder.\n" +
                "\n1. Ver algoritmos de ordenamiento\n2. Ver sobre la notación asintótica\n3. Modificar archivos de texto con listas\n4. Terminar programa");
            int choice = 0;

            // En todas las instancias en las que al usuario se le de una elección, habrá una estructura try-catch, para que regresar al menú más cercano en caso de que el usuario ingrese una opción en un formato inválido.
            try
            {
                choice = Convert.ToInt32(Console.ReadLine());
            }
            catch (System.FormatException)
            {
                Console.WriteLine("El formato de la entrada no es correcto, intenta introduce uno de los números especificados.");
                KeyContinue();
                Menu();
                throw;
            }
            
            switch (choice)
            {
                // Algoritmos de ordenamiento
                case 1:
                    Console.Clear();
                    Order.Choice();
                    break;

                // Información sobre notación asintótica
                case 2:
                    Console.Clear();
                    Notation.Not();
                    break;

                // Modificar archivo de texto de prueba
                case 3:
                    Console.Clear();
                    Text.Choice();
                    break;

                // Terminar programa
                case 4:
                    Console.Clear();
                    Environment.Exit(0);
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("Opción no válida, presiona cualquier tecla para regresar e intenta de nuevo.");
                    Console.ReadKey();
                    Console.Clear();
                    Menu();
                    break;
            }
        }
        
        static void Main(string[] args)
        {
            {
                Menu();
            }
        }
    }
}

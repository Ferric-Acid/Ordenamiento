

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
        public static void Menu()
        {
            Console.Clear();
            // Nombre preliminar 2: OPTIMAL_ORDER
            Console.WriteLine("Bienvenido a OPTIMAL_ORDER. Elige la opción a la que desees acceder.\n" +
                "\n1. Ver algoritmos de ordenamiento.\n2. Ver sobre la notación asintótica.\n3. Modificar archivo de texto de prueba." +
                "\n4. Ver créditos.\n5. Terminar programa.");

            int choice = Convert.ToInt32(Console.ReadLine());
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

                // Créditos
                case 4:

                    Console.Clear();
                    break;

                // Terminar programa
                case 5:
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

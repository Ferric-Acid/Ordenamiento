using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Ordenamiento
{
    internal class Order
    {
        static string user = "";
        static string list_route = "";
        static string list_file = "";
        static string list_name = "";

        public static void Choice()
        {

            void KeyContinue()
            {
                Console.WriteLine("\n[Presiona cualquier tecla para continuar.]\n");
                Console.ReadKey();
            }

            Console.WriteLine("Algoritmos de ordenamiento\nElige el algoritmo que desees revisar.\n\n1. Ordenamiento por burbuja" +
                "\n2. Ordenamiento por inserción.\n3. Ordenamiento Shell.\n4. Bogosort.\n5. Regresar al menú anterior.");

            void CallSortingAlgorithm(Func<float[], float[]> Fx_Sorting)
            {
                /* Pendientes:
                 * Llamar método de algoritmo de ordenamiento.
                 * Pedir si se desea ordenar una lista introducida directamente a la consola o que se consigan datos de un archivo de texto.
                 * Ordenar elementos introducidos directamente y mostrar arreglo ordenado.
                 * Ordenar elementos en un archivo de texto (se pueden leer los datos para introducirlos al arreglo, junto a su cantidad para crear el arreglo de tamaño definido).
                 * Contar tiempo para ordenamiento (almacena el tiempo al inicio de la operación y al final, obtén la diferencia entre los tiempos y muestra tal diferencia.)
                 */

                Console.WriteLine("\nIntenta ordenar una lista con este algoritmo, ¿deseas introducir la lista directamente o extraerla desde un archivo de texto?\n" +
                    "ADVERTENCIA: Sólo puedes usar números mayores o iguales que 0.\n" +
                    "1. Introducir lista directamente.\n2. Extraer lista de archivo de texto.");

                int choice_input = Convert.ToInt32(Console.ReadLine());
                DateTime beginning = DateTime.Now;
                DateTime ending = DateTime.Now;
                TimeSpan difference = ending - beginning;
                switch (choice_input)
                {
                    // Directamente
                    case 1:
                        Console.WriteLine("Excelente, primero introduce el número de elementos (número entero positivo) que deseas introducir.");
                        int amount = Convert.ToInt32(Console.ReadLine());
                        float[] list = new float[amount];
                        for (int i = 1; i <= amount; i++)
                        {
                            Console.WriteLine($"Introduce el elemento #{i}");
                            string input = Console.ReadLine();
                            float element = float.Parse(input);
                            list[i - 1] = element;
                        }

                        beginning = DateTime.Now;
                        float[] ordered = Fx_Sorting(list);
                        ending = DateTime.Now;
                        difference = ending - beginning;

                        Console.WriteLine("La lista ordenada es la siguiente: ");
                        Console.Write("[");
                        foreach (float floaty in ordered)
                        {
                            Console.Write(" " + floaty + " ");
                        }
                        Console.WriteLine("]");
                        break;

                    // Archivo de texto
                    case 2:
                        while (string.IsNullOrWhiteSpace(user))
                        {
                            Console.WriteLine("Antes de comenzar, ¿cuál es nombre de la carpeta de su usuario?");
                            user = Console.ReadLine().ToLower();
                        }

                        string direc = $"C:\\Users\\{user}\\Documents\\Sorting";
                        Text.Nonexistent_Empty_Directory(direc);

                        void Order_Read()
                        {
                            Console.WriteLine($"Dentro del directorio {direc} están los siguientes archivos:");
                            string[] files = Directory.GetFiles(direc);
                            foreach (string file in files)
                            {
                                Console.WriteLine(Path.GetFileName(file));
                            }
                            Console.WriteLine("¿Cuál archivo deseas ordenar? Introduce el nombre del archivo sin la extensión \".txt\".");
                            list_name = Console.ReadLine();
                            list_file = list_name + ".txt";
                            list_route = Path.Combine(direc, list_file);

                            if (!File.Exists(list_route))
                            {
                                Console.WriteLine("Tal archivo no existe, ingresa un nombre válido.");
                                Order_Read();
                            }
                        }
                        Order_Read();

                        string[] elements_as_string = File.ReadAllLines(list_route);
                        float[] unordered = new float[elements_as_string.Length];

                        for (int i = 0; i < unordered.Length; i++)
                        {
                            unordered[i] = float.Parse(elements_as_string[i]);
                        }

                        beginning = DateTime.Now;
                        float[] ordered_list = Fx_Sorting(unordered);
                        ending = DateTime.Now;
                        difference = ending - beginning;

                        string ordered_txt = list_name + "_ordered.txt";
                        string ordered_route = Path.Combine(direc, ordered_txt);

                        if (File.Exists(ordered_route))
                        {
                            File.WriteAllText(ordered_route, string.Empty);
                        }
                        else
                        {
                            File.Create(ordered_route).Close();
                        }
                        
                        using (StreamWriter writer = File.AppendText(ordered_route))
                        {
                            for (int i = 0; i < ordered_list.Length; i++)
                            {
                                writer.WriteLine(ordered_list[i]);
                            }
                        }
                        Console.WriteLine($"La lista ha sido ordenada, checa el archivo {ordered_route} en {direc}.");
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Opción no válida, presiona cualquier tecla para regresar e intenta de nuevo.");
                        Console.ReadKey();
                        Console.Clear();
                        CallSortingAlgorithm(Fx_Sorting);
                        break;
                }

                if (difference.Milliseconds < 5000)
                {
                    int little_difference = difference.Milliseconds;
                    Console.WriteLine($"Y el ordenamiento tomó {little_difference} milisegundos.");
                }
                else
                {
                    Console.WriteLine($"Y el ordenamiento tomó {difference}.");
                }

                KeyContinue();

                bool valid = false;
                while (!valid)
                {
                    Console.WriteLine("¿Qué deseas hacer ahora?\n\n1. Repetir el algoritmo de ordenamiento con " +
                        "otra lista.\n2. Regresar al menú anterior.");
                    sbyte repeat_return = Convert.ToSByte(Console.ReadLine());

                    switch (repeat_return)
                    {
                        case 1: CallSortingAlgorithm(Fx_Sorting); break;
                        case 2: Choice(); break;
                        default: Console.WriteLine("Opción no válida, elige una opción válida"); break;
                    }
                }
            }

            int choice = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            switch (choice)
            {
                // Burbuja
                case 1:
                    Console.WriteLine("El ordenamiento de burbuja (Bubble Sort) verifica si el primer par de elementos " +
                        "está en orden ascendente, de no ser así, invierte el orden de los elementos.\n " +
                        "Repetirá esto con todos los elementos consecuentes, por pares.\n\n" +
                        "Tiempos de ejecución\n◘ O(n^2) \n◘ ZETA(n^2) \n◘ OMEGA(n)");
                    KeyContinue();
                    Console.WriteLine("\nTomemos la siguiente lista como ejemplo:\n" +
                        "[\t10\t6\t8\t5\t3\t]\n" +
                        " \t^\t^\t \t \t \t \n" +
                        "Primero se comparan el primer y segundo elemento: 10 y 6. " +
                        "Considerando que para estar en orden, la lista debe estar en orden ascendente, " +
                        "los elementos no están en orden; así que deben cambiar de lugar.\n" +
                        "[\t6\t10\t8\t5\t3\t]\n");
                    KeyContinue();
                    Console.WriteLine("\nAhora, vamos con el segundo y tercer elemento:\n" +
                        "[\t6\t10\t8\t5\t3\t]\n" +
                        " \t \t^\t^\t \t \t \n" +
                        "10 es mayor que 8, así que cambian de lugar.\n" +
                        "[\t6\t8\t10\t5\t3\t]\n");
                    KeyContinue();
                    Console.WriteLine("\nVamos con los pares consecuentes.\n" +
                        "[\t6\t8\t10\t5\t3\t]\n" +
                        "\t \t \t^\t^\t \t \n" +
                        "[\t6\t8\t5\t10\t3\t]\n\n" +
                        "[\t6\t8\t5\t10\t3\t]\n" +
                        "\t \t \t \t^\t^\t \n" +
                        "[\t6\t8\t5\t3\t10\t]\n\n");
                    KeyContinue();
                    Console.WriteLine("Como se puede notar, el elemento mayor ha \"burbujeado\" al final de la lista. " +
                        "Dado que está en el lugar que le corresponde, ya no se debe comparar con más elementos, reduciendo la cantidad de iteraciones necesarias.\n" +
                        "[\t6\t8\t5\t3\t10\t]\n" +
                        "\t \t \t \t \tX\t \n\n" +
                        "Se vuelven a comparar los elementos restantes, cuando el segundo elemento mayor (8), llegue a su lugar, la cantidad de iteraciones necesarias " +
                        "se reducirá de nuevo.\n" +
                        "[\t6\t5\t3\t8\t10\t]\n" +
                        "\t \t \t \tX\tX\n\n");

                    CallSortingAlgorithm(Algor.Bubble);
                    KeyContinue();
                    break;

                // Inserción
                case 2:
                    Console.WriteLine("El ordenamiento por inserción (Insertion Sort) verifica si el primer par de elementos " +
                        "está ordenado, de ser así, sigue con el siguiente par, de tal manera que compara " +
                        "todos los elementos por pares [n] y [n+1]. Si encuentra que un par de elementos " +
                        "no está ordenado en orden ascendente, invierte el orden de los elementos y " +
                        "retrocede para comparar los elementos [n - 1] y [n]; si están invertidos, invierte " +
                        "el orden de los elementos y vuelve a retroceder, hasta que el orden de los elementos " +
                        "comparados sea el correcto.\n\n" +
                        "Tiempos de ejecución\n◘ O(n^2) \n◘ ZETA(n^2) \n◘ OMEGA(n)");
                    KeyContinue();
                    Console.WriteLine("\nTomemos la siguiente lista como ejemplo:\n" +
                        "[\t10\t6\t8\t5\t3\t]\n" +
                        " \t^\t^\t \t \t \t \n" +
                        "Primero se comparan el primer y segundo elemento: 10 y 6. " +
                        "Considerando que los elementos no están en orden, cambian de lugar.\n" +
                        "[\t6\t10\t8\t5\t3\t]\n");
                    KeyContinue();
                    Console.WriteLine("6 no tiene un elemento que le precede, así que no retrocede y se compara el siguiente par.\n" +
                        "[\t6\t10\t8\t5\t3\t]\n" +
                        " \t \t^\t^\t \t \t \n" +
                        "10 es mayor que 8, así que cambian de lugar; y se compara el par anterior, haciendo la inversión de lugares si es necesario.\n" +
                        "[\t6\t8\t10\t5\t3\t]\n" +
                        " \t^\t^\t \t \t \t \n" +
                        "Como 6 es menor que 8, se regresa a la posición de comparación original.\n" +
                        "[\t6\t8\t10\t5\t3\t]\n" +
                        " \t \t \t^\t^\t \t \n");
                    KeyContinue();
                    Console.WriteLine("5 es el menor que todos los elementos que le preceden, así que se " +
                        "realizarían varias comparaciones y retrocesos para mandar a 5 hasta el principio.\n" +
                        "[\t6\t8\t5\t10\t3\t]\n" +
                        " \t \t^\t^\t \t \t \n" +
                        "[\t6\t5\t8\t10\t3\t]\n" +
                        " \t^\t^\t \t \t \t \n" +
                        "[\t5\t6\t8\t10\t3\t]\n");
                    KeyContinue();
                    Console.WriteLine("Y lo mismo sucedería con 3, ya que es el elemento menor de la lista.\n" +
                        "[\t5\t6\t8\t10\t3\t]\n" +
                        " \t \t \t \t^\t^\t \n" +
                        "[\t5\t6\t8\t3\t10\t]\n" +
                        " \t \t \t^\t^\t \t \n" +
                        "[\t5\t6\t3\t8\t10\t]\n" +
                        " \t \t^\t^\t \t \t \n" +
                        "[\t5\t3\t6\t8\t10\t]\n" +
                        " \t^\t^\t \t \t \t \n" +
                        "[\t3\t5\t6\t8\t10\t]\n");
                    KeyContinue();
                    Console.WriteLine("A pesar de que tenga la misma complejidad temporal que el ordenamiento por burbuja " +
                        "es ligeramente más eficiente por este mecanismo de retroceso para comparar elementos, reduciendo la cantidad necesaria de comparaciones.");
                    CallSortingAlgorithm(Algor.Insertion);
                    KeyContinue();
                    break;

                // Shell
                case 3:
                    Console.WriteLine("El ordenamiento Shell (Shell Sort) es muy similar al ordenamiento por " +
                        "inserción; el proceso de comparación es idéntico al ordenamiento por inserción, " +
                        "pero las comparaciones se hacen en intervalos mayores a uno, " +
                        "el intervalo entre elementos va reduciendo hasta llegar a 1; " +
                        "lo que equivale a un ordenamiento por inserción, pero será más rápido ya que " +
                        "la lista está parcialmente ordenada.\n\n" +
                        "Tiempos de ejecución\n◘ O(n^2) \n◘ ZETA(n*log n) \n◘ OMEGA(n*log n)");
                    KeyContinue();
                    Console.WriteLine("Existen varias maneras de determinar los intervalos para el ordenamiento. " +
                        "En la definición original del ordenamiento Shell se maneja la siguiente función: f(x) = floor(x/2^n). " +
                        "Donde x es el número de elementos en la lista; n inicia en 1 y es el exponente que aumentará por 1 por cada iteración, hasta que el f(x) = 1.\n" +
                        "Nota: floor(a) representa la función piso, si a es un número no entero, este se redondeará al entero menor.");
                    KeyContinue();
                    Console.WriteLine("\nTomemos la siguiente lista como ejemplo:\n" +
                        "[\t10\t6\t8\t5\t3\t]\n" +
                        "Hay 5 elementos en esta lista, así que intervalo será de 2 elementos.\n" +
                        "floor(5/2^1) = floor(2.5) = 2\n" +
                        "[\t10\t6\t8\t5\t3\t]\n" +
                        " \t^\t \t^\t \t \t \n" +
                        "10 es mayor que 8, así que cambian de lugar.\n" +
                        "[\t8\t6\t10\t5\t3\t]\n");
                    KeyContinue();
                    Console.WriteLine("Como 8 no tiene elementos detrás, se continua con el siguiente par.\n" +
                        "[\t8\t6\t10\t5\t3\t]\n" +
                        "\t \t \t^\t \t^\t \n" +
                        "10 es mayor que 3, así que cambian de lugar y se retrocede al intervalo anterior, para realizar los cambios necesarios.\n" +
                        "[\t8\t6\t3\t5\t10\t]\n" +
                        "\t^\t \t^\t \t \t \n" +
                        "[\t3\t6\t8\t5\t10\t]\n");
                    KeyContinue();
                    Console.WriteLine("Dado que ya no hay más elementos más allá del 10, una iteración ha finalizado y el intervalo debe modificarse.\n" +
                        "Ahora, a n se le suma 1. floor(5/2^2) = floor(1.25) = 1\n" +
                        "Dado que el intervalo es 1, es un ordenamiento de inserción normal, pero los elementos ya están más cerca de su lugar, " +
                        "incluso 3 y 10 están en el lugar correcto.\n" +
                        "[\t3\t6\t8\t5\t10\t]\n" +
                        "\t^\t^\t \t \t \t \n" +
                        "[\t3\t6\t8\t5\t10\t]\n" +
                        "\t \t^\t^\t \t \t \n" +
                        "[\t3\t6\t8\t5\t10\t]\n" +
                        "\t \t \t^\t^\t \t \n" +
                        "[\t3\t6\t5\t8\t10\t]\n" +
                        "\t \t^\t^\t \t \t \n" +
                        "[\t3\t5\t6\t8\t10\t]\n" +
                        "\t^\t^\t \t \t \t \n" +
                        "[\t3\t5\t6\t8\t10\t]\n" +
                        "\t \t \t \t^\t ^\t \n");
                    KeyContinue();
                    Console.WriteLine("Como se puede ver en este caso, el ordenamiento Shell es más eficiente " +
                        "con esta lista que el ordenamiento por inserción. Esto demuestra que tiende a ser más eficiente, " +
                        "pero no siempre se da el caso.");
                    CallSortingAlgorithm(Algor.Shell);
                    KeyContinue();
                    break;

                // Bogosort
                case 4:
                    Console.WriteLine("Se suele decir que Bogosort, también conocido como Slowsort y Stupid sort, fue creado como un chiste, " +
                        "ya que este es un algoritmo de ordenamiento que suele ser muy ineficiente.\n\n" +
                        "Tiempos de ejecución\n◘ O(infinito) \n◘ ZETA(n*n!) \n◘ OMEGA(n)");
                    KeyContinue();
                    Console.WriteLine("\nTomemos la siguiente lista como ejemplo:\n" +
                        "[\t6\t10\t8\t5\t3\t]\n" +
                        "Primero verifica si la lista está en orden, por pares de elementos.\n" +
                        "[\t6\t10\t8\t5\t3\t] [O]\n" +
                        "\t^\t^\t \t \t \t \n" +
                        "[\t6\t10\t8\t5\t3\t] [X]\n" +
                        "\t \t^\t^\t \t \t \n");
                    KeyContinue();
                    Console.WriteLine("Si los elementos no están ordenados, la posición de cada elemento de la lista " +
                        "se cambia al azar. Y se repite el proceso hasta que la lista esté ordenada.\n" +
                        "[\t10\t5\t3\t6\t8\t] [X]\n" +
                        "\t^\t^\t \t \t \t \n" +
                        "[\t6\t10\t3\t5\t8\t] [X]\n" +
                        "\t^\t^\t \t \t \t \n" +
                        "[\t3\t5\t6\t8\t10\t] [O]\n");
                    KeyContinue();
                    Console.WriteLine("Este algoritmo de ordenamiento puede ordenar los elementos después de unas pocas " +
                        "iteraciones, como en el ejemplo; pero, debido a que se basa en la probabilidad para ordenarse, en el " +
                        "peor de los casos puede tardarse una cantidad de tiempo infinita para ordenarse.\n\n" +
                        "ADVERTENCIA: Se recomienda no usar muchos elementos con este algoritmo, debido a que puede ser muy tardado.");
                    CallSortingAlgorithm(Algor.Bogo);
                    KeyContinue();
                    break;

                // Regresar
                case 5:
                    Console.Clear();
                    Program.Menu();
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("Opción no válida, presiona cualquier tecla para regresar e intenta de nuevo.");
                    Console.ReadKey();
                    Console.Clear();
                    Choice();
                    break;
            }
        }
    }
}
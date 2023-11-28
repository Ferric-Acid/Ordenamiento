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

        public static void Choice()
        {
            Console.Clear();
            Console.WriteLine("Algoritmos de ordenamiento\nElige el algoritmo que desees revisar.\n\n1. Ordenamiento por burbuja\n2. Ordenamiento por inserción\n3. Ordenamiento Shell\n4. Bogo sort\n5. Regresar al menú anterior.");

            int first_choice = 0;

            try
            {
                first_choice = Convert.ToInt32(Console.ReadLine());
            }
            catch (System.FormatException)
            {
                Console.WriteLine("El formato de la entrada no es correcto, intenta introduce uno de los números especificados.");
                Program.KeyContinue();
                Choice();
                throw;
            }

            float choice = (float)first_choice;

            // Para regresar al menú
            if (first_choice == 5)
            {
                Console.Clear();
                Program.Menu();
            } else if (first_choice < 1 || first_choice > 5)
            {
                Console.Clear();
                Console.WriteLine("Opción no válida, presiona cualquier tecla para regresar e intenta de nuevo.");
                Console.ReadKey();
                Console.Clear();
                Choice();
            }

            void Explanation_Execution()
            {
                Console.WriteLine("¿Deseas ver la explicación del algoritmo o ejecutarlo?\n\n1. Ver explicación\n2. Proceder a ejecución");
                int exp_exe = 0;

                try
                {
                    exp_exe = Convert.ToInt32(Console.ReadLine());
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("El formato de la entrada no es correcto, intenta introduce uno de los números especificados.");
                    Program.KeyContinue();
                    Console.Clear();
                    Explanation_Execution();
                    throw;
                }

                switch (exp_exe)
                {
                    case 1:
                        break;
                    case 2:
                        // En caso de buscar la ejecución directa, sólo se podrá acceder a casos que terminan con .5f, que son las opciones de ejecución directa.
                        choice += 0.5f;
                        break;
                    default:
                        Console.WriteLine("Opción no válida, presiona cualquier tecla para regresar e intenta de nuevo.");
                        Console.ReadKey();
                        Console.Clear();
                        Explanation_Execution();
                        break;
                }
            }

            Explanation_Execution();
            Console.Clear();

            // Fx_Sorting requiere un arreglo de flotantes como entrada y da un arreglo de flotantes como salida.
            void CallSortingAlgorithm(Func<float[], float[]> Fx_Sorting)
            {

                Console.WriteLine("\nIntenta ordenar una lista con este algoritmo, ¿deseas introducir la lista directamente o extraerla desde un archivo de texto?\n\n" +
                    "1. Introducir lista directamente.\n2. Extraer lista de archivo de texto.");

                int choice_input = 0;

                try
                {
                    choice_input = Convert.ToInt32(Console.ReadLine());
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("El formato de la entrada no es correcto, intenta introduce uno de los números especificados.");
                    Program.KeyContinue();
                    Console.Clear();
                    CallSortingAlgorithm(Fx_Sorting);
                    throw;
                }

                DateTime beginning = DateTime.Now;
                DateTime ending = DateTime.Now;
                TimeSpan difference = ending - beginning;
                switch (choice_input)
                {
                    // Introducción de arreglo directamente la consola.
                    case 1:
                        int amount = 0;
                        void Input()
                        {
                            Console.WriteLine("Excelente, primero introduce el número de elementos (número entero positivo) que deseas introducir.");

                            try
                            {
                                amount = Convert.ToInt32(Console.ReadLine());
                            }
                            catch (System.FormatException)
                            {
                                Console.WriteLine("El formato de la entrada no es correcto, introduce un número entero.");
                                Program.KeyContinue();
                                Input();
                                //throw;
                            }

                            if (amount < 1)
                            {
                                Console.WriteLine("Cantidad de elementos inválida, introduce un número válido.");
                                Input();
                            }
                        }
                        Input();

                        float[] list = new float[amount];
                        for (int i = 1; i <= amount; i++)
                        {
                            Console.WriteLine($"Introduce el elemento #{i}");
                            string input = Console.ReadLine();
                            float element = 0;

                            try
                            {
                                element = float.Parse(input);
                            }
                            catch (System.FormatException)
                            {
                                Console.WriteLine("La entrada no está en un formato válido, introduce un número.");
                                // Si la entrada no está en el formato válido, no se guardará tal entrada, se tendrá que volver a introducir.
                                i--;
                                continue;
                                throw;
                            }
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
                        // Siempre que se ejecute el programa, se le pedirá el nombre de la carpeta del usuario, pero sólo una vez.
                        while (string.IsNullOrWhiteSpace(user))
                        {
                            Console.WriteLine("Antes de comenzar, ¿cuál es nombre de la carpeta de su usuario?");
                            user = Console.ReadLine().ToLower();
                        }

                        string direc = $"C:\\Users\\{user}\\Documents\\Sorting";
                        // Primero se verifica si directorio no existe o si está vacío, si tal es el caso, se redirigirá a Text.cs para crear el directorio o un archivo para contener los datos.
                        Text.Nonexistent_Empty_Directory(direc);

                        string list_name = "";
                        string list_file = "";
                        string list_route = "";

                        void Order_Read()
                        {
                            Console.WriteLine($"Dentro del directorio {direc} están los siguientes archivos:");
                            // Muestra los archivos existentes en el directorio.
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

                        // Se leen los elementos, separados entre líneas, y se almacenan en un arreglo de cadenas de texto.
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

                        // La lista de elementos ordenada se almacenará en un archivo con nombre similar al del original, pero con "_ordered.txt" al final del nombre original.
                        string ordered_txt = list_name + "_ordered.txt";
                        string ordered_route = Path.Combine(direc, ordered_txt);

                        // Si hay un archivo con el mismo nombre del anterior, se formateará; de lo contrario, se creará tal archivo.
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
                        Console.WriteLine($"La lista ha sido ordenada, checa el archivo {ordered_txt} en {direc}.");
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Opción no válida, presiona cualquier tecla para regresar e intenta de nuevo.");
                        Console.ReadKey();
                        Console.Clear();
                        CallSortingAlgorithm(Fx_Sorting);
                        break;
                }

                // Si el ordenamiento tomó menos de 5 segundos, se mostrará cuánto tomó en milisegundos; de lo contrario se mostrará en el formato hh:mm:ss.
                if (difference.Milliseconds < 5000)
                {
                    int little_difference = difference.Milliseconds;
                    Console.WriteLine($"Y el ordenamiento tomó {little_difference} milisegundos.");
                }
                else
                {
                    Console.WriteLine($"Y el ordenamiento tomó {difference}.");
                }

            }

            switch (choice)
            {
                // Ordenamiento de Burbuja (explicación + ejecución)
                case 1f:
                    Console.WriteLine("El ordenamiento de burbuja (Bubble Sort) verifica si el primer par de elementos está en orden ascendente, de no ser así, invierte el orden de los elementos.\nRepetirá esto con todos los elementos consecuentes, por pares.\n\nTiempos de ejecución\n◘ O(n^2) \n◘ ZETA(n^2) \n◘ OMEGA(n)");
                    Program.KeyContinue();
                    Console.WriteLine("\nTomemos la siguiente lista como ejemplo:\n" +
                        "[\t10\t6\t8\t5\t3\t]\n" +
                        " \t^\t^\t \t \t \t \n" +
                        "Primero se comparan el primer y segundo elemento: 10 y 6. Considerando que para estar en orden, la lista debe estar en orden ascendente, los elementos no están en orden; así que deben cambiar de lugar.\n" +
                        "[\t6\t10\t8\t5\t3\t]\n");
                    Program.KeyContinue();
                    Console.WriteLine("\nAhora, vamos con el segundo y tercer elemento:\n" +
                        "[\t6\t10\t8\t5\t3\t]\n" +
                        " \t \t^\t^\t \t \t \n" +
                        "10 es mayor que 8, así que cambian de lugar.\n" +
                        "[\t6\t8\t10\t5\t3\t]\n");
                    Program.KeyContinue();
                    Console.WriteLine("\nVamos con los pares consecuentes.\n" +
                        "[\t6\t8\t10\t5\t3\t]\n" +
                        "\t \t \t^\t^\t \t \n" +
                        "[\t6\t8\t5\t10\t3\t]\n\n" +
                        "[\t6\t8\t5\t10\t3\t]\n" +
                        "\t \t \t \t^\t^\t \n" +
                        "[\t6\t8\t5\t3\t10\t]\n\n");
                    Program.KeyContinue();
                    Console.WriteLine("Como se puede notar, el elemento mayor ha \"burbujeado\" al final de la lista. Dado que está en el lugar que le corresponde, ya no se debe comparar con más elementos, reduciendo la cantidad de iteraciones necesarias.\n" +
                        "[\t6\t8\t5\t3\t10\t]\n" +
                        "\t \t \t \t \tX\t \n\n" +
                        "Se vuelven a comparar los elementos restantes, cuando el segundo elemento mayor (8), llegue a su lugar, la cantidad de iteraciones necesarias se reducirá de nuevo.\n" +
                        "[\t6\t5\t3\t8\t10\t]\n" +
                        "\t \t \t \tX\tX\n\n");

                    CallSortingAlgorithm(Algor.Bubble);
                    break;

                // Ordenamiento de Burbuja (ejecución directa)
                case 1.5f:
                    CallSortingAlgorithm(Algor.Bubble);
                    break;

                // Ordenamiento por Inserción (explicación + ejecución)
                case 2f:
                    Console.WriteLine("El ordenamiento por inserción (Insertion Sort) verifica si el primer par de elementos está ordenado, de ser así, sigue con el siguiente par, de tal manera que compara todos los elementos por pares [n] y [n+1]. Si encuentra que un par de elementos no está ordenado en orden ascendente, invierte el orden de los elementos y retrocede para comparar los elementos [n - 1] y [n]; si están invertidos, invierte el orden de los elementos y vuelve a retroceder, hasta que el orden de los elementos comparados sea el correcto.\n\nTiempos de ejecución\n◘ O(n^2) \n◘ ZETA(n^2) \n◘ OMEGA(n)");
                    Program.KeyContinue();
                    Console.WriteLine("\nTomemos la siguiente lista como ejemplo:\n" +
                        "[\t10\t6\t8\t5\t3\t]\n" +
                        " \t^\t^\t \t \t \t \n" +
                        "Primero se comparan el primer y segundo elemento: 10 y 6. Considerando que los elementos no están en orden, cambian de lugar.\n" +
                        "[\t6\t10\t8\t5\t3\t]\n");
                    Program.KeyContinue();
                    Console.WriteLine("6 no tiene un elemento que le precede, así que no retrocede y se compara el siguiente par.\n" +
                        "[\t6\t10\t8\t5\t3\t]\n" +
                        " \t \t^\t^\t \t \t \n" +
                        "10 es mayor que 8, así que cambian de lugar; y se compara el par anterior, haciendo la inversión de lugares si es necesario.\n" +
                        "[\t6\t8\t10\t5\t3\t]\n" +
                        " \t^\t^\t \t \t \t \n" +
                        "Como 6 es menor que 8, se regresa a la posición de comparación original.\n" +
                        "[\t6\t8\t10\t5\t3\t]\n" +
                        " \t \t \t^\t^\t \t \n");
                    Program.KeyContinue();
                    Console.WriteLine("5 es el menor que todos los elementos que le preceden, así que se realizarían varias comparaciones y retrocesos para mandar a 5 hasta el principio.\n" +
                        "[\t6\t8\t5\t10\t3\t]\n" +
                        " \t \t^\t^\t \t \t \n" +
                        "[\t6\t5\t8\t10\t3\t]\n" +
                        " \t^\t^\t \t \t \t \n" +
                        "[\t5\t6\t8\t10\t3\t]\n");
                    Program.KeyContinue();
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
                    Program.KeyContinue();
                    Console.WriteLine("A pesar de que tenga la misma complejidad temporal que el ordenamiento por burbuja es ligeramente más eficiente por este mecanismo de retroceso para comparar elementos, reduciendo la cantidad necesaria de comparaciones.");
                    Console.WriteLine("ADVERTENCIA: Para este algoritmo sólo puedes usar números mayores o iguales a 0.");
                    CallSortingAlgorithm(Algor.Insertion);
                    break;

                // Ordenamiento por Inserción (ejecución directa)
                case 2.5f:
                    Console.WriteLine("ADVERTENCIA: Para este algoritmo sólo puedes usar números mayores o iguales a 0.");
                    CallSortingAlgorithm(Algor.Insertion);
                    break;

                // Ordenamiento Shell (explicación + ejecución)
                case 3f:
                    Console.WriteLine("El ordenamiento Shell (Shell Sort) es muy similar al ordenamiento por inserción; el proceso de comparación es idéntico al ordenamiento por inserción, pero las comparaciones se hacen en intervalos mayores a uno, el intervalo entre elementos va reduciendo hasta llegar a 1; lo que equivale a un ordenamiento por inserción, pero será más rápido ya que la lista está parcialmente ordenada.\n\nTiempos de ejecución\n◘ O(n^2) \n◘ ZETA(n*log n) \n◘ OMEGA(n*log n)");
                    Program.KeyContinue();
                    Console.WriteLine("Existen varias maneras de determinar los intervalos para el ordenamiento. En la definición original del ordenamiento Shell se maneja la siguiente función: f(x) = floor(x/2^n). Donde x es el número de elementos en la lista; n inicia en 1 y es el exponente que aumentará por 1 por cada iteración, hasta que el f(x) = 1.\n Nota: floor(a) representa la función piso, si a es un número no entero, este se redondeará al entero menor.");
                    Program.KeyContinue();
                    Console.WriteLine("\nTomemos la siguiente lista como ejemplo:\n" +
                        "[\t10\t6\t8\t5\t3\t]\n" +
                        "Hay 5 elementos en esta lista, así que intervalo será de 2 elementos.\n" +
                        "floor(5/2^1) = floor(2.5) = 2\n" +
                        "[\t10\t6\t8\t5\t3\t]\n" +
                        " \t^\t \t^\t \t \t \n" +
                        "10 es mayor que 8, así que cambian de lugar.\n" +
                        "[\t8\t6\t10\t5\t3\t]\n");
                    Program.KeyContinue();
                    Console.WriteLine("Como 8 no tiene elementos detrás, se continua con el siguiente par.\n" +
                        "[\t8\t6\t10\t5\t3\t]\n" +
                        "\t \t \t^\t \t^\t \n" +
                        "10 es mayor que 3, así que cambian de lugar y se retrocede al intervalo anterior, para realizar los cambios necesarios.\n" +
                        "[\t8\t6\t3\t5\t10\t]\n" +
                        "\t^\t \t^\t \t \t \n" +
                        "[\t3\t6\t8\t5\t10\t]\n");
                    Program.KeyContinue();
                    Console.WriteLine("Dado que ya no hay más elementos más allá del 10, una iteración ha finalizado y el intervalo debe modificarse.\nAhora, a n se le suma 1. floor(5/2^2) = floor(1.25) = 1\nDado que el intervalo es 1, es un ordenamiento de inserción normal, pero los elementos ya están más cerca de su lugar, incluso 3 y 10 están en el lugar correcto.\n" +
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
                    Program.KeyContinue();
                    Console.WriteLine("Como se puede ver en este caso, el ordenamiento Shell es más eficiente con esta lista que el ordenamiento por inserción. Esto demuestra que tiende a ser más eficiente, pero no siempre se da el caso.");
                    Console.WriteLine("ADVERTENCIA: Para este algoritmo sólo puedes usar números mayores o iguales a 0.");
                    CallSortingAlgorithm(Algor.Shell);
                    break;

                // Ordenamiento Shell (ejecución directa)
                case 3.5f:
                    Console.WriteLine("ADVERTENCIA: Para este algoritmo sólo puedes usar números mayores o iguales a 0.");
                    CallSortingAlgorithm(Algor.Shell);
                    break;

                // Bogo sort (explicación + ejecución)
                case 4f:
                    Console.WriteLine("Se suele decir que Bogo sort, también conocido como Slow sort y Stupid sort, fue creado como un chiste, ya que este es un algoritmo de ordenamiento que suele ser muy ineficiente.\n\nTiempos de ejecución\n◘ O(infinito) \n◘ ZETA(n*n!) \n◘ OMEGA(n)");
                    Program.KeyContinue();
                    Console.WriteLine("\nTomemos la siguiente lista como ejemplo:\n" +
                        "[\t6\t10\t8\t5\t3\t]\n" +
                        "Primero verifica si la lista está en orden, por pares de elementos.\n" +
                        "[\t6\t10\t8\t5\t3\t] [O]\n" +
                        "\t^\t^\t \t \t \t \n" +
                        "[\t6\t10\t8\t5\t3\t] [X]\n" +
                        "\t \t^\t^\t \t \t \n");
                    Program.KeyContinue();
                    Console.WriteLine("Si los elementos no están ordenados, la posición de cada elemento de la lista " +
                        "se cambia al azar. Y se repite el proceso hasta que la lista esté ordenada.\n" +
                        "[\t10\t5\t3\t6\t8\t] [X]\n" +
                        "\t^\t^\t \t \t \t \n" +
                        "[\t6\t10\t3\t5\t8\t] [X]\n" +
                        "\t^\t^\t \t \t \t \n" +
                        "[\t3\t5\t6\t8\t10\t] [O]\n");
                    Program.KeyContinue();
                    Console.WriteLine("Este algoritmo de ordenamiento puede ordenar los elementos después de unas pocas iteraciones, como en el ejemplo; pero, debido a que se basa en la probabilidad para ordenarse, en el peor de los casos puede tardarse una cantidad de tiempo infinita para ordenarse.\n\nADVERTENCIA: Se recomienda no usar muchos elementos con este algoritmo, debido a que puede ser muy tardado.");
                    CallSortingAlgorithm(Algor.Bogo);
                    break;

                // Bogo sort (ejecución directa)
                case 4.5f:
                    CallSortingAlgorithm(Algor.Bogo);
                    break;

                // Opción inválida
                default:
                    Console.Clear();
                    Console.WriteLine("Opción no válida, presiona cualquier tecla para regresar e intenta de nuevo.");
                    break;
            }

            Console.WriteLine("Vas a regresar al menú anterior.");
            Program.KeyContinue();
            Console.Clear();
            Choice();
        }
    }
}
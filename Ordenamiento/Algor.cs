using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Ordenamiento
{
    internal class Algor
    {
        // Nota: Para que una lista se considere ordenada, debe estar organizada en orden ascendente.
        public static float[] Insertion(float[] list)
        {
            float[] ordered = new float[list.Length];
            float[] process = new float[list.Length + 1];

            for (int i = 0; i < list.Length; i++)
            {
                process[i + 1] = list[i]; // Tendrá un espacio vacío al inicio.
            }

            /* El ordenamiento por inserción verifica si los elementos process[1] y process[2] están ordenados de ser así, sigue con process[2] y process[3], de tal manera que compara los elementos process[n] y process[n + 1] con todos los elementos. 
             * Si encuentra que un par de elementos process[n] y process[n + 1] no está ordenado en orden ascendente, invierte el orden de los elementos y retrocede para comparar los elementos process[n - 1] y process[n], si su orden no es el correcto, invierte el orden de los elementos y vuelve a retroceder, hasta que el orden de los elementos sea el correcto.
             */

            float contained1 = 0;
            float contained2 = 0;
            int minus = 1;
            for (int key = 2; key <= list.Length; key++)
            {
                minus = 1;
                // Iniciando en los elementos de índice 1 y 2, se comparan todos los elementos no vacíos de la lista.
                while (process[key - minus + 1] < process[key - minus])
                {
                    // Si un par de ellos no está ordenado, el orden de los elementos será invertido.
                    contained1 = process[key - minus + 1];
                    contained2 = process[key - minus];
                    process[key - minus + 1] = contained2;
                    process[key - minus] = contained1;
                    // Y se verificará el par anterior, hasta llegar a process[0] y process[1], en tal caso, no se repetirá ya que process[0] siempre será 0.
                    // Por lo que si se introduce al menos un número negativo, el número negativo menor no aparecerá, en cambio, aparecerá un 0.
                    minus++;
                }
            }
            

            for (int i = 1; i < process.Length; i++)
            {
                // Una vez que se hayan ordenado todos los elementos, se pondrán en un arreglo nuevo, que no dará el primer elemento, que es un 0 adicional.
                ordered[i - 1] = process[i];
            }

            return ordered;
        }

        public static float[] Bubble(float[] list)
        {
            float[] process = new float[list.Length];

            for (int i = 0; i < list.Length; i++)
            {
                process[i] = list[i];
            }

            /* El ordenamiento de burbuja verifica si el par de elementos process[0] y process[1] está en orden ascendente, de no ser así, invierte el orden de los elementos.
             Repetirá esto con todos los elementos process[n] y process[n + 1]. Al final de una iteración, el elemento mayor estará al final de la lista. Así que al final de la
            segunda iteración, no se deberían comparar los dos últimos elementos, ya que son el segundo elemento mayor y el elemento mayor respectivamente, así que por cada iteración,
            la cantidad necesaria de comparaciones reducirá por 1.
             */

            float contained1 = 0;
            float contained2 = 0;
            int needed_iterations = list.Length;

            for (int i = 0; i < list.Length; i++)
            {
                // La cantidad inicial de iteraciones necesarias es igual a la longitud de list.
                // Pero para el bucle, considerando process[n] y process[n + 1], dado que la referencia es el process[n] el límite es needed_iterations - 1 para evitar desbordamiento del arreglo.
                for (int b = 0; b < (needed_iterations - 1); b++)
                {
                    // Si el elemento con la posición menor es mayor que el elemento con la posición mayor, ambos cambian de luugar.
                    if (process[b] > process[b + 1])
                    {
                        contained1 = process[b];
                        contained2 = process[b + 1];
                        process[b] = contained2;
                        process[b + 1] = contained1;
                    }
                }
                // Al final de una iteración, los elementos mayores estarán en las posiciones correctas; por ejemplo, al final de la primera iteración, el elemento mayor estará al final del arreglo, y al final de la segunda iteración, el segundo elemento mayor estará en una posición anterior a la final.
                // Para optimizar el algoritmo, se reduce el límite para ya no checar estos elementos.
                needed_iterations--;
            }

            return process;
        }

        public static float[] Bogo(float[] list)
        {
            void Shuffle(float[] sample)
            {
                // Este es el algoritmo usado para hacer que el orden de los elementos sea aleatorio, específicamente, el algoritmo Fisher-Yates.
                int position = 0;
                float r_swapped1 = 0f;
                float r_swapped2 = 0f;
                Random pos_random = new Random();

                // Al final de una iteración, el elemento sample[i - 1], y todos los que le preceden, ya no serán considerados para el siguiente cambio de lugar.
                for (int i = 0; i < sample.Length; i++)
                {
                    // Las posiciones usadas para realizar cambios en el orden son i y una determinada por la clase Random entre i y la longitud del arreglo.
                    position = pos_random.Next(i, sample.Length);
                    r_swapped1 = sample[i];
                    r_swapped2 = sample[position];
                    sample[i] = r_swapped2;
                    sample[position] = r_swapped1;
                }
            }

            bool isSorted(float[] sample)
            {
                for (int i = 0; i < sample.Length - 1; ++i)
                {
                    if (sample[i] > sample[i + 1])
                    {
                        return false; // Si en algún momento se detecta que los elementos no están ordenados, se interrumpe el proceso para hacer una permutación nueva de la lista.
                    }
                }
                return true;
            }

            while (isSorted(list) == false)
            {
                Shuffle(list); // Se verificará si la lista está ordenada, de no estarlo, se creará una permutación al azar de la lista.
            }
            return list;
        }

        public static float[] Shell(float[] list)
        {
            /* El ordenamiento Shell es muy similar al ordenamiento por inserción.
             * El proceso de verificación de orden es idéntico, pero las comparaciones se hacen en intervalos mayores a uno.
             * El intervalo entre elementos va reduciendo hasta llegar a 1.
             * Cuando el intervalo entre elementos sea 1, sólo se hace un ordenamiento por inserción, pero será más rápido ya que la lista está parcialmente ordenada.
             * Los intervalos se determinarán en la definición original del ordenamiento Shell: f(x) = ⌊x/2^n⌋.
             * Donde x = Número de elementos en la lista; y n = Exponente, aumentará por 1 por cada iteración, hasta que el f(x) = 1.
             */

            int extra = list.Length / 2 + 1;

            float[] process = new float[list.Length + extra];

            for (int i = 0; i < list.Length; i++)
            {
                // Debido a que funciona como Insertion, tendrá varios elementos vacíos al inicio.
                // Tal cantidad es igual a la mitad redondeada de la cantidad de elementos, más 1.
                process[i + extra] = list[i];
            }
            
            float contained1 = 0;
            float contained2 = 0;
            int minus = 0;
            int exponent = 1;
            // El método funciona como Insertion, pero el intervalo entre elementos cambiados de lugar se define por la función previamente mencionada: f(x) = ⌊x/2^n⌋, donde n inicia como 1.
            int interval = list.Length / (int)(Math.Pow(2, exponent));

            // Cuando n haga que ⌊x/2^n⌋ < 1, el ordenamiento habrá terminado.
            while (interval > 0) 
            {
                for (int key = extra; key < process.Length; key += interval)
                {
                    minus = 0;
                    // Se compararán elementos por el intervalo actual.
                    while (process[key - minus] < process[key - interval - minus])
                    {
                        contained1 = process[key - minus];
                        contained2 = process[key - interval - minus];
                        process[key - minus] = contained2;
                        process[key - interval - minus] = contained1;
                        // En caso que se deba hacer un cambio de posición de elementos, la comparación se va a recorrer por el intervalo especificado. Los elementos vacíos al inicio de process sirven como límites de seguridad.
                        minus += interval;
                    }
                }
                // Tomando en cuenta que el intervalo es f(x) = ⌊x/2^n⌋, a n se le sumará 1. Esto se repetirá hasta que f(x) (o interval), sea 1.
                interval = list.Length / (int)(Math.Pow(2, ++exponent));
            }

            float[] ordered = new float[list.Length];
            for (int i = extra; i < process.Length; i++)
            {
                // Al final, los elementos ordenados son asignados a ordered, sin incluir los elementos de límite de seguridad.
                ordered[i - extra] = process[i];
            }

            return ordered;
        }


        // Método no funcional
        /*
        public static float[] Merge(float[] list)
        {
            int columns = 0;
            int rows = 0;
            while (columns < list.Length)
            {
                rows++;
                columns = (int)Math.Pow(2, rows);
                //* El número de filas será el exponente que haga una potencia de 2 mayor o 
                //* igual al número de columnas, además de una fila adicional.
                //* y el número de columnas es la potencia resultante.
            }
            rows++;
            string[,] process = new string[rows, columns];
            for (int y = 0; y < list.Length; y++)
            {
                string elem = Convert.ToString(list[y]);
                process[0, y] = elem;
            }

            for (int xd = 1; xd < process.GetLength(0); xd++)
            {
                for (int yd = 0; yd < process.GetLength(1); yd++)
                {
                    process[xd, yd] = process[0, yd];
                }
            }


            int subsorts = columns / 2;

            int magnitude = 0;
            for (int s = rows - 1; s > 0; s--)
            {
                for (int sub_b = 0; sub_b > subsorts; sub_b++)
                {
                    magnitude = rows - s;
                    SublistSort(process, s, (int)(Math.Pow(2, magnitude) * sub_b), (int)(Math.Pow(2, magnitude) * (sub_b + 1) - 1));
                    // El límite inferior es definido por la función f(x)=(2^m)x, y el límite superior 
                    // por la función g(x)=(2^m)(x+1)-1
                    // Donde x = sub_b, m = mag
                }
            }

            float[] ordered = new float[list.Length];
            for (int d = 0; d < list.Length; d++)
            {
                float ordered_element = float.Parse(process[0, d]);
                ordered[d] = ordered_element;
            }

            return ordered;

            string CompareIfEmpty(string xs, string ys)
            {
                float x = float.Parse((xs));
                float y = float.Parse((ys));
                float minimum = Math.Min(x, y);

                if (String.IsNullOrEmpty(xs) && String.IsNullOrEmpty(ys))
                {
                    return "";
                }
                else if (String.IsNullOrEmpty(ys))
                {
                    return xs;
                }
                else if (String.IsNullOrEmpty(xs))
                {
                    return ys;
                } else
                {
                    if (minimum == x)
                    {
                        return xs;
                    } else
                    {
                        return ys;
                    }
                }
            }

            void SublistSort(string[,] sample_matrix, int current_row, int first_limit, int second_limit)
            {
                void Move(int begin, int end)
                {
                    for (int m = begin + 1; m < end; m++)
                    {
                        sample_matrix[current_row, m - 1] = sample_matrix[current_row, m];
                    }
                    sample_matrix[current_row, end] = "";
                }

                int total = (int)Math.Pow(2, (sample_matrix.GetLength(0) - current_row));
                string cont1 = "";
                string cont2 = "";
                string minimum_str = "";
                for (int i = 0; i < total; i++)
                {
                    cont1 = sample_matrix[current_row, first_limit];
                    cont2 = sample_matrix[current_row, second_limit];
                    minimum_str = CompareIfEmpty(cont1, cont2);
                    sample_matrix[current_row - 1, i] = minimum_str;

                    if (minimum_str == cont1)
                    {
                        Move(first_limit, second_limit);
                    }
                    else
                    {
                        Move(second_limit, sample_matrix.GetLength(0));
                    }
                }
            }
        }*/
    }
}

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
        public static float[] Insertion(float[] list)
        {
            float[] ordered = new float[list.Length];
            float[] process = new float[list.Length + 1];

            for (int i = 0; i < list.Length; i++)
            {
                process[i + 1] = list[i]; //Tendrá un espacio vacío al inicio.
            }

            /* El ordenamiento por inserción verifica si los elementos process[0] y process[1] están ordenados,
            de ser así, sigue con process[1] y process[2], de tal manera que compara los elementos process[n] y process[n + 1]
            con todos los elementos. Si encuentra que un par de elementos process[n] y process[n + 1] no está ordenado en orden ascendente, invierte el orden
            de los elementos y retrocede para comparar los elementos process[n - 1] y process[n], si su orden no es el correcto, invierte el orden de los elementos
            y vuelve a retroceder, hasta que el orden de los elementos sea el correcto.
             */

            float contained1 = 0;
            float contained2 = 0;
            int minus = 1;

            for (int key = 2; key <= list.Length; key++)
            {
                minus = 1;
                while (process[key - minus + 1] < process[key - minus])
                {
                    contained1 = process[key - minus + 1];
                    contained2 = process[key - minus];
                    process[key - minus + 1] = contained2;
                    process[key - minus] = contained1;
                    minus++;
                }
            }

            for (int i = 1; i < process.Length; i++)
            {
                ordered[i - 1] = process[i];
            }

            return ordered;
        }

        public static float[] Bubble(float[] list)
        {
            float[] process = new float[list.Length + 1];

            for (int i = 0; i < list.Length; i++)
            {
                process[i + 1] = list[i]; //Tendrá un espacio vacío al final.
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
                for (int b = 0; b < needed_iterations; b++)
                {
                    if (process[b] > process[b + 1])
                    {
                        contained1 = process[b];
                        contained2 = process[b + 1];
                        process[b] = contained2;
                        process[b + 1] = contained1;
                    }
                }
                needed_iterations--;
            }

            for (int a = 0; a < list.Length; a++)
            {
                list[a] = process[a + 1];
            }

            return list;
        }

        public static float[] Bogo(float[] list)
        {
            void Shuffle(float[] sample)
            {
                /*
                Este es el algoritmo usado para hacer que el orden de los elementos sea aleatorio, específicamente, el algoritmo Fisher-Yates.
                */
                int position = 0;
                float r_swapped1 = 0f;
                float r_swapped2 = 0f;
                Random pos_random = new Random();

                for (int i = 0; i < sample.Length; i++)
                {
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
                        return false;
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
            int extra = list.Length / 2 + 1;

            float[] process = new float[list.Length + extra];

            for (int i = 0; i < list.Length; i++)
            {
                process[i + extra] = list[i];
            }
            /* El ordenamiento Shell es muy similar al ordenamiento por inserción.
             * El proceso de verificación de orden es idéntico, pero las comparaciones se hacen en intervalos mayores a uno.
             * El intervalo entre elementos va reduciendo hasta llegar a 1.
             * Cuando el intervalo entre elementos sea 1, sólo se hace un ordenamiento por inserción, pero será más rápido ya que la lista está parcialmente ordenada.
             * Los intervalos se determinarán en la definición original del ordenamiento Shell: f(x) = ⌊x/2^n⌋.
             * Donde x = Número de elementos en la lista; y n = Exponente, aumentará por 1 por cada iteración, hasta que el f(x) = 1.
             */
            float contained1 = 0;
            float contained2 = 0;
            int minus = 0;
            int exponent = 1;
            int interval = list.Length / (int)(Math.Pow(2, exponent));

            while (interval > 0) 
            {
                for (int key = extra; key < process.Length; key += interval)
                {
                    minus = 0;
                    while (process[key - minus] < process[key - interval - minus])
                    {
                        contained1 = process[key - minus];
                        contained2 = process[key - interval - minus];
                        process[key - minus] = contained2;
                        process[key - interval - minus] = contained1;
                        minus += interval;
                    }
                }
                exponent++;
                interval = list.Length / (int)(Math.Pow(2, exponent));
            }

            float[] ordered = new float[list.Length];
            for (int i = extra; i < process.Length; i++)
            {
                ordered[i - extra] = process[i];
            }

            return ordered;
        }

        public static float[] Merge(float[] list)
        {
            int columns = 0;
            int rows = 0;
            while (columns < list.Length)
            {
                rows++;
                columns = (int)Math.Pow(2, rows);
                /* El número de filas será el exponente que haga una potencia de 2 mayor o 
                 * igual al número de columnas, además de una fila adicional.
                 * y el número de columnas es la potencia resultante.
                 */
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
                    /* El límite inferior es definido por la función f(x)=(2^m)x, y el límite superior 
                     * por la función g(x)=(2^m)(x+1)-1
                     * Donde x = sub_b, m = mag*/
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
        }
    }
}

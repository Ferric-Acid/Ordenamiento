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
            de los elementos y retrocede para comparar los elementos process[n - 1] y process[n], si su orden no es el correcto, inverte el orden de los elementos
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

            return list;

        }

        public static float[] Bubble(float[] list)
        {
            float[] ordered = new float[list.Length];
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
            int nec_iterations = list.Length;

            for (int i = 0; i < list.Length; i++)
            {
                for (int b = 0; b < nec_iterations; b++)
                {
                    if (process[b] > process[b + 1])
                    {
                        contained1 = process[b];
                        contained2 = process[b + 1];
                        process[b] = contained2;
                        process[b + 1] = contained1;
                    }
                }
                nec_iterations--;
            }

            for (int a = 0; a < list.Length; a++)
            {
                list[a] = process[a + 1];
            }

            return list;
        }

        public static float[] Bogo(float[] list)
        {
            float[] process = new float[list.Length];
            for (int i = 0; i < list.Length; i++)
            {
                process[i] = list[i]; //No tendrá espacios vacíos.
            }

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

            while (isSorted(process) == false)
            {
                Shuffle(process);
            }
            return process;
        }

        public static float[] Merge(float[] list)
        {
            float CompareIfEmpty(float x, float y)
            {
                string xs = Convert.ToString(x);
                string ys = Convert.ToString(y);

                if (String.IsNullOrEmpty(xs) && String.IsNullOrEmpty(ys))
                {
                    return float.Parse("");
                }
                else if (String.IsNullOrEmpty(ys))
                {
                    return x;
                }
                else if (String.IsNullOrEmpty(xs))
                {
                    return y;
                }
                else
                {
                    return Math.Min(x, y);
                }
            }

            void SublistSort(float[,] div, int current_row, int f_lim, int s_lim)
            {
                void Move(int begin, int end)
                {
                    for (int m = begin; m < end; m++)
                    {
                        div[current_row, m] = div[current_row, m++];
                    }
                }

                int total = (int)Math.Pow(2, (div.GetLength(0) - current_row));
                float cont1 = 0;
                float cont2 = 0;
                float minim = 0;
                for (int i = 0; i < total; i++)
                {
                    cont1 = div[current_row, f_lim];
                    cont2 = div[current_row, s_lim];
                    minim = CompareIfEmpty(cont1, cont2);

                    if (minim == cont1)
                    {
                        Move(f_lim, s_lim);
                    }
                    else
                    {
                        Move(s_lim, div.GetLength(0));
                    }
                }
            }

            int columns = 0;
            int rows = 0;
            while (columns < list.Length)
            {
                rows++;
                columns = (int)Math.Pow(2, rows); // El número de filas será el exponente que haga una potencia de 2 mayor o igual al número de columnas, además de una fila adicional, y el número de columnas es la potencia resultante.
            }

            rows++;

            float[,] process = new float[rows, columns];
            for (int y = 0; y < list.Length; y++)
            {
                process[0, y] = list[y];
            }

            for (int xd = 1; xd < process.GetLength(0); xd++)
            {
                for (int yd = 0; yd < process.GetLength(1); yd++)
                {
                    process[xd, yd] = process[0, yd];
                }
            }


            int subsorts = rows / 2;

            int mag = 0;
            for (int s = rows - 1; s > 0; s--)
            {
                for (int sub_b = 0; sub_b > subsorts; sub_b++)
                {
                    mag = rows - s;
                    SublistSort(process, s, (int)(Math.Pow(2, mag) * sub_b), (int)(Math.Pow(2, mag) * (++sub_b) - 1));
                    // El límite inferior es definido por la función f(x)=(2^m)x, y el límite superior por la función g(x)=(2^m)(x+1)-1
                    // Donde x = sub_b, m = mag.
                }
                for (int db = 0; db < process.GetLength(1); db++)
                {
                    process[s - 1, db] = process[s, db];
                }
                subsorts /= 2;
            }

            float[] ordered = new float[process.GetLength(1)];
            for (int d = 0; d < process.GetLength(1); d++)
            {
                ordered[d] = process[1, d];
            }

            return ordered;
        }
    }
}

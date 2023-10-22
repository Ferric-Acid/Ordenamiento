using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Ordenamiento
{
    internal class Algor
    {
        public static void Insertion(float[] list)
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

            for (int a = 0; a < list.Length; a++)
            {
                list[a] = process[a + 1];
                Console.WriteLine(list[a]);
            }

        }

        public static void Bubble(float[] list)
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
                Console.WriteLine(list[a]);
            }
        }



        public static float[] Bogo(float[] list)
        {
            float[] process = new float[list.Length + 1];
            for (int i = 0; i < list.Length; i++)
            {
                process[i] = list[i]; //No tendrá espacios vacíos.
            }
            
            float[] Shuffle(float[] sample)
            {
                /*
                Este es el algoritmo usado para hacer que el orden de los elementos sea aleatorio, específicamente, el algoritmo Fisher-Yates.
                */
                float[] randomized = new float[sample.Length];

                for (int i = 0; i < list.Length; i++)
                {
                    randomized[i] = sample[i]; //No tendrá espacios vacíos.
                }

                int position = 0;
                float r_swapped1 = 0.0f;
                float r_swapped2 = 0.0f;
                Random pos_random = new Random();

                for (int i = 0; i < sample.Length; i++)
                {
                    position = pos_random.Next(i,list.Length);
                    r_swapped1 = randomized[i];
                    r_swapped2 = randomized[position];
                    randomized[i] = r_swapped2;
                    randomized[position] = r_swapped1;
                }

                return randomized; 
            }
            

            bool sorted = false;
            while (sorted == false)
            {
                for (int p = 0; p < list.Length; p++)
                {
                    if (process[p] > process[p + 1])
                    {
                        break;
                    }
                    sorted = true;
                }

                if (sorted == false)
                {
                    process = Shuffle(process);
                }

            }

            return process;
        }

    }
}

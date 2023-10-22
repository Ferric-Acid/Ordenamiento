

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
        static void Main(string[] args)
        {
            // Nombre preliminar: El Ordenamiento y Tu
            float[] arr_og = { 97.56f, 41.45f, 39.56f, 73.56f, 83.83f, 73.58f };
            float[] sorted = new float[arr_og.Length];
            sorted = Algor.Bogo(arr_og);
            foreach (float element in sorted)
            {
                Console.WriteLine(element);
            }
        }
    }
}

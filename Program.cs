

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
            float[] arr_og = {9f, 8f, 7f, 6f, 5f, 4f, 3f, 2f, 1f };
            float[] sorted = new float[arr_og.Length];
            sorted = Algor.Shell(arr_og);
            foreach (float element in sorted)
            {
                Console.WriteLine(element);
            }
        }
    }
}

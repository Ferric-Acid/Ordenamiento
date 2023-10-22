

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
            float[] arr_og = {35.1f, 97.6f, 41.2f, 98.2f, 23.7f};
            Algor.Bogo(arr_og);
            foreach (float element in arr_og)
            {
                Console.WriteLine(element);
            }
        }
    }
}

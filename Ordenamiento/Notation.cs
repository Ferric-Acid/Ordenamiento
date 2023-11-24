using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Ordenamiento
{
    internal class Notation
    {

        public static void Not()
        {
            // Esta es la explicación de la notación asintótica, usada para describir el tiempo de procesamiento necesario de un algoritmo según la entrada que reciban.
            Console.WriteLine("Al analizar algoritmos, en este caso algoritmos de ordenamiento, la memoria que usa (la cual no se cubrirá debido al tamaño mínimo de los elementos) y su tiempo de ejecución, la notación asintótica se usa para medir el cambio del tiempo de ejecución con el aumento del tamaño de la entrada, el tamaño de la lista; el tiempo necesario para ejecutar un algoritmo de ordenamiento se puede expresar en una función matemática f(n), donde la función \"f\" es el algoritmo y la variable \"n\" representa la cantidad de elementos en la lista, tales como:\n\n◘ Función constante: 1\n◘ Función logarítmica: log n\n◘ Función polinomial: an + b\n◘ an^z + an^(z-1) + . . . + an^2 + an + a\n◘ Función exponencial: a^n.\n◘ Función factorial: n!\n\nNota: \"a\" representa una constante.\nEn esta lista, las funciones que están más abajo son las que se tardan más.");
            Program.KeyContinue();
            Console.WriteLine("Pero en la notación asintótica se omiten términos inferiores al mayor y constantes para que permanezca el crecimiento y simplificar la nomenclatura, ya que suelen causar diferencias irrelevantes.\nEjemplo: f(n) = 4n^3 + 7n^2 + 1 -->  4n^3 -->  n^3");
            Program.KeyContinue();
            Console.WriteLine("Existen 3 tipos de notación asintótica: Big-O, Big-Omega y Big-Theta.");
            Program.KeyContinue();
            Console.WriteLine("Notación Big-O\n" +
                "La notación O Grande o Big-O [O(n)] mide el tiempo del procesamiento de un algoritmo para el peor caso, o la función techo del crecimiento de una función.\nEste se utiliza para expresar el tiempo máximo de ejecución. Y es la notación más usada de las tres.");
            Program.KeyContinue();
            Console.WriteLine("Big-Omega\n" +
                "Mientras que la notación Big-Omega (que se representa con la letra griega omega mayúscula, pero la terminal no admite este tipo de caracteres especiales, así que se representará con \"OMEGA\") [OMEGA(n)] mide el tiempo del procesamiento para el mejor caso, o la función piso del crecimiento de una función.\nEste se usa para expresar el tiempo mínimo de ejecución. Es la notación menos usada de las tres.");
            Program.KeyContinue();
            Console.WriteLine("Big-Theta\n" +
                "Finalmente, la notación Big-Theta (que se representa con la letra griega zeta mayúscula, pero se representará con \"ZETA\") [ZETA(n)] mide el tiempo de procesamiento para el caso promedio, que se da cuando O(n) y OMEGA(n) son iguales o cuando se realiza un análisis más profundo del algoritmo.\n");
            
            Console.WriteLine("\n[Presiona cualquier tecla para regresar al menú principal.]");
            Console.ReadKey();
            Program.Menu();
        }
    }
}
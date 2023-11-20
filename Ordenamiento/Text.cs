using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Ordenamiento
{
    internal class Text
    {
        static void KeyContinue()
        {
            Console.WriteLine("\n[Presiona cualquier tecla para continuar.]\n");
            Console.ReadKey();
        }

        static string user = "";
        static string directory = "";
        static string route = "";

        public static void Choice()
        {
            Console.Clear();
            while (string.IsNullOrWhiteSpace(user))
            {
                Console.WriteLine("Antes de comenzar, ¿cuál es nombre de la carpeta de su usuario?");
                user = Console.ReadLine().ToLower();
            }

            Console.WriteLine("Elige la operación que desees realizar sobre un archivo.\n\n1. Crear un archivo " +
                "nuevo.\n2. Leer un archivo existente.\n3. Actualizar los datos de un archivo.\n" +
                "4. Borrar un archivo.\n5. Regresar al menú anterior.");

            sbyte CRUD_return = Convert.ToSByte(Console.ReadLine());
            directory = $"C:\\Users\\{user}\\Documents\\Sorting";
            switch (CRUD_return)
            {
                case 1: Create(); break;
                case 2: Read(); break;
                case 3: Update(); break;
                case 4: Delete(); break;
                case 5: Program.Menu(); break;
                default:
                    Console.WriteLine("Opción no válida, introduce una opción válida.");
                    KeyContinue();
                    Console.Clear();
                    Choice();
                    break;
            }
        }

        public static void Nonexistent_Empty_Directory(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Console.WriteLine("El directorio no existe, no hay archivos para mostrar.");
                KeyContinue();
                Text.Choice();
            }

            if (!Directory.EnumerateFileSystemEntries(dir).Any())
            {
                Console.WriteLine("El directorio existe, pero está vacío.");
                KeyContinue();
                Text.Choice();
            }
        }


        public static void Create()
        {
            if (!Directory.Exists(directory))
            {
                Console.WriteLine("Se creará el directorio \"Sorting\" en \"Documents\".");
                Directory.CreateDirectory(directory);
            }

            Console.WriteLine("Menciona el nombre del archivo a crear, se le adjuntará la extensión \".txt\" al final.");
            string file_name = Console.ReadLine() + ".txt";
            route = Path.Combine(directory, file_name);

            if (!File.Exists(route))
            {
                Console.WriteLine("El archivo como tal no existe, será creado en el directorio.");
                File.Create(route).Close();
                Console.WriteLine("Ingresa los números a insertar en el documento, al terminar, deja la línea en blanco.");


                using (StreamWriter writer = File.AppendText(route))
                {
                    string input;
                    while (!string.IsNullOrEmpty((input = Console.ReadLine())))
                    {
                        writer.WriteLine(input);
                    }
                }
            }
            else
            {
                Console.WriteLine($"El archivo en {route} ya existe.");
            }
            KeyContinue();
            Choice();
        }

        static void Read()
        {
            Nonexistent_Empty_Directory(directory);

            Console.WriteLine("Estos son los archivos existentes:");
            string[] lists = Directory.GetFiles(directory);
            foreach (string list in lists)
            {
                Console.WriteLine(Path.GetFileName(list));
            }

            Console.WriteLine("¿Cuál archivo deseas ver? Se le agregará la extensión \".txt\".");
            string file = Console.ReadLine() + ".txt";
            route = Path.Combine(directory, file);

            if (!File.Exists(route))
            {
                Console.WriteLine("Tal archivo no existe.");
                KeyContinue();
                Choice();
            }

            Console.WriteLine("Estos son los elementos:");
            using (StreamReader reader = File.OpenText(route))
            {
                string element;
                while ((element = reader.ReadLine()) != null)
                {
                    Console.WriteLine(element);
                }
            }
            KeyContinue();
            Choice();
        }

        static void Update()
        {
            Nonexistent_Empty_Directory(directory);

            Console.WriteLine("Estos son los archivos existentes:");
            string[] lists = Directory.GetFiles(directory);
            foreach (string list in lists)
            {
                Console.WriteLine(Path.GetFileName(list));
            }

            Console.WriteLine("¿Cuál archivo deseas modificar? Se le agregará la extensión \".txt\".");
            string searched = Console.ReadLine() + ".txt";
            route = Path.Combine(directory, searched);

            if (!File.Exists(route))
            {
                Console.WriteLine("Tal archivo no existe.");
                KeyContinue();
                Choice();
            }

            void Overwrite_Add()
            {
                Console.WriteLine("¿Deseas sobreescribir el archivo o añadir información?\n\n1. Sobreescribir\n" +
                    "2. Añadir texto");
                sbyte OA_choice = Convert.ToSByte(Console.ReadLine());
                switch (OA_choice)
                {
                    case 1:
                        File.WriteAllText(route, string.Empty);
                        break;

                    case 2:
                        break;

                    default:
                        Console.WriteLine("Opción no válida, intoduce una opción válida.");
                        KeyContinue();
                        Overwrite_Add();
                        break;
                }
            }

            Overwrite_Add();

            Console.WriteLine("Introduce los elementos nuevos para el archivo. Deja el espacio en blanco cuando " +
            "termines.");
            using (StreamWriter writer = File.AppendText(route))
            {
               string input;
               while (!string.IsNullOrEmpty((input = Console.ReadLine())))
               {
                    writer.WriteLine(input);
               }
            }

            Console.WriteLine("Archivo actualizado exitosamente.");
            KeyContinue();
            Choice();
            
        }

        static void Delete()
        {
            Nonexistent_Empty_Directory(directory);

            Console.WriteLine("Estos son los archivos existentes:");
            string[] lists = Directory.GetFiles(directory);
            foreach (string list in lists)
            {
                Console.WriteLine(Path.GetFileName(list));
            }

            Console.WriteLine("¿Cuál archivo deseas eliminar? Se le agregará la extensión \".txt\".");
            string to_delete = Console.ReadLine() + ".txt";
            route = Path.Combine(directory, to_delete);

            if (File.Exists(route))
            {
                File.Delete(route);
                Console.WriteLine("Archivo eliminado exitosamente.");
            }
            else
            {
                Console.WriteLine("Tal archivo no existe.");
            }

            KeyContinue();
            Choice();
        }
    }
}

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
        static string user = "";
        static string directory = "";
        static string route = "";

        public static void Choice()
        {
            Console.Clear();
            // Siempre que se ejecute el programa, se le pedirá el nombre de la carpeta del usuario, pero sólo una vez.
            while (string.IsNullOrWhiteSpace(user))
            {
                Console.WriteLine("Antes de comenzar, ¿cuál es nombre de la carpeta de su usuario?");
                user = Console.ReadLine().ToLower();
            }

            Console.WriteLine("Modificación de archivos de texto con listas\nElige la operación que desees realizar sobre un archivo.\n\n1. Crear un archivo " +
                "nuevo.\n2. Leer un archivo existente.\n3. Actualizar los datos de un archivo.\n" +
                "4. Borrar un archivo.\n5. Regresar al menú anterior.");

            int CRUD_return = 0;

            try
            {
                CRUD_return = Convert.ToInt32(Console.ReadLine());
            }
            catch (System.FormatException)
            {
                Console.WriteLine("El formato de la entrada no es correcto, intenta introduce uno de los números especificados.");
                Program.KeyContinue();
                Console.Clear();
                Choice();
                throw;
            }

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
                    Program.KeyContinue();
                    Console.Clear();
                    Choice();
                    break;
            }
        }

        // Determina si el directorio "C:\Users\[nombre de usuario]\Documents\Sorting" no existe o si está vacío.
        public static void Nonexistent_Empty_Directory(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Console.WriteLine("El directorio no existe, no hay archivos para mostrar.");
                Program.KeyContinue();
                Text.Create();
            }

            if (!Directory.EnumerateFileSystemEntries(dir).Any())
            {
                Console.WriteLine("El directorio existe, pero está vacío.");
                Program.KeyContinue();
                Text.Create();
            }
        }


        public static void Create()
        {
            // Si el directorio no existe, se creará.
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


                // Permitirá almacenar valores en el archivo recién creado, hasta que la entrada del usuario esté vacía.
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
            Program.KeyContinue();
            Choice();
        }

        static void Read()
        {
            Nonexistent_Empty_Directory(directory);

            // Primero se muestran todos los archivos que existen en el directorio.
            Console.WriteLine("Estos son los archivos existentes:");
            string[] lists = Directory.GetFiles(directory);
            foreach (string list in lists)
            {
                Console.WriteLine(Path.GetFileName(list));
            }

            // Se le pide al usuario el nombre del archivo que desea consultar, sin incluir la extensión .txt
            Console.WriteLine("¿Cuál archivo deseas ver? Se le agregará la extensión \".txt\".");
            string file = Console.ReadLine() + ".txt";
            route = Path.Combine(directory, file);

            if (!File.Exists(route))
            {
                Console.WriteLine("Tal archivo no existe.");
                Program.KeyContinue();
                Choice();
            }

            // Se mostrarán todos los elementos existentes en el directorio.
            Console.WriteLine("Estos son los elementos:");
            using (StreamReader reader = File.OpenText(route))
            {
                string element;
                while ((element = reader.ReadLine()) != null)
                {
                    Console.WriteLine(element);
                }
            }
            Program.KeyContinue();
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

            // Se le pide al usuario el nombre del archivo que desea consultar, sin incluir la extensión .txt
            Console.WriteLine("¿Cuál archivo deseas modificar? Se le agregará la extensión \".txt\".");
            string searched = Console.ReadLine() + ".txt";
            route = Path.Combine(directory, searched);

            if (!File.Exists(route))
            {
                Console.WriteLine("Tal archivo no existe.");
                Program.KeyContinue();
                Choice();
            }

            // Al modificar un archivo ya existente, sólo se puede sobreescribir o añadir más información al final de este.
            void Overwrite_Add()
            {
                Console.WriteLine("¿Deseas sobreescribir el archivo o añadir información?\n\n1. Sobreescribir\n2. Añadir texto");
                int OA_choice = 0;

                try
                {
                    OA_choice = Convert.ToInt32(Console.ReadLine());
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("El formato de la entrada no es correcto, intenta introduce uno de los números especificados.");
                    Program.KeyContinue();
                    Console.Clear();
                    Overwrite_Add();
                    throw;
                }

                switch (OA_choice)
                {
                    case 1:
                        // En caso de que se desee sobreescribir el archivo elegido, se formateará el archivo.
                        File.WriteAllText(route, string.Empty);
                        break;

                    case 2:
                        break;

                    default:
                        Console.WriteLine("Opción no válida, intoduce una opción válida.");
                        Program.KeyContinue();
                        Overwrite_Add();
                        break;
                }
            }

            Overwrite_Add();

            Console.WriteLine("Introduce los elementos nuevos para el archivo. Deja el espacio en blanco cuando termines.");
            // Independientemente de que se haya solicitado sobreescribir o agregar información al archivo de texto, se usa el método File.AppendText para realizar la operación solicitada.
            using (StreamWriter writer = File.AppendText(route))
            {
               string input;
               while (!string.IsNullOrEmpty((input = Console.ReadLine())))
               {
                    writer.WriteLine(input);
               }
            }

            Console.WriteLine("Archivo actualizado exitosamente.");
            Program.KeyContinue();
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

            // Se le pide al usuario el nombre del archivo que desea consultar, sin incluir la extensión .txt
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

            Program.KeyContinue();
            Choice();
        }
    }
}

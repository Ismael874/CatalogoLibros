using System.Text;
using CatalogoLibros.Data;
using CatalogoLibros.Models;
using CatalogoLibros.Services;

namespace CatalogoLibros;

public static class Program
{
    private static readonly BookCatalog catalog = new();

    public static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        SeedData.Load(catalog);

        string option;
        do
        {
            ShowMenu();
            option = (Console.ReadLine() ?? "").Trim();
            Console.WriteLine();

            switch (option)
            {
                case "1": AddBook(); break;
                case "2": ListBooks(); break;
                case "3": SearchBook(); break;
                case "4": UpdateBook(); break;
                case "5": DeleteBook(); break;
                case "0": Console.WriteLine("Saliendo del sistema..."); break;
                default: Console.WriteLine("Opción no válida."); break;
            }

            if (option != "0")
            {
                Console.WriteLine("\nPresione ENTER para continuar...");
                Console.ReadLine();
            }
        } while (option != "0");
    }

    private static void ShowMenu()
    {
        if (!Console.IsOutputRedirected) Console.Clear();
        Console.WriteLine("====== SISTEMA DE BIBLIOTECA ======");
        Console.WriteLine($"  Libros registrados: {catalog.Count}/{BookCatalog.Capacity}\n");
        Console.WriteLine("1. Agregar libro");
        Console.WriteLine("2. Listar libros");
        Console.WriteLine("3. Buscar libro por ID");
        Console.WriteLine("4. Actualizar libro");
        Console.WriteLine("5. Eliminar libro");
        Console.WriteLine("0. Salir");
        Console.Write("\nSeleccione una opción: ");
    }

    // ---------- 1. Agregar: O(1) ----------
    private static void AddBook()
    {
        Console.WriteLine("--- AGREGAR LIBRO ---");
        if (catalog.IsFull)
        {
            Console.WriteLine($"ERROR: El catálogo está lleno ({BookCatalog.Capacity}/{BookCatalog.Capacity}). " +
                              "El arreglo tiene tamaño fijo y no admite más libros.");
            return;
        }

        int id = catalog.NextId();
        Console.WriteLine($"ID asignado: {id}");
        string title = ReadText("Título: ");
        string author = ReadText("Autor: ");
        int year = ReadInt("Año de publicación: ");
        string genre = ReadText("Género: ");

        if (catalog.Add(new Book(id, title, author, year, genre)))
            Console.WriteLine($"Libro agregado en la posición [{catalog.Count - 1}] del arreglo.");
    }

    // ---------- 2. Listar: O(n) ----------
    private static void ListBooks()
    {
        Console.WriteLine("--- LISTADO DE LIBROS ---");
        Book[] books = catalog.GetAll();
        if (books.Length == 0)
        {
            Console.WriteLine("No hay libros registrados.");
            return;
        }

        PrintHeader();
        foreach (Book book in books)
            Console.WriteLine(book);
        PrintLine();
        Console.WriteLine($"Total: {books.Length} libro(s).");
    }

    // ---------- 3. Buscar: O(n) ----------
    private static void SearchBook()
    {
        Console.WriteLine("--- BUSCAR LIBRO POR ID ---");
        int id = ReadInt("Ingrese el ID: ");
        Book? book = catalog.FindById(id);

        if (book is null)
        {
            Console.WriteLine($"No existe ningún libro con el ID {id}.");
            return;
        }

        PrintHeader();
        Console.WriteLine(book);
        PrintLine();
    }

    // ---------- 4. Actualizar: O(n) ----------
    private static void UpdateBook()
    {
        Console.WriteLine("--- ACTUALIZAR LIBRO ---");
        int id = ReadInt("Ingrese el ID del libro a actualizar: ");
        Book? book = catalog.FindById(id);

        if (book is null)
        {
            Console.WriteLine($"No existe ningún libro con el ID {id}.");
            return;
        }

        Console.WriteLine("Deje el campo vacío para conservar el valor actual.");
        string title = ReadOptional($"Título [{book.Title}]: ", book.Title);
        string author = ReadOptional($"Autor [{book.Author}]: ", book.Author);
        string yearText = ReadOptional($"Año [{book.Year}]: ", book.Year.ToString());
        int year = int.TryParse(yearText, out int y) ? y : book.Year;
        string genre = ReadOptional($"Género [{book.Genre}]: ", book.Genre);

        catalog.Update(id, title, author, year, genre);
        Console.WriteLine("Libro actualizado correctamente.");
    }

    // ---------- 5. Eliminar: O(n) ----------
    private static void DeleteBook()
    {
        Console.WriteLine("--- ELIMINAR LIBRO ---");
        int id = ReadInt("Ingrese el ID del libro a eliminar: ");
        Book? book = catalog.FindById(id);

        if (book is null)
        {
            Console.WriteLine($"No existe ningún libro con el ID {id}.");
            return;
        }

        Console.Write($"¿Eliminar \"{book.Title}\"? (s/n): ");
        if ((Console.ReadLine() ?? "").Trim().ToLower() != "s")
        {
            Console.WriteLine("Operación cancelada.");
            return;
        }

        catalog.Remove(id);
        Console.WriteLine("Libro eliminado. Los elementos posteriores se desplazaron a la izquierda.");
    }

    // ---------- Utilidades de entrada/salida ----------
    private static string ReadText(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string value = (Console.ReadLine() ?? "").Trim();
            if (value.Length > 0) return value;
            Console.WriteLine("El valor no puede estar vacío.");
        }
    }

    private static string ReadOptional(string prompt, string current)
    {
        Console.Write(prompt);
        string value = (Console.ReadLine() ?? "").Trim();
        return value.Length == 0 ? current : value;
    }

    private static int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int value)) return value;
            Console.WriteLine("Ingrese un número entero válido.");
        }
    }

    private static void PrintLine() => Console.WriteLine(new string('-', 105));

    private static void PrintHeader()
    {
        PrintLine();
        Console.WriteLine($"| {"ID",-4} | {"Título",-38} | {"Autor",-26} | {"Año",-4} | {"Género",-17} |");
        PrintLine();
    }
}

using CatalogoLibros.Models;

namespace CatalogoLibros.Services;

/// <summary>
/// Gestiona el catálogo de libros usando EXCLUSIVAMENTE un arreglo de tamaño fijo (Book[]).
/// Los libros se mantienen contiguos en las posiciones [0 .. count-1], sin huecos.
/// </summary>
public class BookCatalog
{
    public const int Capacity = 20;

    private readonly Book[] books = new Book[Capacity];
    private int count;   // Cantidad de libros registrados = siguiente posición disponible.

    public int Count => count;
    public bool IsFull => count == Capacity;

    /// <summary>
    /// Agrega un libro en la siguiente posición disponible (books[count]).
    /// Big O: O(1) — acceso directo por índice, sin recorrer el arreglo.
    /// (Si se valida que el Id no esté repetido, la operación pasa a O(n) por la búsqueda previa.)
    /// </summary>
    public bool Add(Book book)
    {
        if (IsFull)
            return false;           // Capacidad máxima alcanzada: el arreglo no puede crecer.

        books[count] = book;        // Inserción al final: O(1)
        count++;
        return true;
    }

    /// <summary>
    /// Devuelve los libros registrados recorriendo el arreglo de 0 a count-1.
    /// Big O: O(n) — se visita cada libro una vez.
    /// </summary>
    public Book[] GetAll()
    {
        Book[] result = new Book[count];
        for (int i = 0; i < count; i++)
            result[i] = books[i];
        return result;
    }

    /// <summary>
    /// Búsqueda lineal: recorre el arreglo hasta encontrar el Id.
    /// Big O: O(n) — peor caso (no existe o está al final) revisa los n libros.
    ///        Mejor caso O(1) si está en la primera posición.
    /// Retorna el índice o -1 si no existe.
    /// </summary>
    public int IndexOf(int id)
    {
        for (int i = 0; i < count; i++)
        {
            if (books[i].Id == id)
                return i;
        }
        return -1;
    }

    /// <summary>
    /// Busca un libro por Id.
    /// Big O: O(n) — depende de la búsqueda lineal.
    /// </summary>
    public Book? FindById(int id)
    {
        int index = IndexOf(id);
        return index == -1 ? null : books[index];
    }

    /// <summary>
    /// Actualiza los datos de un libro existente.
    /// Big O: O(n) — O(n) para localizarlo + O(1) para modificarlo en su índice.
    /// </summary>
    public bool Update(int id, string title, string author, int year, string genre)
    {
        int index = IndexOf(id);            // O(n)
        if (index == -1)
            return false;

        books[index].Title = title;         // O(1)
        books[index].Author = author;
        books[index].Year = year;
        books[index].Genre = genre;
        return true;
    }

    /// <summary>
    /// Elimina un libro y desplaza a la izquierda los elementos posteriores
    /// para no dejar posiciones vacías entre los libros registrados.
    /// Big O: O(n) — O(n) para buscarlo + O(n) para desplazar = O(n).
    /// </summary>
    public bool Remove(int id)
    {
        int index = IndexOf(id);            // O(n)
        if (index == -1)
            return false;

        // Desplazamiento a la izquierda: O(n)
        for (int i = index; i < count - 1; i++)
            books[i] = books[i + 1];

        books[count - 1] = null!;           // Liberar la última posición ocupada.
        count--;
        return true;
    }

    /// <summary>
    /// Genera el siguiente Id disponible (mayor Id + 1).
    /// Big O: O(n).
    /// </summary>
    public int NextId()
    {
        int max = 0;
        for (int i = 0; i < count; i++)
            if (books[i].Id > max) max = books[i].Id;
        return max + 1;
    }
}

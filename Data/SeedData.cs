using CatalogoLibros.Models;
using CatalogoLibros.Services;

namespace CatalogoLibros.Data;

/// <summary>
/// Carga inicial del catálogo con 12 libros (mínimo requerido: 10).
/// </summary>
public static class SeedData
{
    public static void Load(BookCatalog catalog)
    {
        catalog.Add(new Book(1, "Cien años de soledad", "Gabriel García Márquez", 1967, "Novela"));
        catalog.Add(new Book(2, "Don Quijote de la Mancha", "Miguel de Cervantes", 1605, "Clásico"));
        catalog.Add(new Book(3, "La ciudad y los perros", "Mario Vargas Llosa", 1963, "Novela"));
        catalog.Add(new Book(4, "1984", "George Orwell", 1949, "Distopía"));
        catalog.Add(new Book(5, "El principito", "Antoine de Saint-Exupéry", 1943, "Fábula"));
        catalog.Add(new Book(6, "Clean Code", "Robert C. Martin", 2008, "Programación"));
        catalog.Add(new Book(7, "Introduction to Algorithms", "Thomas H. Cormen", 1990, "Algoritmos"));
        catalog.Add(new Book(8, "Rayuela", "Julio Cortázar", 1963, "Novela"));
        catalog.Add(new Book(9, "La fiesta del chivo", "Mario Vargas Llosa", 2000, "Novela histórica"));
        catalog.Add(new Book(10, "Ficciones", "Jorge Luis Borges", 1944, "Cuento"));
        catalog.Add(new Book(11, "El amor en los tiempos del cólera", "Gabriel García Márquez", 1985, "Novela"));
        catalog.Add(new Book(12, "The Pragmatic Programmer", "Andrew Hunt", 1999, "Programación"));
    }
}

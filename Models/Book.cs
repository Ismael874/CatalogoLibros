namespace CatalogoLibros.Models;

/// <summary>
/// Representa un libro del catálogo de la biblioteca.
/// </summary>
public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
    public string Genre { get; set; }

    public Book(int id, string title, string author, int year, string genre)
    {
        Id = id;
        Title = title;
        Author = author;
        Year = year;
        Genre = genre;
    }

    public override string ToString() =>
        $"| {Id,-4} | {Title,-38} | {Author,-26} | {Year,-4} | {Genre,-17} |";
}

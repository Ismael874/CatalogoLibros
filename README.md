# Catálogo de Libros con Arreglos (C#)

Aplicación de consola que gestiona un catálogo de libros usando **exclusivamente un arreglo `Book[]`** con capacidad fija de **20** libros. Se cargan **12 libros** al iniciar.

## Estructura

```
CatalogoLibros/
├── Models/Book.cs            -> Entidad Libro (Id, Título, Autor, Año, Género)
├── Services/BookCatalog.cs   -> Operaciones sobre el arreglo (con Big O comentado)
├── Data/SeedData.cs          -> Carga inicial de 12 libros
└── Program.cs                -> Menú de consola
```

## Ejecución

```bash
dotnet run
```

## Complejidad temporal (Big O)

| Operación  | Complejidad | Explicación |
|------------|-------------|-------------|
| Agregar    | **O(1)**    | Se inserta directamente en `books[count]`, la siguiente posición libre; no se recorre el arreglo. |
| Listar     | **O(n)**    | Se recorre el arreglo desde 0 hasta `count - 1`, visitando cada libro una vez. |
| Buscar     | **O(n)**    | Búsqueda lineal: en el peor caso (no existe o está al final) se revisan los n libros. Mejor caso O(1). |
| Actualizar | **O(n)**    | O(n) para localizar el libro + O(1) para modificarlo por índice → O(n). |
| Eliminar   | **O(n)**    | O(n) para buscarlo + O(n) para desplazar a la izquierda los elementos posteriores → O(n). |

## Limitación del arreglo de tamaño fijo

**¿Qué pasa con el libro número 21?** El arreglo se creó con `new Book[20]`, es decir, índices del 0 al 19. Cuando `count == 20` el método `Add` detecta que el catálogo está lleno y rechaza la inserción mostrando un mensaje de error. Si no existiera esa validación, intentar escribir en `books[20]` lanzaría una `IndexOutOfRangeException`.

**¿Por qué es una limitación?** En C# el tamaño de un arreglo se define al crearlo y **no puede cambiar**: la memoria se reserva como un bloque contiguo de 20 posiciones. Si la biblioteca adquiere más libros no hay espacio, y si tiene pocos se desperdicia memoria reservada. Además, eliminar obliga a desplazar elementos (O(n)) para no dejar huecos.

**Solución conceptual:** usar un **arreglo dinámico**. Cuando el arreglo se llena, se crea uno nuevo con el **doble de capacidad**, se copian los elementos (O(n), ocasional) y se sigue insertando. Gracias a que la duplicación ocurre pocas veces, el costo **amortizado** de agregar sigue siendo O(1). Esto es exactamente lo que hace `List<Book>` en .NET (internamente usa `Array.Resize`). Otra alternativa es una **lista enlazada**, que crece nodo a nodo sin reservar un bloque contiguo, a cambio de perder el acceso directo por índice.



## Catálogo de Libros con Arreglos (C#)

Para esta asignación me pidieron crear una aplicación de consola que gestionara un catálogo de libros usando exclusivamente un arreglo `Book[]` con capacidad fija de 20 libros. Además, debía cargar 10 libros al iniciar y puse 12 por si acaso. Así que me puse manos a la obra y esto fue lo que armé.

Cómo organicé el proyecto

Decidí separar las responsabilidades en carpetas para que el código quedara ordenado y fuera fácil de entender:


## Catálogo Libros


Models/Book.cs            Aquí definí la entidad Libro (Id, Título, Autor, Año, Género)
Services/BookCatalog.cs   Aquí puse las operaciones sobre el arreglo, con los comentarios de Big O
Data/SeedData.cs          Aquí cargué los 12 libros iniciales
Program.cs                Aquí armé el menú de consola


## Cómo se ejecuta

Para probarlo solo hay que correr:

```bash
dotnet run
```

## La eficiencia de cada operación (complejidad temporal)

Una parte importante de la asignación era analizar qué tan eficiente era cada operación usando la notación Big O. Esto fue lo que determiné:

Lo primero es agregar. Esta operación es O(1), o sea, siempre tarda lo mismo sin importar cuántos libros haya. Esto se debe a que el libro nuevo se inserta directamente en `books[count]`, que es la siguiente posición libre. No hay que recorrer nada, es inmediato.

Luego está listar, que es O(n). Aquí sí hay que recorrer el arreglo desde la posición 0 hasta `count - 1`, visitando cada libro una vez. Si hay 12 libros, se visitan 12; si hay 20, se visitan 20.

Después tenemos buscar, que también es O(n). Se hace una búsqueda lineal: en el peor de los casos, cuando el libro no existe o está al final, hay que revisar los (n) libros. Pero en el mejor de los casos, si el libro está en la primera posición, sería O(1).

Para actualizar la complejidad es O(n). Primero hay que localizar el libro, lo cual toma O(n), y luego modificarlo por índice, que toma O(1). Sumando todo, queda O(n).

Y finalmente eliminar, que también es O(n). Hay que buscarlo (O(n)) y después desplazar hacia la izquierda todos los elementos que vienen después (O(n)). Al final, la complejidad queda O(n).

## El problema del arreglo de tamaño fijo

Aquí me topé con una limitación que la misma asignación pedía analizar. ¿Qué pasa si queremos agregar el libro número 21? Pues sencillamente no se puede. El arreglo se creó con `new Book[20]`, o sea, índices del 0 al 19. Cuando `count == 20`, el método `Add` detecta que el catálogo está lleno y rechaza la inserción mostrando un mensaje de error. Si no hubiera puesto esa validación, intentaría escribir en `books[20]` lanzaría una `IndexOutOfRangeException`.

¿Y por qué esto es una limitación? Porque en C# El tamaño de un arreglo se define al crearlo y no puede cambiar. La memoria se reserva como un bloque contiguo de 20 posiciones. Si la biblioteca consigue más libros, no hay espacio. Y si tiene pocos, se desperdicia memoria reservada. Además, eliminar obliga a desplazar elementos (O(n)) para no dejar huecos.

¿Cómo se podría solucionar? La idea sería usar un arreglo dinámico. Cuando el arreglo se llena, se crea uno nuevo con el doble de capacidad, se copian los elementos (O(n), pero solo pasa de vez en cuando) y se sigue insertando. Como la duplicación ocurre pocas veces, el costo amortizado de agregar sigue siendo O(1). Esto es exactamente lo que hace `List<Book>` en .NET (internamente usa `Array.Resize`). Otra alternativa sería una lista enlazada, que crece nodo a nodo sin reservar un bloque contiguo, aunque se pierde el acceso directo por índice.

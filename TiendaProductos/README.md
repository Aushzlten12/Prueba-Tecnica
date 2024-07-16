# Backend in .NET Core

Creación de una API, para gestionar una lista de productos. Cada producto debe de tener estos atributos.

En una tabla SQL
| Id  |  Name  |   Price |
| :-- | :----: | ------: |
| Int | String | Decimal |

Se usará MySQL para la gestión de la base de datos. Para ello se necesitará tener instalado MySQL de forma local, creando la base de datos **productos** en este caso.

# Database Configuration

En `appsettings.json`:

```bash
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=productos;User=root;Password=root1234;"
},
```

# Dtos

Se crearan Dtos para enviar al cliente los productos con los atributos deseados.

```cs
public record class ProductoDto(int Id, string Nombre, decimal Precio);
```

# Entities

Para manejar las base de datos necesitamos las entidades.

```cs
public class Producto
{
  public int Id { get; set; }
  public required string Name { get; set; }
  [Column(TypeName = "decimal(10,2)")]
  public required decimal Price { get; set; }
}
```

## DbContext

Para poder acceder a la base de datos y poder realizar el CRUD necesitamos del Context.

```cs
public class TiendaProductosContext(DbContextOptions<TiendaProductosContext> options) : DbContext(options)
{
  public DbSet<Producto> Productos => Set<Producto>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Producto>().HasData(
      new { Id = 1, Name = "Televisor", Price = 1599.99M },
      new { Id = 2, Name = "Laptop", Price = 2700.00M },
      new { Id = 3, Name = "Tablet", Price = 800.00M }
    );
  }
}
```

Se uso un seed, para poder inicializar la tabla, esto es bueno en el entorno de desarrollo para verificar y probar el servidor.

## Endpoints

Por último requerimos de los endpoints, primero creamos un *group* para **/productos**

```cs
var group = app.MapGroup("productos").WithParameterValidation();
```

### Método GET

```cs
 // GET /productos
group.MapGet("/", async (TiendaProductosContext dbContext) =>
{
  try
  {
    var productos = await dbContext.Productos.Select(producto => producto.ToDto()).AsNoTracking().ToListAsync();
    return Results.Ok(productos);
  }
  catch (Exception e)
  {
    return Results.Problem($"Ha ocurrido un error mientras se obtiene los productos: {e.Message}");
  }
});

// GET /productos/1
group.MapGet("/{id}", async (int id, TiendaProductosContext dbContext) =>
{
  try
  {
    Producto? producto = await dbContext.Productos.FindAsync(id);
    return producto is null ? Results.NotFound() : Results.Ok(producto);
  }
  catch (Exception e)
  {
    return Results.Problem($"Ha ocurrido un error mientras se obtiene el producto: {e.Message}");
  }

}).WithName(GetProductEndpoint);
```

### Método POST

```cs
// POST /productos
group.MapPost("/", async (CreateProductoDto newProduct, TiendaProductosContext dbContext) =>
{
  try
  {
    Producto producto = newProduct.ToEntity();

    dbContext.Productos.Add(producto);
    await dbContext.SaveChangesAsync();

    return Results.CreatedAtRoute(GetProductEndpoint, new { id = producto.Id }, producto.ToDto());
  }
  catch (Exception e)
  {
    return Results.Problem($"Ha ocurrido un error mientras se crea el producto: {e.Message}");

  }
});
```

### Método PUT

```cs
// PUT /productos/1
group.MapPut("/{id}", async (int id, UpdateProductoDto updatedProduct, TiendaProductosContext dbContext) =>
{
  try
  {
    var existingProduct = await dbContext.Productos.FindAsync(id);
    if (existingProduct is null)
    {
      return Results.NotFound();
    }

    dbContext.Entry(existingProduct).CurrentValues.SetValues(updatedProduct.ToEntity(id));
    await dbContext.SaveChangesAsync();
    return Results.NoContent();
  }
  catch (Exception e)
  {
    return Results.Problem($"Ha ocurrido un error mientras se actualiza el producto: {e.Message}");
  }
});
```

### Método DELETE

```cs
// DELETE /productos/1
group.MapDelete("/{id}", async (int id, TiendaProductosContext dbContext) =>
{
  try
  {
    var producto = await dbContext.Productos.FindAsync(id);
    if (producto is null)
    {
      return Results.NotFound();
    }

    dbContext.Productos.Remove(producto);
    await dbContext.SaveChangesAsync();
    return Results.NoContent();
  }
  catch (Exception e)
  {
    return Results.Problem($"Ha ocurrido un error mientras se elimina el producto: {e.Message}");
  }
});
```

## Setup

```bash
dotnet run
```

### Images

using Microsoft.EntityFrameworkCore;
using TiendaProductos.Api.Data;
using TiendaProductos.Api.Dtos;
using TiendaProductos.Api.Entities;
using TiendaProductos.Api.Mapping;

namespace TiendaProductos.Api.Endpoints;

public static class ProductosEndpoints
{
  const string GetProductEndpoint = "GetProduct";

  public static RouteGroupBuilder MapProductosEndpoints(this WebApplication app)
  {

    var group = app.MapGroup("productos").WithParameterValidation();

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
    return group;
  }
}

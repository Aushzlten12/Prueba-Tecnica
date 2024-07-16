using TiendaProductos.Api.Dtos;
using TiendaProductos.Api.Entities;

namespace TiendaProductos.Api.Mapping;

public static class ProductoMapping
{
  public static Producto ToEntity(this CreateProductoDto newProducto)
  {
    return new()
    {
      Name = newProducto.Name,
      Price = newProducto.Price,
    };
  }

  public static Producto ToEntity(this UpdateProductoDto newProducto, int id)
  {
    return new()
    {
      Id = id,
      Name = newProducto.Name,
      Price = newProducto.Price,
    };
  }

  public static ProductoDto ToDto(this Producto producto)
  {
    return new(
      producto.Id,
      producto.Name,
      producto.Price
    );
  }

}

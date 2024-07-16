using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaProductos.Api.Entities;

public class Producto
{
  public int Id { get; set; }
  public required string Name { get; set; }
  [Column(TypeName = "decimal(10,2)")]
  public required decimal Price { get; set; }
}

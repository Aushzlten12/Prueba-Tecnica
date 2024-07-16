using System.ComponentModel.DataAnnotations;

namespace TiendaProductos.Api.Dtos;

public record class CreateProductoDto
(
  [Required][StringLength(50)] string Name,
  [Range(1, 50000)] decimal Price
);
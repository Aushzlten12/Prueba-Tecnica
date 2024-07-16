using Microsoft.EntityFrameworkCore;
using TiendaProductos.Api.Entities;

namespace TiendaProductos.Api.Data;

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

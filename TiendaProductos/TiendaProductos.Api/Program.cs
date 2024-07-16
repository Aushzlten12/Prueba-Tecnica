using TiendaProductos.Api.Data;
using TiendaProductos.Api.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowSpecificOrigin",
    builder => builder.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader()
  );
});

builder.Services.AddDbContext<TiendaProductosContext>((serviceProvider, options) =>
{
  options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
                   new MySqlServerVersion(new Version(8, 0, 38)));
});
var app = builder.Build();
app.UseCors("AllowSpecificOrigin");
app.MapProductosEndpoints();
await app.MigrateDbAsync();
app.Run();

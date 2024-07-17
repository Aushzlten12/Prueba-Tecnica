# Prueba Técnica

Consta de dos carpetas, una para el Frontend y otra para el Backend.

## Para el backend

Se require tener instaldo .NET Core y MySQL localmente, dada esta configuración de MySQL.

```json 
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=productos;User=root;Password=root1234;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*"
}
```

Ademas de instalar algunos paquetes adicionales, como: **MinimalApis.Extensions**, **Pomelo.EntityFrameworkCore.MySql** y **Microsoft.EntityFrameworkCore.Design**, todos se pueden encontrar en https://www.nuget.org/

Con el comando:
```sh
dotnet run
```
Se ejecuta el servidor y se crean las migraciones siempre y cuando **DefaultConnection** exista de manera local.

## Para el Frontend

Se requiere tener instalado Nodejs y NPM. Al clonar el repositorio ejecutar.
```sh
npm install
```
Y teniendo ya el backend de manera local, primero debemos de ejcutarlo y luego ejecutar 
```sh
npm start
```
En la parte del Frotend para que este se pueda conectar al Backend.

## Nota
En cada carpeta hay una explicación más detallada de cada parte del proyecto.
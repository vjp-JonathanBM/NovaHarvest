# NovaHarvest

NovaHarvest es un proyecto web orientado al aprovechamiento de excedentes agrícolas del Valle del Jerte.

La aplicación está desarrollada con ASP.NET Core 10 y utiliza nginx como reverse proxy. Ambos servicios se ejecutan mediante Docker Compose.

## Arranque

### Requisitos

- Docker con Docker Compose.
- SDK de .NET 10 para ejecutar el backend sin Docker.

### Ejecutar con Docker

Desde la raíz del repositorio:

```bash
docker compose up -d --build
```

Abrir en el navegador:

- Inicio: http://localhost:8080/
- Acerca de: http://localhost:8080/about
- Estado: http://localhost:8080/status

Para comprobar los contenedores:

```bash
docker compose ps
```

Para detenerlos:

```bash
docker compose down
```

### Ejecutar el backend sin Docker

Entrar en la carpeta del proyecto:

```bash
cd backend/NovaHarvest.Web
dotnet run
```

Abrir la dirección local indicada en la terminal.

Para detener la aplicación, pulsar `Ctrl + C`.

## Arquitectura

La aplicación está formada por dos servicios:

- **web:** servidor nginx que recibe las peticiones del navegador en el puerto 8080 del ordenador y las reenvía al backend.
- **backend:** aplicación ASP.NET Core ejecutada mediante Kestrel en el puerto interno 8080. Genera las páginas HTML y sirve los archivos estáticos.

```text
Navegador
    |
    | http://localhost:8080
    v
nginx (web)
Puerto interno 80
    |
    | http://backend:8080
    v
ASP.NET Core (backend)
Kestrel - Puerto interno 8080
```

El backend no publica ningún puerto en el ordenador. Los dos servicios se comunican mediante la red interna de Docker Compose.

## Rutas

| Ruta | Descripción |
|---|---|
| `/` | Página principal de NovaHarvest. |
| `/about` | Información del proyecto y del autor. |
| `/status` | Fecha y hora del servidor. |
| `/css/site.css` | Hoja de estilos. |
| `/img/logo.png` | Logotipo. |

## Pruebas realizadas

Se ha comprobado que ambos contenedores arrancan correctamente y que nginx puede comunicarse con el backend.

Al detener el backend mediante `docker compose stop backend`, nginx devuelve el error HTTP **502 Bad Gateway**, porque no puede contactar con el servidor de destino.

Al volver a iniciar el backend, la aplicación puede responder nuevamente a las peticiones.
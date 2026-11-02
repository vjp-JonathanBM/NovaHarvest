# Cuestiones UT2 - NovaHarvest

## 1. Cliente y servidor

Al abrir `/status`, el navegador solicita la página y muestra el HTML recibido.  
nginx recibe la petición y la envía al backend.  
ASP.NET Core ejecuta el código C# y calcula la hora del servidor.  
La regla de no reservar más kilos de los disponibles debe comprobarse en el backend.  
Esto es necesario porque el usuario puede modificar los datos enviados desde el navegador.

## 2. Estático o dinámico

Las páginas `/` y `/about` podrían ser estáticas porque muestran información fija.  
En este proyecto se generan con C# porque lo exige el ejercicio.  
`/status` es dinámica porque muestra la hora actual del servidor.  
También sería dinámica una página con los productos disponibles.  
Otra sería la página de reservas de cada usuario.

## 3. Ejecución en el servidor

El navegador solicita `localhost:8080/status`.  
nginx recibe la petición y la reenvía a `backend:8080`.  
Kestrel ejecuta la ruta `/status` definida en `Program.cs`.  
Con CGI, el servidor ejecutaría un programa externo para procesar la petición.  
Con un módulo embebido, el código se ejecutaría dentro del proceso del servidor web.

## 4. Reverse proxy

Con Docker, la cabecera `Server` debería indicar `nginx`.  
Con `dotnet run`, debería indicar `Kestrel`, porque accedemos directamente al backend.  
Al detener el backend, `/status` y `/css/site.css` devuelven `502 Bad Gateway`.  
Esto ocurre porque nginx no puede comunicarse con ASP.NET Core.  
El backend no publica puertos para que solo sea accesible desde la red interna de Docker.

## 5. nginx

nginx no sirve directamente el CSS ni el logotipo porque está configurado como reverse proxy.  
Todas las peticiones se envían al backend ASP.NET Core.  
Los archivos están guardados en la carpeta `wwwroot`.  
En Internet, nginx podría gestionar conexiones HTTPS.  
También podría repartir las peticiones entre varios servidores.

## 6. Tecnologías

Si cambiáramos ASP.NET Core por otra tecnología de servidor, habría que modificar el backend.  
Cambiarían `Program.cs`, el archivo del proyecto y el `backend/Dockerfile`.  
La nueva aplicación tendría que mantener las mismas rutas.  
`compose.yaml` podría mantenerse si el backend siguiera escuchando en el puerto 8080.  
`web/nginx.conf` y el `Dockerfile` de nginx podrían seguir iguales.

## 7. Marcado y código

En `Program.cs` genero HTML utilizando cadenas de texto, por ejemplo:

```csharp
string html = $"""
    <h1>Estado del servidor</h1>
    <p>Fecha y hora: {currentDate}</p>
    """;
```

Un problema es que mezclar HTML y C# dificulta la lectura del código.  
Otro problema es que se repite mucho HTML entre las páginas.  
En las tres rutas se repiten el encabezado, el menú y el pie.  
Si cambiamos el menú, tenemos que modificarlo en las tres páginas.

## 8. Docker

Mi proyecto utiliza dos imágenes y dos contenedores principales: `web` y `backend`.  
Una imagen contiene los archivos necesarios para crear un contenedor.  
Un contenedor es una instancia de esa imagen en ejecución.  
La imagen final del backend no incluye el SDK porque solo necesita ejecutar la aplicación.  
Sin `.dockerignore`, Docker enviaría también `bin/` y `obj/` al construir la imagen.

## 9. Herramientas

| Herramienta | Para qué la he utilizado |
|---|---|
| VS Code | Escribir y modificar el código. |
| C# | Generar las páginas dinámicas. |
| ASP.NET Core | Crear las rutas de la aplicación. |
| Kestrel | Ejecutar el servidor HTTP del backend. |
| nginx | Reenviar las peticiones al backend. |
| Docker | Ejecutar la aplicación en contenedores. |
| Docker Compose | Arrancar los dos servicios juntos. |
| Git | Guardar las fases y cambios del proyecto. |

## 10. Observación

Con `dotnet run`, `/status` muestra la hora del equipo donde ejecuto .NET.  
Con Docker, muestra la hora del entorno del contenedor.  
Las horas pueden coincidir o tener diferencias por la zona horaria configurada.  
En ambos casos, `DateTime.Now` obtiene la hora local del entorno de ejecución.  
**Resultado de mi prueba:** [escribir aquí si coincidieron y qué horas aparecieron].
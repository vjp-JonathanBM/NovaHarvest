var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapStaticAssets();


app.MapGet("/", () =>
{
    string html = $"""
        <!DOCTYPE html>
        <html lang="es">
        <head>
            <meta charset="UTF-8">
            <title>NovaHarvest - Inicio</title>
            <link rel="stylesheet" href="/css/site.css">
        </head>
        <body>
            <header>
                <img src="/img/logo.png" alt="Logotipo de NovaHarvest" width="80">
                <h2>NovaHarvest</h2>
                <nav>
                    <a href="/">Inicio</a>
                    <a href="/about">Acerca de</a>
                    <a href="/status">Estado</a>
                </nav>
            </header>

            <main>
                <h1>Inicio</h1>
                <p>NovaHarvest es un proyecto para aprovechar los excedentes agrícolas del Valle del Jerte.</p>
                <p>Conectará a productores con compradores interesados en productos disponibles.</p>
                <p>Su objetivo es reducir el desperdicio alimentario y facilitar el aprovechamiento de las cosechas.</p>
            </main>

            <footer>
                NovaHarvest - IES Valle del Jerte - {DateTime.Now.Year}
            </footer>
        </body>
        </html>
        """;

    return Results.Content(html, "text/html; charset=utf-8");
});


app.MapGet("/about", () =>
{
    return Results.Content(
        "<h1>Acerca de</h1><p>Proyecto del IES Valle del Jerte. Versión 0.1.0.</p>",
        "text/html"
    );
});

app.MapGet("/status", () =>
{
    string currentDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

    return Results.Content(
        $"<h1>Estado del servidor</h1><p>{currentDate}</p><p>Pagina generada por ASP.NET Core (Kestrel)</p>",
        "text/html"
    );
});
app.Run();

using System.Net;

namespace Auth.Routes;

public static class Pages
{

    private static string? Index { get; set; }
    public static void AddPagesRouter(this WebApplication app)
    {
        AddIndexRoute(app);
    }

    public static void AddIndexRoute(WebApplication app)
    {
        var GetIndex = () =>
        {

            if (string.IsNullOrEmpty(Index))
            {
                Index = GetPage(Directory.GetCurrentDirectory(), "wwwroot", "index.html");
            }
            if (string.IsNullOrEmpty(Index))
            {
                return Results.NotFound();
            }


            return Results.Text(content: Index,
                contentType: "text/html",
                statusCode: (int?)HttpStatusCode.OK);
        };
        app.MapGet("/", GetIndex);
        app.MapFallback(GetIndex);
    }

    private static string? GetPage(params string[] path)
    {
        var filePath = Path.Combine(path);
        Console.WriteLine(filePath);
        Console.WriteLine(System.IO.File.Exists(filePath));
        if (System.IO.File.Exists(filePath))
        {
            using FileStream fileStream = new(filePath, FileMode.Open);
            using StreamReader reader = new(fileStream);
            return reader.ReadToEnd();
        }
        else
        {
            return null;
        }
    }
}

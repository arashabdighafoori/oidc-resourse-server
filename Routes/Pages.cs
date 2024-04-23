using auth.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Dynamic;
using System.Net;

namespace auth.Routes;

public static class Pages
{

    private static string? Index { get; set; }
    public static void AddPagesRoute(this WebApplication app)
    {
        app.AddIndexRoute();
    }

    public static void AddIndexRoute(this WebApplication app)
    {
        var GetIndex = () =>
        {

            if (string.IsNullOrEmpty(Index))
            {
                Index = GetPage(Directory.GetCurrentDirectory(), "wwwroot", "index.html");
            }
            if (string.IsNullOrEmpty(Index))
            {
                Console.WriteLine("wow");
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

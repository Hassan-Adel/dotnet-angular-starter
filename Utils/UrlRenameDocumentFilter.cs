using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace dotnet_angular_starter.Utils;

public class UrlRenameDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        OpenApiPaths keyValuePairs = new OpenApiPaths();
        foreach (var path in swaggerDoc.Paths)
        {
            var value = path.Value;

            // here you have to put logic to convert name to camelCase
            string newkey = path.Key.ToUpper();

            keyValuePairs.Add(newkey, value);
        }
        swaggerDoc.Paths = keyValuePairs;
    }
}

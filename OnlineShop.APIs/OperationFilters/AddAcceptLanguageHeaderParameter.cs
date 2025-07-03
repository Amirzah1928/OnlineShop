using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using OnlineShop.APIs.Features;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OnlineShop.APIs.OperationFilters
{
    public class AddAcceptLanguageHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var languages = Enum
           .GetValues<Languages>()
           .Select(x => new OpenApiString(x.ToString()));


            operation.Parameters ??= [];

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Accept-Language",
                In = ParameterLocation.Header,
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Enum = languages.Cast<IOpenApiAny>().ToList()
                }
            });
        }
    }
}

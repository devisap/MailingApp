using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AssetManagement.Utilities
{
    public class SwaggerFilterAuthorized : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Check if the endpoint has the Authorize attribute
            var hasAuthorize = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                                 .OfType<AuthorizeAttribute>().Any() ||
                               context.MethodInfo.GetCustomAttributes(true)
                                 .OfType<AuthorizeAttribute>().Any();

            if (hasAuthorize)
            {
                operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                }
            };
            }
        }
    }
}
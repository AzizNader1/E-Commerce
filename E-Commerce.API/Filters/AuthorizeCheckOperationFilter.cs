using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class AuthorizeCheckOperationFilter : IOperationFilter
{
    /// <summary>
    /// This method applies the authorization check to the OpenAPI operation. It checks if the method or its declaring type has the Authorize attribute. If it does, it adds a security requirement to the operation, indicating that the operation requires authentication using a bearer token. This is done by adding an OpenApiSecurityRequirement to the operation's Security property, which references a security scheme with the ID "Bearer". This allows Swagger to understand that this operation requires authentication and will prompt users to provide a token when trying to access it through the Swagger UI.
    /// </summary>
    /// <remarks>
    /// The method first checks for the presence of the Authorize attribute on both the method and its declaring type. If neither has the attribute, it simply returns without modifying the operation. If the Authorize attribute is found, it constructs a security requirement that references a bearer token authentication scheme and adds it to the operation's security requirements. This ensures that any API endpoint decorated with the Authorize attribute will be properly documented in Swagger as requiring authentication.
    /// </remarks>
    /// <param name="operation"></param>
    /// <param name="context"></param>
    /// <Return>
    /// An OpenApiOperation object that has been modified to include security requirements if the Authorize attribute is present on the method or its declaring type. If the Authorize attribute is not present, the operation is returned unmodified.
    /// </Return>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var hasAuthorize =
            context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                .OfType<AuthorizeAttribute>().Any()
            || context.MethodInfo.GetCustomAttributes(true)
                .OfType<AuthorizeAttribute>().Any();

        if (!hasAuthorize)
            return;

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
                    new List<string>()
                }
            }
        };
    }
}
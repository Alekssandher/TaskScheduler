namespace TaskScheduler.API.Extensions
{
    public static class DocumentationExtension
    {
        public static void AddDocumentation(this IServiceCollection services)
        {
            services.AddOpenApi(options =>
            {
                options.AddDocumentTransformer((document, context, cancellationToken) =>
                {
                    document.Info = new()
                    {
                        Title = "TaskScheduler",
                        Version = "v1",
                        Description = "API to Schedule Tasks."
                    };

                    document.Components ??= new();

                    document.Components.SecuritySchemes["Bearer"] = new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\""
                    };

                    return Task.CompletedTask;
                });
            });
        }
    
    }
}
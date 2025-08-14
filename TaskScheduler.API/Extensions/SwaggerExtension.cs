namespace TaskScheduler.API.Extensions
{
    public static class SwaggerExtension
    {
        public static void AddSwaggerDocumentation(this IServiceCollection services)
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
                    return Task.CompletedTask;
                });
            });
        }
    
    }
}
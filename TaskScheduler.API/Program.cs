using Scalar.AspNetCore;
using TaskScheduler.API.Extensions;
using TaskScheduler.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDocumentation();
builder.Services.RegisterDependencies(builder.Configuration);
builder.Services.RegisterAuthorization(builder.Configuration);

builder.Services.AddHealthChecks();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi().CacheOutput();
    app.MapScalarApiReference("/docs");
    app.MapHealthChecks("/health");
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();

app.Run();

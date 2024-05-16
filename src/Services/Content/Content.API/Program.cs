using Content.API.Middleware;
using Content.Application;
using Content.Infrastructure;
using Content.Infrastructure.DataProvider;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Content.API;

abstract class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(name: "v1", new OpenApiInfo() { Title = "Content.API", Version = "v1" });
        });

        builder.Services.AddControllers();

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ContentDbContext>();
            context.Database.Migrate();
        }

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "Content.API v1");
            });
        }

        app.UseCors(options =>
        {
            options.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
        app.MapControllers();
        app.UseCustomExceptionHandler();
        app.Run();
    }
}
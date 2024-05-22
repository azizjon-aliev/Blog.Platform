using Content.API.Middleware;
using Content.Application;
using Content.Infrastructure;
using Content.Infrastructure.DataProvider;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
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

        // Swagger
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(name: "v1", new OpenApiInfo() { Title = "Content.API", Version = "v1" });


            // c.AddSecurityDefinition("AuthToken 1",
            //     new OpenApiSecurityScheme
            //     {
            //         In = ParameterLocation.Header,
            //         Type = SecuritySchemeType.Http,
            //         BearerFormat = "JWT",
            //         Scheme = "bearer",
            //         Name = "Authorization",
            //         Description = "Authorization token"
            //     });

            // c.AddSecurityRequirement(new OpenApiSecurityRequirement
            // {
            //     {
            //         new OpenApiSecurityScheme
            //         {
            //             Reference = new OpenApiReference
            //             {
            //                 Type = ReferenceType.SecurityScheme,
            //                 Id = $"AuthToken 1"
            //             }
            //         },
            //         new string[] { }
            //     }
            // });
        });

        // builder.Services.AddAuthentication(config =>
        // {
        //     config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //     config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        // }).AddJwtBearer(options =>
        // {
        //     options.Authority = builder.Configuration["JwtSettings:Authority"];
        //     options.Audience = builder.Configuration["JwtSettings:Audience"];
        //     options.RequireHttpsMetadata = false;
        // });

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
                c.RoutePrefix = string.Empty;
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
        // app.UseAuthentication();
        // app.UseAuthorization();
        app.Run();
    }
}
using Microsoft.AspNetCore.Authentication.JwtBearer;
using OpenIdConnectDemo.Api.Repositories;
using OpenIdConnectDemo.Api.Services;
using Scalar.AspNetCore;

namespace OpenIdConnectDemo.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
        });

        var connectionString = "Data Source=books.db";
        builder.Services.AddSingleton(_ => new BookRepository(connectionString: connectionString));
        builder.Services.AddSingleton<DatabaseInitializer>();
        builder.Services.AddSingleton<BookService>();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
        {
            options.Authority = "https://localhost:5001";
            options.Audience = "bookstoreapi";
            options.TokenValidationParameters = new()
            {
                ValidTypes = ["at+jwt"]
            };
        });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("CanWrite", policy =>
            {
                policy.RequireClaim("scope", "bookstoreapi.write");
            });

            options.AddPolicy("CanRead", policy =>
            {
                policy.RequireClaim("scope", "bookstoreapi.read");
            });
        });

        builder.Services.AddControllers();
        builder.Services.AddOpenApi();

       

        var app = builder.Build();

        app.UseCors("AllowAll");

        app.MapOpenApi();
        app.MapScalarApiReference(options =>
        {
            options.DarkMode = false;
        });

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        using (var scope = app.Services.CreateScope())
        {
            var initializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
            await  initializer.InitializeAsync(); // Create database and seed data
        }

        app.Run();
    }
}

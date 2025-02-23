using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Net.Http.Headers;
using OpenIdConnectDemo.Client.Services;

namespace OpenIdConnectDemo.Client;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
        }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
        {
            options.AccessDeniedPath = "/Authentication/AccessDenied";
        })
         .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
          {
              options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
              options.Authority = "https://localhost:5001";
              options.ClientId = "bookstore";
              options.ClientSecret = "secret";
              options.ResponseType = "code";
              options.SaveTokens = true;
              options.GetClaimsFromUserInfoEndpoint = true;
              options.Scope.Add("roles");
              options.Scope.Add("bookstoreapi.read");
              options.Scope.Add("bookstoreapi.write");
              options.ClaimActions.MapJsonKey("role", "role");
              options.TokenValidationParameters = new()
              {
                  NameClaimType = "name",
                  RoleClaimType = "role"
              };
          });

        builder.Services.AddAccessTokenManagement();

        builder.Services.AddSingleton<BookService>();
        builder.Services.AddHttpClient("APIClient", client =>
      {
          client.BaseAddress = new Uri("https://localhost:5000");
          client.DefaultRequestHeaders.Clear();
          client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
      }).AddUserAccessTokenHandler();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}

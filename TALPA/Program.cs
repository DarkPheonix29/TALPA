using Auth0.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = builder.Configuration["Auth0:Domain"];
    options.ClientId = builder.Configuration["Auth0:ClientId"];
});

// Expose Connection String for Database
string connectionString = builder.Configuration.GetConnectionString("TALPADB");
DAL.ConnectionManager.Initialize(connectionString); 

//build the app
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
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

app.MapControllerRoute(
    name: "uitje",
    pattern: "uitje",
    defaults: new { controller = "App", action = "Activity" }
);

app.MapControllerRoute(
    name: "suggesties",
    pattern: "suggesties",
    defaults: new { controller = "App", action = "Suggestions" }
);

app.MapControllerRoute(
    name: "stemmen",
    pattern: "stemmen",
    defaults: new { controller = "App", action = "Poll" }
);

app.MapControllerRoute(
    name: "uitloggen",
    pattern: "uitloggen",
    defaults: new { controller = "Account", action = "Logout" }
);

app.Run();

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
string connectionStringMySQL = builder.Configuration.GetConnectionString("LocalMySQL");
DAL.ConnectionManager.Initialize(connectionString);
DAL.SQLConnectionManager.Initialize(connectionStringMySQL);

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
    defaults: new { controller = "App", action = "Uitje" }
);

app.MapControllerRoute(
    name: "suggesties",
    pattern: "suggesties",
    defaults: new { controller = "App", action = "Suggesties" }
);

app.MapControllerRoute(
    name: "stemmen",
    pattern: "stemmen",
    defaults: new { controller = "App", action = "Stemmen" }
);

app.MapControllerRoute(
    name: "logout",
    pattern: "logout",
    defaults: new { controller = "Account", action = "Logout" }
);

app.MapControllerRoute(
	name: "BevestigEmail",
	pattern: "BevestigEmail",
	defaults: new { controller = "Account", action = "VerifyEmail" }
);

app.Run();

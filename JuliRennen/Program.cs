using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using JuliRennen.Data;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(3000);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", options =>
{
    options.Cookie.Name = "MyCookieAuth";
    options.AccessDeniedPath = "/AccessDenied";
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly",
        policy => policy.RequireClaim(ClaimTypes.Name, "admin"));
});

builder.Services.AddRazorPages();
builder.Services.AddDbContext<JuliRennenContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("JuliRennenContext") ?? throw new InvalidOperationException("Connection string 'JuliRennenContext' not found.")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseSession();

app.MapRazorPages();

app.MapControllerRoute(
    name: "Route",
    pattern: "{controller=Route}/{action=Index}/");

app.Run();

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Parcial2.Data;
using Parcial2.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ArticuloContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ArticuloContext") ?? throw new InvalidOperationException("Connection string 'ArticuloContext' not found.")));

// Add services to the container.
builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ArticuloContext>();

builder.Services.AddControllersWithViews();

//Acá inyectamos nuestra interfaz
builder.Services.AddScoped<ILocalService, LocalService>();
builder.Services.AddScoped<IArticuloService, ArticuloService>();


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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

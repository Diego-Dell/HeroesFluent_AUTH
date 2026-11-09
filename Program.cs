using HeroesFluent.Data;
using HeroesFluent.Seguridad;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<HeroesDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HeroesFluent")));

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.User.RequireUniqueEmail = true;
        options.Password.RequiredLength = 8;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<HeroesDbContext>();

builder.Services.AddAuthorization(options =>
{
    foreach (var permiso in new[]
    {
        Permisos.CrearHeroes,
        Permisos.EditarHeroes,
        Permisos.EliminarHeroes
    })
    {
        options.AddPolicy(permiso, policy => policy
            .RequireAuthenticatedUser()
            .RequireClaim(Permisos.TipoClaim, permiso));
    }
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});

var app = builder.Build();

var cantidadHeroes = app.Configuration.GetValue<int>("DatosIniciales:CantidadHeroes");
ArgumentOutOfRangeException.ThrowIfNegative(cantidadHeroes);

if (app.Environment.IsDevelopment())
{
    await using var scope = app.Services.CreateAsyncScope();
    var context = scope.ServiceProvider.GetRequiredService<HeroesDbContext>();
    await context.Database.MigrateAsync();
    if (cantidadHeroes > 0)
    {
        await HeroesSeeder.InicializarAsync(context, cantidadHeroes);
    }
    await SeguridadSeeder.InicializarAsync(scope.ServiceProvider);
}
else if (cantidadHeroes > 0)
{
    await using var scope = app.Services.CreateAsyncScope();
    var context = scope.ServiceProvider.GetRequiredService<HeroesDbContext>();
    await context.Database.MigrateAsync();
    await HeroesSeeder.InicializarAsync(context, cantidadHeroes);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Heroes}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

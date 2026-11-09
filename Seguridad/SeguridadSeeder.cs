using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace HeroesFluent.Seguridad;

public static class SeguridadSeeder
{
    public static async Task InicializarAsync(IServiceProvider services)
    {
        var roles = services.GetRequiredService<RoleManager<IdentityRole>>();
        var usuarios = services.GetRequiredService<UserManager<IdentityUser>>();

        foreach (var nombre in new[] { "Administrador", "Editor", "Lector" })
        {
            if (!await roles.RoleExistsAsync(nombre))
            {
                Comprobar(await roles.CreateAsync(new IdentityRole(nombre)));
            }
        }

        var cuentas = new[]
        {
            (Correo: "admin@heroes.local", Rol: "Administrador",
                Permisos: new[] { Permisos.CrearHeroes,
                    Permisos.EditarHeroes, Permisos.EliminarHeroes }),
            (Correo: "editor@heroes.local", Rol: "Editor",
                Permisos: new[] { Permisos.CrearHeroes, Permisos.EditarHeroes }),
            (Correo: "lector@heroes.local", Rol: "Lector",
                Permisos: Array.Empty<string>())
        };

        foreach (var cuenta in cuentas)
        {
            var usuario = await usuarios.FindByEmailAsync(cuenta.Correo);
            if (usuario == null)
            {
                usuario = new IdentityUser
                {
                    UserName = cuenta.Correo,
                    Email = cuenta.Correo,
                    EmailConfirmed = true
                };
                Comprobar(await usuarios.CreateAsync(usuario, "Heroes2026!"));
            }

            if (!await usuarios.IsInRoleAsync(usuario, cuenta.Rol))
            {
                Comprobar(await usuarios.AddToRoleAsync(usuario, cuenta.Rol));
            }

            var actuales = await usuarios.GetClaimsAsync(usuario);
            foreach (var permiso in cuenta.Permisos)
            {
                if (!actuales.Any(c => c.Type == Permisos.TipoClaim && c.Value == permiso))
                {
                    Comprobar(await usuarios.AddClaimAsync(usuario,
                        new Claim(Permisos.TipoClaim, permiso)));
                }
            }
        }
    }

    private static void Comprobar(IdentityResult resultado)
    {
        if (!resultado.Succeeded)
        {
            throw new InvalidOperationException(string.Join("; ",
                resultado.Errors.Select(e => e.Description)));
        }
    }
}

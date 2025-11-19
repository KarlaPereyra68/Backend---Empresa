using Microsoft.EntityFrameworkCore;
using empresaAPI.Data;
using empresaAPI.Models;

namespace empresaAPI.Endpoints;

public static class ContactosEndpoints
{
    public static void MapContactos(this WebApplication app)
    {
        var group = app.MapGroup("/api/contactos");

        group.MapGet("/", async (EmpresaContext db) => await db.Contactos.ToListAsync())
            .WithName("GetContactos");

        group.MapGet("/{id:int}", async (int id, EmpresaContext db) =>
        {
            var contacto = await db.Contactos.FindAsync(id);
            return contacto is not null ? Results.Ok(contacto) : Results.NotFound();
        }).WithName("GetContactoById");

        group.MapPost("/", async (Contacto contacto, EmpresaContext db) =>
        {
            db.Contactos.Add(contacto);
            await db.SaveChangesAsync();
            return Results.Created($"/api/contactos/{contacto.ID}", contacto);
        }).WithName("CreateContacto");

        group.MapPut("/{id:int}", async (int id, Contacto updated, EmpresaContext db) =>
        {
            var contacto = await db.Contactos.FindAsync(id);
            if (contacto is null) return Results.NotFound();

            contacto.Nombre = updated.Nombre;
            contacto.Apellido = updated.Apellido;
            contacto.Telefono = updated.Telefono;
            contacto.Correo = updated.Correo;

            await db.SaveChangesAsync();
            return Results.NoContent();
        }).WithName("UpdateContacto");

        group.MapDelete("/{id:int}", async (int id, EmpresaContext db) =>
        {
            var contacto = await db.Contactos.FindAsync(id);
            if (contacto is null) return Results.NotFound();

            db.Contactos.Remove(contacto);
            await db.SaveChangesAsync();
            return Results.NoContent();
        }).WithName("DeleteContacto");
    }
}

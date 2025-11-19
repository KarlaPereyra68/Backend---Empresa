using Microsoft.EntityFrameworkCore;
using empresaAPI.Models;

namespace empresaAPI.Data;

public class EmpresaContext : DbContext
{
    public EmpresaContext(DbContextOptions<EmpresaContext> options) : base(options)
    {
    }

    public DbSet<Contacto> Contactos { get; set; } = null!;
}

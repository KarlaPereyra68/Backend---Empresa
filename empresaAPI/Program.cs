var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core + SQLite
using Microsoft.EntityFrameworkCore;
using empresaAPI.Data;
using empresaAPI.Endpoints;

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=empresa.db";
builder.Services.AddDbContext<EmpresaContext>(options => options.UseSqlite(connectionString));

var app = builder.Build();

// Ensure database created
using (var scope = app.Services.CreateScope())
{
	var db = scope.ServiceProvider.GetRequiredService<EmpresaContext>();
	db.Database.EnsureCreated();
}

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Hello World!");

// Map Contactos endpoints
app.MapContactos();

app.Run();

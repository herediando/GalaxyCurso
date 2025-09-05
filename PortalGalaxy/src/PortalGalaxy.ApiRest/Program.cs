using Microsoft.EntityFrameworkCore;
using PortalGalaxy.DataAccess;
using PortalGalaxy.Repositories.Implementaciones;
using PortalGalaxy.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();

builder.Services.AddDbContext<PortalGalaxyDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("PortalGalaxy"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("api/Categorias", async (ICategoriaRepository repository) =>
{
    var categorias = await repository.ListAsync();

    return Results.Ok(categorias);
});

app.MapGet("api/CategoriasList", async (string filtro, ICategoriaRepository repository) =>
{
    var categorias = await repository.ListAsync(p => p.Nombre.Contains(filtro));

    return Results.Ok(categorias);
});

app.Run();

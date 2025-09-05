using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalGalaxy.Common;
using PortalGalaxy.Entities;

namespace PortalGalaxy.DataAccess.Configurations;

public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.ToTable(nameof(Categoria));

        builder.HasData(new List<Categoria>
        {
            new() { Id = 1, Nombre = ".NET", FechaCreacion = Convert.ToDateTime(Constantes.FechaCreacionDefault)}
        });
    }
}
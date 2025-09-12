using System;
using PortalGalaxy.DataAccess;
using PortalGalaxy.Entities;
using PortalGalaxy.Entities.Infos;
using PortalGalaxy.Repositories.Interfaces;

namespace PortalGalaxy.Repositories.Implementaciones;

public class TallerRepository : RepositoryBase<Taller>, ITallerRepository
{
    public TallerRepository(PortalGalaxyDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<(ICollection<TallerInfo> Collection, int Total)> ListAsync(string? nombre, int? categoria, int? situacion, int pageNumber, int pageSize)
    {
        var tupla = await ListAsync(predicate: p => p.Nombre.Contains(nombre ?? string.Empty) &&
                                                     (!categoria.HasValue || p.CategoriaId == categoria.Value) &&
                                                     (!situacion.HasValue || p.Situacion == (SituacionTaller)situacion.Value),
                                     selector: s => new TallerInfo
                                     {
                                         Id = s.Id,
                                         Taller = s.Nombre,
                                         Categoria = s.Categoria.Nombre,
                                         Instructor = s.Instructor.Nombres,
                                         Fecha = s.FechaInicio,
                                         Situacion = s.Situacion.ToString().Replace("_", " ")
                                     },
                                     orderBy: o => o.Id,
                                     relations: "Categoria,Instructor",
                                     pageNumber: pageNumber,
                                     pageSize: pageSize);

        return tupla;
    }
}
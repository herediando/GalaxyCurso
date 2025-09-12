using System;
using PortalGalaxy.Entities;
using PortalGalaxy.Entities.Infos;

namespace PortalGalaxy.Repositories.Interfaces;

public interface ITallerRepository : IRepositoryBase<Taller>
{
    Task<(ICollection<TallerInfo> Collection, int Total)> ListAsync(string? nombre, int? categoria,
        int? situacion, int pageNumber, int pageSize);
}

using System;
using PortalGalaxy.Common.Response;

namespace PortalGalaxy.Services.Interfaces;

public interface ITallerService
{
    Task<PaginationResponse<TallerDtoResponse>> ListAsync(string? nombre, int? categoria, int? situacion, int pageNumber, int pageSize);
}

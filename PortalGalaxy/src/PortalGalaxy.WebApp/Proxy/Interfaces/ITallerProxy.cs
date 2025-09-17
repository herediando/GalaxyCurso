using PortalGalaxy.Common.Response;

namespace PortalGalaxy.WebApp.Proxy.Interfaces;

public interface ITallerProxy
{
    Task<PaginationResponse<TallerDtoResponse>> ListAsync(string? nombre, int? categoriaId, int? situacion, int pageNumber = 1, int pageSize = 5);
}
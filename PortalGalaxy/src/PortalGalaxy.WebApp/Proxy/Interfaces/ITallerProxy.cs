using PortalGalaxy.Common.Request;
using PortalGalaxy.Common.Response;

namespace PortalGalaxy.WebApp.Proxy.Interfaces;

public interface ITallerProxy : ICrudRestHelper<TallerDtoRequest, TallerDtoResponse>
{
    Task<PaginationResponse<TallerDtoResponse>> ListAsync(string? nombre, int? categoriaId, int? situacion, int pageNumber = 1, int pageSize = 5);

    Task<BaseResponse<ICollection<TallerSimpleDtoResponse>>> ListarAsync();

    Task<Stream> ExportarPdf(BusquedaTallerRequest request);
}
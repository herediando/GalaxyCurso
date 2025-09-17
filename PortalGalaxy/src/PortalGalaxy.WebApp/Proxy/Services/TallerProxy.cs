using PortalGalaxy.Common.Response;
using PortalGalaxy.WebApp.Proxy.Interfaces;

namespace PortalGalaxy.WebApp.Proxy.Services;

public class TallerProxy : RestBase, ITallerProxy
{
    public TallerProxy(HttpClient httpClient) 
        : base("api/talleres", httpClient)
    {
    }

    public async Task<PaginationResponse<TallerDtoResponse>> ListAsync(string? nombre, int? categoriaId, int? situacion, 
        int pageNumber = 1, int pageSize = 15)
    {
        var data = await SendAsync<PaginationResponse<TallerDtoResponse>>(
            $"?nombre={nombre}&categoria={categoriaId}&situacion={situacion}&pageNumber={pageNumber}&pageSize={pageSize}");

        return data;
    }
}
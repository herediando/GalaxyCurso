using PortalGalaxy.Common.Response;

namespace PortalGalaxy.Services.Interfaces;

public interface ICategoriaService
{
    Task<BaseResponse<ICollection<CategoriaDtoResponse>>> ListAsync();
}
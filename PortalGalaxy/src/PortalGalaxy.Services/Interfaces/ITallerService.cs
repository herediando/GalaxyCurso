using System;
using PortalGalaxy.Common.Request;
using PortalGalaxy.Common.Response;

namespace PortalGalaxy.Services.Interfaces;

public interface ITallerService
{
    Task<PaginationResponse<TallerDtoResponse>> ListAsync(BusquedaTallerRequest request);
}

using System;
using PortalGalaxy.Common.Request;
using PortalGalaxy.Common.Response;

namespace PortalGalaxy.Services.Interfaces;

public interface ITallerService
{
    Task<PaginationResponse<TallerDtoResponse>> ListAsync(BusquedaTallerRequest request);

    Task<BaseResponse> AddAsync(TallerDtoRequest request);

    Task<BaseResponse<TallerDtoRequest>> FindByIdAsync(int id);

    Task<BaseResponse> UpdateAsync(int id, TallerDtoRequest request);

    Task<BaseResponse> DeleteAsync(int id);
}

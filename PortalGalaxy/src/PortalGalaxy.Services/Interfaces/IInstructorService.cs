using System;
using PortalGalaxy.Common.Response;

namespace PortalGalaxy.Services.Interfaces;

public interface IInstructorService
{  
    Task<BaseResponse<ICollection<InstructorDtoResponse>>> ListAsync(string? filtro, string? nroDocumento, int? categoriaId);
}

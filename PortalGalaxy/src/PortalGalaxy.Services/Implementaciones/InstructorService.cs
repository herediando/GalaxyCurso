using System;
using AutoMapper;
using Microsoft.Extensions.Logging;
using PortalGalaxy.Common.Response;
using PortalGalaxy.Repositories.Interfaces;
using PortalGalaxy.Services.Interfaces;

namespace PortalGalaxy.Services.Implementaciones;

public class InstructorService : IInstructorService
{
    private readonly IInstructorRepository _repository;
    private readonly ILogger<InstructorService> _logger;
    private readonly IMapper _mapper;
    public InstructorService(IInstructorRepository repository, ILogger<InstructorService> logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<BaseResponse<ICollection<InstructorDtoResponse>>> ListAsync(string? filtro, string? nroDocumento,
    int? categoriaId)
    {
        var response = new BaseResponse<ICollection<InstructorDtoResponse>>();
        
        try
        {
            var lista = await _repository.ListAsync(filtro, nroDocumento, categoriaId);
            response.Data = _mapper.Map<ICollection<InstructorDtoResponse>>(lista);
            response.Success = true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in InstructorService.ListAsync");
            response.Success = false;
            response.ErrorMessage = "Error al listar los instructores.";
        }

        return response;
    }
}

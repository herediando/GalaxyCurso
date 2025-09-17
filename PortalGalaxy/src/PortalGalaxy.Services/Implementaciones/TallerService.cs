using System;
using AutoMapper;
using Microsoft.Extensions.Logging;
using PortalGalaxy.Common.Request;
using PortalGalaxy.Common.Response;
using PortalGalaxy.Repositories.Interfaces;
using PortalGalaxy.Services.Interfaces;
using PortalGalaxy.Services.Utils;

namespace PortalGalaxy.Services.Implementaciones;

public class TallerService : ITallerService
{
    private readonly ITallerRepository _repository;
    private readonly ILogger<TallerService> _logger;
    private readonly IMapper _mapper;

    public TallerService(ITallerRepository repository, ILogger<TallerService> logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<PaginationResponse<TallerDtoResponse>> ListAsync(BusquedaTallerRequest request)
    {
        var response = new PaginationResponse<TallerDtoResponse>();

        try
        {
            var (lista, total) = await _repository.ListAsync(request.Nombre, request.Categoria, 
                request.Situacion, request.PageNumber, request.PageSize);
            response.Data = _mapper.Map<ICollection<TallerDtoResponse>>(lista);
            response.TotalPages = Helper.GetTotalPages(total, request.PageSize); // Calcular el total de páginas
            response.Success = true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in TallerService.ListAsync");
            response.Success = false;
            response.ErrorMessage = "Error al listar los talleres.";
        }

        return response;
    }
}

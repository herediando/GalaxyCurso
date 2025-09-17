using AutoMapper;
using Microsoft.Extensions.Logging;
using PortalGalaxy.Common.Response;
using PortalGalaxy.Repositories.Interfaces;
using PortalGalaxy.Services.Interfaces;

namespace PortalGalaxy.Services.Implementaciones;

public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaRepository _repository;
    private readonly ILogger<CategoriaService> _logger;
    private readonly IMapper _mapper;

    public CategoriaService(ICategoriaRepository repository, ILogger<CategoriaService> logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }
    public async Task<BaseResponse<ICollection<CategoriaDtoResponse>>> ListAsync()
    {
        var response = new BaseResponse<ICollection<CategoriaDtoResponse>>();
        try
        {
            var data = await _repository.ListAsync();
            response.Data = _mapper.Map<ICollection<CategoriaDtoResponse>>(data);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al listar los elementos";
            _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }
}
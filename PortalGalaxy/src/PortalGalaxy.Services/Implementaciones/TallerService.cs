using AutoMapper;
using Microsoft.Extensions.Logging;
using PortalGalaxy.Common.Request;
using PortalGalaxy.Common.Response;
using PortalGalaxy.Entities;
using PortalGalaxy.Repositories.Interfaces;
using PortalGalaxy.Services.Interfaces;
using PortalGalaxy.Services.Utils;

namespace PortalGalaxy.Services.Implementaciones;

public class TallerService : ITallerService
{
    private readonly ITallerRepository _repository;
    private readonly ILogger<TallerService> _logger;
    private readonly IMapper _mapper;
    private readonly IFileUploader _fileUploader;

    public TallerService(ITallerRepository repository, ILogger<TallerService> logger, IMapper mapper, IFileUploader fileUploader)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
        _fileUploader = fileUploader;
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
            response.TotalCount = total;
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

    public async Task<BaseResponse> AddAsync(TallerDtoRequest request)
    {
        var response = new BaseResponse();
        try
        {
            var entity = _mapper.Map<Taller>(request);

            entity.PortadaUrl = await _fileUploader.UploadFileAsync(request.PortadaBase64, request.PortadaFileName);
            entity.TemarioUrl = await _fileUploader.UploadFileAsync(request.TemarioBase64, request.TemarioFileName);

            await _repository.AddAsync(entity);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al agregar el taller";
            _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }

    public async Task<BaseResponse<TallerDtoRequest>> FindByIdAsync(int id)
    {
        var response = new BaseResponse<TallerDtoRequest>();
        try
        {
            var entity = await _repository.FindAsync(id);
            if (entity == null)
            {
                response.ErrorMessage = "Taller no encontrado";
                return response;
            }
            response.Data = _mapper.Map<TallerDtoRequest>(entity);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al buscar el taller";
            _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }

    public async Task<BaseResponse> UpdateAsync(int id, TallerDtoRequest request)
    {
        var response = new BaseResponse();
        try
        {
            var entity = await _repository.FindAsync(id);
            if (entity == null)
            {
                response.ErrorMessage = "Taller no encontrado";
                return response;
            }
            _mapper.Map(request, entity);

            if (!string.IsNullOrEmpty(request.PortadaBase64) && !string.IsNullOrEmpty(request.PortadaFileName))
            {
                entity.PortadaUrl = await _fileUploader.UploadFileAsync(request.PortadaBase64, request.PortadaFileName);
            }

            if (!string.IsNullOrEmpty(request.TemarioBase64) && !string.IsNullOrEmpty(request.TemarioFileName))
            {
                entity.TemarioUrl = await _fileUploader.UploadFileAsync(request.TemarioBase64, request.TemarioFileName);
            }
            
            await _repository.UpdateAsync();
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al actualizar el taller";
            _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }

    public async Task<BaseResponse> DeleteAsync(int id)
    {
        var response = new BaseResponse();
        try
        {
            await _repository.DeleteAsync(id);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al eliminar el taller";
            _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }
}

using Microsoft.Extensions.Logging;
using PortalGalaxy.Common.Request;
using PortalGalaxy.Common.Response;
using PortalGalaxy.Services.Interfaces;
using QuestPDF.Fluent;

namespace PortalGalaxy.Services.Implementaciones;

public class PdfService : IPdfService
{
    private readonly ITallerService _tallerService;
    private readonly ILogger<PdfService> _logger;

    public PdfService(ITallerService tallerService, ILogger<PdfService> logger)
    {
        _tallerService = tallerService;
        _logger = logger;
    }

    public async Task<BaseResponse<Document>> Generar(BusquedaTallerRequest request)
    {
        var response = new BaseResponse<Document>();
        try
        {
            var data = await _tallerService.ListAsync(request);
            if (data is { Success: true, TotalCount: > 0, Data: not null })
            {
                QuestPDF.Settings.CheckIfAllTextGlyphsAreAvailable = false;

                var doc = Document.Create(document =>
                {
                    document.Page(page =>
                    {
                        page.MarginLeft(20);
                        page.MarginTop(20);
                        page.MarginRight(10);
                        page.Header().Row(row =>
                        {
                            row.ConstantItem(120).Height(80).AlignCenter().PaddingTop(20).Text("LISTADO DE TALLERES");
                        });
                        page.Content().PaddingVertical(15).Column(col =>
                        {
                            col.Item().PaddingTop(10).Row(row =>
                            {
                                row.RelativeItem().AlignCenter().Text("ID");
                                row.RelativeItem().AlignCenter().Text("Nombre");
                                row.RelativeItem().AlignCenter().Text("Categoría");
                                row.RelativeItem().AlignCenter().Text("Instructor");
                                row.RelativeItem().AlignCenter().Text("Fecha");
                                row.RelativeItem().AlignCenter().Text("Situación");
                            });
                            col.Item().Border(0.5f).Row(row =>
                            {
                                row.RelativeItem().Column(c =>
                                {
                                    foreach (var taller in data.Data)
                                    {
                                        c.Item().Row(r =>
                                        {
                                            r.RelativeItem().Text(taller.Id.ToString());
                                            r.RelativeItem().Text(taller.Taller);
                                            r.RelativeItem().Text(taller.Categoria);
                                            r.RelativeItem().Text(taller.Instructor);
                                            r.RelativeItem().Text(taller.Fecha);
                                            r.RelativeItem().Text(taller.Situacion);
                                        });
                                    }
                                });
                            });
                        });
                    });
                });

                response.Data = doc;
                response.Success = true;
            }
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al generar el PDF";
            _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }
}
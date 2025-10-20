using Microsoft.Extensions.Logging;
using PortalGalaxy.Common.Request;
using PortalGalaxy.Common.Response;
using PortalGalaxy.Services.Interfaces;
using PortalGalaxy.Services.Utils;
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
            if (data is { Success: true, TotalPages: > 0, Data: not null })
            {
                QuestPDF.Settings.CheckIfAllTextGlyphsAreAvailable = false;

                var doc = Document.Create(document =>
                {
                    document.Page(page =>
                    {
                        page.MarginLeft(25);
                        page.MarginTop(25);
                        page.MarginRight(20);
                        page.MarginBottom(25);

                        
                        page.Header().PaddingBottom(10).Row(row =>
                        {
                            row.RelativeItem().AlignMiddle().AlignCenter().Text("LISTADO DE TALLERES")
                                .FontSize(16).SemiBold().FontColor(QuestPDF.Helpers.Colors.Grey.Darken3);

                            row.ConstantItem(90);
                        });

                        page.Content().PaddingTop(10).Table(table =>
                        {
                            table.ColumnsDefinition(cols =>
                            {
                                cols.ConstantColumn(45);
                                cols.RelativeColumn(2.2f);
                                cols.RelativeColumn(1.6f);
                                cols.RelativeColumn(1.6f);
                                cols.RelativeColumn(1.2f);
                                cols.RelativeColumn(1.2f);
                            });

                            table.Header(header =>
                            {
                                var headerBg = QuestPDF.Helpers.Colors.Grey.Lighten3;

                                header.Cell().Background(headerBg).PaddingVertical(6).PaddingHorizontal(6)
                                    .Text("ID").FontSize(10).SemiBold().FontColor(QuestPDF.Helpers.Colors.Grey.Darken2).AlignCenter();

                                header.Cell().Background(headerBg).PaddingVertical(6).PaddingHorizontal(6)
                                    .Text("Nombre").FontSize(10).SemiBold().FontColor(QuestPDF.Helpers.Colors.Grey.Darken2);

                                header.Cell().Background(headerBg).PaddingVertical(6).PaddingHorizontal(6)
                                    .Text("Categoría").FontSize(10).SemiBold().FontColor(QuestPDF.Helpers.Colors.Grey.Darken2);

                                header.Cell().Background(headerBg).PaddingVertical(6).PaddingHorizontal(6)
                                    .Text("Instructor").FontSize(10).SemiBold().FontColor(QuestPDF.Helpers.Colors.Grey.Darken2);

                                header.Cell().Background(headerBg).PaddingVertical(6).PaddingHorizontal(6)
                                    .Text("Fecha").FontSize(10).SemiBold().FontColor(QuestPDF.Helpers.Colors.Grey.Darken2).AlignCenter();

                                header.Cell().Background(headerBg).PaddingVertical(6).PaddingHorizontal(6)
                                    .Text("Situación").FontSize(10).SemiBold().FontColor(QuestPDF.Helpers.Colors.Grey.Darken2).AlignCenter();
                            });

                            int i = 0;
                            foreach (var taller in data.Data)
                            {
                                var bg = (i++ % 2 == 0) ? QuestPDF.Helpers.Colors.White : QuestPDF.Helpers.Colors.Grey.Lighten5;

                                table.Cell().Background(bg).PaddingVertical(5).PaddingHorizontal(6)
                                    .AlignCenter().Text($"{taller.Id}").FontSize(9).FontColor(QuestPDF.Helpers.Colors.Grey.Darken3);

                                table.Cell().Background(bg).PaddingVertical(5).PaddingHorizontal(6)
                                    .Text($"{taller.Taller}".Trim()).FontSize(9).FontColor(QuestPDF.Helpers.Colors.Grey.Darken3);

                                table.Cell().Background(bg).PaddingVertical(5).PaddingHorizontal(6)
                                    .Text($"{taller.Categoria}".Trim()).FontSize(9).FontColor(QuestPDF.Helpers.Colors.Grey.Darken3);

                                table.Cell().Background(bg).PaddingVertical(5).PaddingHorizontal(6)
                                    .Text($"{taller.Instructor}".Trim()).FontSize(9).FontColor(QuestPDF.Helpers.Colors.Grey.Darken3);

                                var fechaTexto = $"{taller.Fecha}";
                                table.Cell().Background(bg).PaddingVertical(5).PaddingHorizontal(6)
                                    .AlignCenter().Text(fechaTexto).FontSize(9).FontColor(QuestPDF.Helpers.Colors.Grey.Darken3);

                                table.Cell().Background(bg).PaddingVertical(5).PaddingHorizontal(6)
                                    .AlignCenter().Text($"{taller.Situacion}".Trim()).FontSize(9).FontColor(QuestPDF.Helpers.Colors.Grey.Darken3);
                            }

                        });

                        page.Footer().AlignRight().Text(txt =>
                        {
                            txt.Span("Página ").FontSize(8).FontColor(QuestPDF.Helpers.Colors.Grey.Darken1);
                            txt.CurrentPageNumber().FontSize(8).FontColor(QuestPDF.Helpers.Colors.Grey.Darken1);
                            txt.Span(" / ").FontSize(8).FontColor(QuestPDF.Helpers.Colors.Grey.Darken1);
                            txt.TotalPages().FontSize(8).FontColor(QuestPDF.Helpers.Colors.Grey.Darken1);
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
    public async Task<BaseResponse<Document>> Generar(BusquedaInscritosPorTallerRequest request)
    {
        var response = new BaseResponse<Document>();

        try
        {
            var data = await _tallerService.ListAsync(request);
            if (data is { Success: true, TotalPages: > 0, Data: not null })
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
                            row.ConstantItem(120).Height(80).AlignCenter().PaddingTop(20).Text("LISTADO DE INSCRITOS POR TALLER");
                        });
                        page.Content().PaddingVertical(15).Column(col =>
                        {
                            col.Item().PaddingTop(10).Row(row =>
                            {
                                row.RelativeItem().AlignCenter().Text("Taller");
                                row.RelativeItem().AlignCenter().Text("Instructor");
                                row.RelativeItem().AlignCenter().Text("Fecha Inicio");
                                row.RelativeItem().AlignCenter().Text("Situacion");
                            });
                            col.Item().Border(0.5f).Row(row =>
                            {
                                row.RelativeItem().Column(c =>
                                {
                                    foreach (var inscritos in data.Data)
                                    {
                                        c.Item().Row(r =>
                                        {
                                            r.RelativeItem().Text(inscritos.Taller).TextData();
                                            r.RelativeItem().Text(inscritos.Instructor).TextData();
                                            r.RelativeItem().Text(inscritos.Fecha).TextData();
                                            r.RelativeItem().Text(inscritos.Situacion).TextData();
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
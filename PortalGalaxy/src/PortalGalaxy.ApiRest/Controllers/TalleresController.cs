using Microsoft.AspNetCore.Mvc;
using PortalGalaxy.Common.Request;
using PortalGalaxy.Services.Interfaces;
using QuestPDF.Fluent;

namespace PortalGalaxy.ApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TalleresController : ControllerBase
    {
        private readonly ITallerService _service;
        private readonly IPdfService _pdfService;

        public TalleresController(ITallerService service, IPdfService pdfService)
        {
            _service = service;
            _pdfService = pdfService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] BusquedaTallerRequest request)
        {
            var response = await _service.ListAsync(request);

            return Ok(response);
        }

        [HttpPost("pdf")]
        public async Task<IActionResult> Pdf(BusquedaTallerRequest request)
        {
            var response = await _pdfService.Generar(request);
            if (response is { Data: not null, Success: true })
            {
                var bytes = response.Data.GeneratePdf();

                // TODO: subir el pdf al servidor

                return File(new MemoryStream(bytes), "application/pdf");
            }

            return Ok(response);
        }
    }
}

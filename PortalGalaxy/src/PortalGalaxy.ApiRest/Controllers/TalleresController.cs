using Microsoft.AspNetCore.Mvc;
using PortalGalaxy.Services.Interfaces;

namespace PortalGalaxy.ApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TalleresController : ControllerBase
    {
        private readonly ITallerService _service;

        public TalleresController(ITallerService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get(string? nombre, int? categoria, int? situacion, int pageNumber = 1, int pageSize = 5)
        {
            var response = await _service.ListAsync(nombre, categoria, situacion, pageNumber, pageSize);

            return Ok(response);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using PortalGalaxy.Common.Request;
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
        public async Task<IActionResult> Get([FromQuery] BusquedaTallerRequest request)
        {
            var response = await _service.ListAsync(request);

            return Ok(response);
        }
    }
}

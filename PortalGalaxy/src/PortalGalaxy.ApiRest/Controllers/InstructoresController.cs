using Microsoft.AspNetCore.Mvc;
using PortalGalaxy.Services.Interfaces;

namespace PortalGalaxy.ApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructoresController : ControllerBase
    {
        private readonly IInstructorService _service;

        public InstructoresController(IInstructorService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get(string? nombre, string? nroDocumento, int? categoriaId)
        {
            var response = await _service.ListAsync(nombre, nroDocumento, categoriaId);

            return Ok(response);
        }
    }
}

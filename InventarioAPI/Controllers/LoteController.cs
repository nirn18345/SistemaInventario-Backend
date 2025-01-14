using Inventario.Application.Interfaces;
using Inventario.Infraestructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventarioAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoteController : ControllerBase
    {

        public readonly ILoteService _loteService;

        public LoteController(ILoteService loteService)
        {
            _loteService = loteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetLotes()
        {
            var product = await _loteService.ObtenerLotesAsync();
            if (product == null)
                return NotFound($"No se encontraron lotes disponibles.");

            return Ok(product);
        }

    }
}

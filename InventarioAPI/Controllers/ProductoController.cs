using Inventario.Application.DTOs;
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
    public class ProductoController : ControllerBase
    {
        public readonly IProductoService _productoService;
        private readonly ILogger<ProductoController> _logger;

        public ProductoController(IProductoService productoService, ILogger<ProductoController> logger)
        {
            _productoService = productoService;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> GetProducts(
       [FromQuery] int page = 1,
       [FromQuery] int size = 10,
       [FromQuery] string sortBy = "name",
       [FromQuery] string search = "")
        {
            _logger.LogInformation("Metodo para obtener productos");

            if (page < 1 || size < 1)
                return BadRequest("Page and size must be greater than 0.");

            var products = await _productoService.ObtenerProductosPaginadosAsync(page, size, sortBy, search);
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductoDto productDto)
        {
            _logger.LogInformation("Metodo para crear productos");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productId = await _productoService.AgregarProductoAsync(productDto);
          
            productDto.Id = productId;

            return CreatedAtAction(nameof(GetProductById), new { id = productId }, productDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            _logger.LogInformation("Metodo para obtener productos por ID");
            var product = await _productoService.ObtenertProductosAsync(id);
            if (product == null)
                return NotFound($"No se encontró un producto con ID {id}.");

            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductoDto productDto)
        {
            _logger.LogInformation("Metodo para actualizar productos");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _productoService.ModificarProductoAsync(id, productDto);
            if (!updated)
                return NotFound($"No se encontró un producto con ID {id} para actualizar.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            _logger.LogInformation("Metodo para eliminar productos");
            var deleted = await _productoService.EliminarProductoAsync(id);
            if (!deleted)
                return NotFound($"No se encontró un producto con ID {id} para eliminar.");

            return NoContent();
        }

    }
}

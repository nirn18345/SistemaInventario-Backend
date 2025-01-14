using Inventario.Application.DTOs;
using Inventario.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventarioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CredencialesController : ControllerBase
    {
        public readonly ICredencialesService _credencialesService;


        public CredencialesController(ICredencialesService credencialesService)
        {
            _credencialesService = credencialesService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] CredencialesDto loginDto)
        {
            var token = await _credencialesService.ValidarCredencialesAsync(loginDto.Usuario, loginDto.Clave);

            if (token == null)
                return Unauthorized("Usuario o contraseña incorrectos.");

            return Ok(new { Token = token });
        }

    }
}

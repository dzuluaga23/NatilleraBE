using Microsoft.AspNetCore.Mvc;
using NatilleraBE.DTOs;
using NatilleraBE.Services;

namespace NatilleraBE.Controllers
{
    [ApiController]
    [Route("api/Prestamos")]
    public class PrestamoController : ControllerBase
    {
        private readonly clsPrestamo _prestamoService;

        public PrestamoController(clsPrestamo prestamoService)
        {
            _prestamoService = prestamoService;
        }

        [HttpPost("Registrar")]
        public async Task<IActionResult> CrearPrestamo([FromBody] PrestamoCreateDto dto)
        {
            var prestamo = await _prestamoService.CrearPrestamoAsync(dto);
            return Ok(prestamo);
        }

        [HttpGet("ObtenerTodos")]
        public async Task<IActionResult> ObtenerTodos()
        {
            var prestamos = await _prestamoService.ObtenerTodosAsync();
            return Ok(prestamos);
        }

        [HttpGet("ObtenerXId")]
        public async Task<IActionResult> ObtenerPorSocio(int idSocio)
        {
            var prestamos = await _prestamoService.ObtenerPorSocioAsync(idSocio);
            return Ok(prestamos);
        }
    }
}

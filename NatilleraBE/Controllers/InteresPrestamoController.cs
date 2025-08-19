using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NatilleraBE.DTOs;
using NatilleraBE.Models;
using NatilleraBE.Services;

namespace NatilleraBE.Controllers
{
    [ApiController]
    [Route("api/InteresPrestamo")]
    public class InteresPrestamoController : ControllerBase
    {
        private readonly clsInteresPrestamo _service;

        public InteresPrestamoController(clsInteresPrestamo service)
        {
            _service = service;
        }

        [HttpPost("Crear")]
        public async Task<IActionResult> Crear([FromBody] InteresPrestamoDto dto)
        {
            try
            {
                var interes = await _service.CrearInteresAsync(dto);
                return Ok(interes);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpGet("ObtenerPorPrestamo")]
        public async Task<IActionResult> ObtenerPorPrestamo(int idPrestamo)
        {
            try
            {
                var intereses = await _service.ObtenerPorPrestamoAsync(idPrestamo);
                return Ok(intereses);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}

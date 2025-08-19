using Microsoft.AspNetCore.Mvc;
using NatilleraBE.DTOs;
using NatilleraBE.Services;

namespace NatilleraBE.Controllers
{
    [ApiController]
    [Route("api/Abonos")]
    public class AbonosController : ControllerBase
    {
        private readonly clsAbonos _abonoService;

        public AbonosController(clsAbonos abonoService)
        {
            _abonoService = abonoService;
        }

        [HttpPost("Registrar")]
        public async Task<IActionResult> Registrar([FromBody] AbonoCreateDto dto)
        {
            try
            {
                var abono = await _abonoService.CrearAbonoAsync(dto);
                return Ok(abono);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpGet("ObtenerXId/{idPrestamo}")]
        public async Task<IActionResult> ObtenerPorPrestamo(int idPrestamo)
        {
            try
            {
                var abonos = await _abonoService.ObtenerPorPrestamoAsync(idPrestamo);
                return Ok(abonos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}

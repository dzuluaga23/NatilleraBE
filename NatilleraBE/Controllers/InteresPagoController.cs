using Microsoft.AspNetCore.Mvc;
using NatilleraBE.DTOs;
using NatilleraBE.Services;

namespace NatilleraBE.Controllers
{
    [ApiController]
    [Route("api/InteresPago")]
    public class InteresPagoController : ControllerBase
    {
        private readonly clsInteresPago _clsInteresPago;

        public InteresPagoController(clsInteresPago clsInteresPago)
        {
            _clsInteresPago = clsInteresPago;
        }

        [HttpPost("Registrar")]
        public async Task<IActionResult> Registrar([FromBody] InteresPagoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var interesPago = await _clsInteresPago.RegistrarInteresPagoAsync(dto);
            return Ok(interesPago);
        }
        [HttpGet("ObtenerXId")]
        public async Task<IActionResult> ObtenerPorIdPago(int idPago)
        {
            var intereses = await _clsInteresPago.ObtenerPorIdPagoAsync(idPago);

            if (intereses == null || intereses.Count == 0)
                return NotFound(new { mensaje = "No se encontraron intereses para este pago" });

            return Ok(intereses);
        }

    }
}

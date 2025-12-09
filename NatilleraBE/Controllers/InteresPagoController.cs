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

    }
}

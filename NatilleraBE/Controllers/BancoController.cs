using Microsoft.AspNetCore.Mvc;
using NatilleraBE.DTOs;
using NatilleraBE.Services;

namespace NatilleraBE.Controllers
{
    [ApiController]
    [Route("api/Banco")]
    public class BancoController : ControllerBase
    {
        private readonly clsBanco _clsBanco;

        public BancoController(clsBanco clsBanco)
        {
            _clsBanco = clsBanco;
        }
        [HttpGet("Resumen")]
        public async Task<ActionResult<BancoResumenDto>> GetResumenGeneral()
        {
            var resumen = await _clsBanco.ObtenerResumenGeneralAsync();
            return Ok(resumen);
        }
        [HttpGet("ResumenXMes")]
        public async Task<ActionResult<BancoResumenDto>> GetResumenPorMes(int anio, int mes)
        {
            if (mes < 1 || mes > 12)
                return BadRequest("El mes debe estar entre 1 y 12");

            var resumen = await _clsBanco.ObtenerResumenPorMesAsync(mes, anio);
            return Ok(resumen);
        }
        [HttpGet("ObtenerBanco")]
        public async Task<ActionResult<BancoDto>> GetBanco()
        {
            var banco = await _clsBanco.ObtenerBancoAsync();
            if (banco == null) return NotFound("No existe un registro de banco.");
            return Ok(banco);
        }

        [HttpPut("ActualizarBanco")]
        public async Task<ActionResult<BancoDto>> ActualizarBanco([FromBody] BancoUpdateDto dto)
        {
            var banco = await _clsBanco.ActualizarBancoAsync(dto);
            if (banco == null) return NotFound("No existe un registro de banco.");
            return Ok(banco);
        }

    }
}

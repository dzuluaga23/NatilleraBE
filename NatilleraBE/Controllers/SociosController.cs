using Microsoft.AspNetCore.Mvc;
using NatilleraBE.Data;
using NatilleraBE.DTOs;
using NatilleraBE.Services;

namespace NatilleraBE.Controllers
{
    [ApiController]
    [Route("api/Socios")]
    public class SocioController : ControllerBase
    {
        private readonly clsSocio _clsSocio;

        public SocioController(clsSocio clsSocio)
        {
            _clsSocio = clsSocio;
        }

        [HttpPost("Insertar")]
        public IActionResult CrearSocio([FromBody] CrearSocioDto dto)
        {
            var resultado = _clsSocio.InsertarSocio(dto);

            if (resultado.StartsWith("Error") || resultado.StartsWith("Ya") || resultado.StartsWith("El"))
                return BadRequest(new { mensaje = resultado });

            return Ok(new { mensaje = resultado });
        }
        [HttpGet("ConsultarTodos")]
        public ActionResult<List<BuscarSocioDto>> ObtenerTodos()
        {
            var socios = _clsSocio.ObtenerTodos();
            return Ok(socios);
        }

        [HttpGet("ConsultarXDocumento")]
        public ActionResult<BuscarSocioDto> ObtenerXDocumento(int documento)
        {
            var socio = _clsSocio.ObtenerXDocumento(documento);
            if (socio == null) return NotFound("Socio no encontrado.");
            return Ok(socio);
        }
        [HttpDelete("Eliminar")]
        public IActionResult EliminarPorDocumento(int documento)
        {
            var mensaje = _clsSocio.EliminarPorDocumento(documento);
            return Ok(mensaje);
        }
        [HttpPut("Activar")]
        public IActionResult ActivarPorDocumento(int documento)
        {
            var mensaje = _clsSocio.ActivarPorDocumento(documento);
            return Ok(mensaje);
        }
        [HttpGet("NumerosDisponibles")]
        public ActionResult<List<string>> ObtenerNumerosDisponibles()
        {
            var resultado = _clsSocio.ObtenerNumerosDisponibles();
            return Ok(resultado);
        }
        [HttpGet("NumerosConSocios")]
        public async Task<IActionResult> GetNumerosConSocios()
        {
            var socios = await _clsSocio.ObtenerSociosConNumerosAsync();
            return Ok(socios);
        }
        [HttpGet("Inactivos")]
        public ActionResult<List<BuscarSocioDto>> ObtenerInactivos()
        {
            var resultado = _clsSocio.ObtenerInactivos();
            return Ok(resultado);
        }


    }
}

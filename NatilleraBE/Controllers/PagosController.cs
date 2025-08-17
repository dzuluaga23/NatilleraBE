using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NatilleraBE.DTOs;
using NatilleraBE.Services;
using static NatilleraBE.Services.clsPago;

namespace NatilleraBE.Controllers
{
    [ApiController]
    [Route("api/Pagos")]
    public class PagosController : ControllerBase
    {
        private readonly clsPago _clsPago;

        public PagosController(clsPago pagoService)
        {
            _clsPago = pagoService;
        }

        [HttpPost("Registrar")]
        public IActionResult RegistrarPago([FromBody] RegistrarPagoDto dto)
        {
            var resultado = _clsPago.RegistrarPago(dto);
            return Ok(resultado);
        }

        [HttpPut("Actualizar")]
        public IActionResult ActualizarPago([FromBody] ActualizarPagoDto dto)
        {
            var resultado = new clsPago().ActualizarPago(dto);
            if (resultado.StartsWith("Error"))
                return BadRequest(resultado);

            return Ok(resultado);
        }

        [HttpGet("PorDocumento")]
        public ActionResult<PagosSocioDto> ObtenerPorDocumento(int documento)
        {
            var resultado = _clsPago.ObtenerPagosPorDocumento(documento);
            if (resultado == null)
                return NotFound("No se encontraron pagos para el documento especificado.");

            return Ok(resultado);
        }
        [HttpGet("PorMes")]
        public ActionResult<ResumenPagosMesDto> GetPagosDeTodosPorMes(int mes, int anio)
        {
            var service = new clsPago();
            var resumen = service.ObtenerPagosPorMes(mes, anio);
            return Ok(resumen);
        }

        [HttpGet("DetalladosXMes")]
        public IActionResult ObtenerPagosDetalladosPorMes(int mes, int anio)
        {
            var pagos = _clsPago.ObtenerPagosDetalladosPorMes(mes, anio); 
            return Ok(pagos);
        }


    }
}

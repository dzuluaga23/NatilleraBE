using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NatilleraBE.Models;
using NatilleraBE.Services;

namespace NatilleraBE.Controllers
{
    [ApiController]
    [Route("api/Pollas")]
    public class PollasController : ControllerBase
    {
        private readonly clsPolla _clsPolla;

        public PollasController(clsPolla clsPolla)
        {
            _clsPolla = clsPolla;
        }

        // POST api/polla/registrar
        [HttpPost("Registrar")]
        public IActionResult Registrar([FromBody] Polla polla)
        {
            var resultado = _clsPolla.RegistrarPolla(polla);

            if (resultado.StartsWith("Error"))
                return BadRequest(resultado);

            return Ok(resultado);
        }

        // GET api/polla/consultar
        [HttpGet("ConsultarTodos")]
        public IActionResult Consultar()
        {
            var lista = _clsPolla.ConsultarPollas();
            return Ok(lista);
        }
    }
}

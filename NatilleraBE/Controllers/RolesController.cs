using Microsoft.AspNetCore.Mvc;
using NatilleraBE.Data;

namespace NatilleraBE.Controllers
{
    [ApiController]
    [Route("api/Roles")]
    public class RolesController : ControllerBase
    {
        private readonly NatilleraDbContext _context;

        public RolesController()
        {
            _context = new NatilleraDbContext();
        }

        [HttpGet("ConsultarTodos")]
        public IActionResult ObtenerTodos()
        {
            var roles = _context.Rols
                .Select(r => new
                {
                    r.Id,
                    r.Nombre
                }).ToList();

            return Ok(roles);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using NatilleraBE.Data;
using NatilleraBE.DTOs;
using NatilleraBE.Models;

namespace NatilleraBE.Services
{
    public class clsPrestamo
    {
        private readonly NatilleraDbContext _context;

        public clsPrestamo(NatilleraDbContext context)
        {
            _context = context;
        }

        public async Task<Prestamo> CrearPrestamoAsync(PrestamoCreateDto dto)
        {
            var prestamo = new Prestamo
            {
                Valor = dto.Valor,
                Fecha = dto.Fecha,             
                FechaCorte = dto.Fecha.Day,    
                IdSocio = dto.IdSocio
            };

            _context.Prestamos.Add(prestamo);
            await _context.SaveChangesAsync();

            return prestamo;
        }

        public async Task<List<PrestamoConsultaDto>> ObtenerTodosAsync()
        {
            var prestamos = await _context.Prestamos
                .Include(p => p.IdSocioNavigation)
                .Include(p => p.AbonosPrestamos)
                .Include(p => p.InteresPrestamos)
                .ToListAsync();

            return prestamos.Select(p => new PrestamoConsultaDto
            {
                Id = p.Id,
                Valor = p.Valor ?? 0,
                Fecha = p.Fecha,
                FechaCorte = p.FechaCorte,
                IdSocio = p.IdSocio,
                NombreSocio = p.IdSocioNavigation.Nombre,
                TotalIntereses = p.InteresPrestamos.Sum(i => i.Valor),
                TotalAbonos = p.AbonosPrestamos.Sum(a => a.Valor ?? 0),
                SaldoPendiente = (p.Valor ?? 0) - p.AbonosPrestamos.Sum(a => a.Valor ?? 0)
            }).ToList();
        }

        public async Task<List<PrestamoConsultaDto>> ObtenerPorSocioAsync(int idSocio)
        {
            var prestamos = await _context.Prestamos
                .Where(p => p.IdSocio == idSocio)
                .Include(p => p.IdSocioNavigation)
                .Include(p => p.AbonosPrestamos)
                .Include(p => p.InteresPrestamos)
                .ToListAsync();

            return prestamos.Select(p => new PrestamoConsultaDto
            {
                Id = p.Id,
                Valor = p.Valor ?? 0,
                Fecha = p.Fecha,
                FechaCorte = p.FechaCorte,
                IdSocio = p.IdSocio,
                NombreSocio = p.IdSocioNavigation.Nombre,
                TotalIntereses = p.InteresPrestamos.Sum(i => i.Valor),
                TotalAbonos = p.AbonosPrestamos.Sum(a => a.Valor ?? 0),
                SaldoPendiente = (p.Valor ?? 0) - p.AbonosPrestamos.Sum(a => a.Valor ?? 0)
            }).ToList();
        }
    }
}

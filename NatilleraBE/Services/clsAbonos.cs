using Microsoft.EntityFrameworkCore;
using NatilleraBE.Data;
using NatilleraBE.DTOs;
using NatilleraBE.Models;

namespace NatilleraBE.Services
{
    public class clsAbonos
    {
        private readonly NatilleraDbContext _context;

        public clsAbonos(NatilleraDbContext context)
        {
            _context = context;
        }

        public async Task<AbonosPrestamo> CrearAbonoAsync(AbonoCreateDto dto)
        {
            var prestamo = await _context.Prestamos
                .Include(p => p.AbonosPrestamos)
                .Include(p => p.InteresPrestamos)
                .FirstOrDefaultAsync(p => p.Id == dto.IdPrestamo);

            if (prestamo == null)
                throw new Exception("El préstamo no existe");

            var totalIntereses = prestamo.InteresPrestamos.Sum(i => i.Valor);
            var totalAbonos = prestamo.AbonosPrestamos.Sum(a => a.Valor ?? 0);
            var saldoPendiente = (prestamo.Valor ?? 0) + totalIntereses - totalAbonos;

            if (dto.Valor > saldoPendiente)
                throw new Exception("El abono no puede ser mayor al saldo pendiente");

            var abono = new AbonosPrestamo
            {
                Valor = dto.Valor,
                Fecha = DateOnly.FromDateTime(DateTime.Now),
                IdPrestamo = dto.IdPrestamo,
                ValorRestante = saldoPendiente - dto.Valor
            };

            _context.AbonosPrestamos.Add(abono);
            await _context.SaveChangesAsync();

            return abono;
        }

        public async Task<List<AbonoConsultaDto>> ObtenerPorPrestamoAsync(int idPrestamo)
        {
            var abonos = await _context.AbonosPrestamos
                .Where(a => a.IdPrestamo == idPrestamo)
                .OrderBy(a => a.Fecha)
                .ToListAsync();

            return abonos.Select(a => new AbonoConsultaDto
            {
                Id = a.Id,
                Valor = a.Valor ?? 0,
                Fecha = a.Fecha,
                ValorRestante = a.ValorRestante,
                IdPrestamo = a.IdPrestamo
            }).ToList();
        }
    }
}

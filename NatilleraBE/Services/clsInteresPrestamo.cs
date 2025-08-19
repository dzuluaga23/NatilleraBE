using Microsoft.EntityFrameworkCore;
using NatilleraBE.Data;
using NatilleraBE.DTOs;
using NatilleraBE.Models;

namespace NatilleraBE.Services
{
    public class clsInteresPrestamo
    {
        private readonly NatilleraDbContext _context;

        public clsInteresPrestamo(NatilleraDbContext context)
        {
            _context = context;
        }

        public async Task<InteresPrestamo> CrearInteresAsync(InteresPrestamoDto dto)
        {
            var prestamo = await _context.Prestamos
                .Include(p => p.InteresPrestamos)
                .FirstOrDefaultAsync(p => p.Id == dto.IdPrestamo);

            if (prestamo == null)
                throw new Exception("El préstamo no existe");

            var interes = new InteresPrestamo
            {
                Valor = dto.Valor,
                Fecha = DateOnly.FromDateTime(DateTime.Now),
                IdPrestamo = dto.IdPrestamo
            };

            _context.InteresPrestamos.Add(interes);
            await _context.SaveChangesAsync();

            return interes;
        }

        public async Task<List<InteresConsultaDto>> ObtenerPorPrestamoAsync(int idPrestamo)
        {
            var intereses = await _context.InteresPrestamos
                .Where(i => i.IdPrestamo == idPrestamo)
                .OrderBy(i => i.Fecha)
                .ToListAsync();

            return intereses.Select(i => new InteresConsultaDto
            {
                Id = i.Id,
                Valor = i.Valor,
                Fecha = i.Fecha,
                IdPrestamo = i.IdPrestamo
            }).ToList();
        }
    }
}

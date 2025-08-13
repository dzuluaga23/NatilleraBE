using Microsoft.EntityFrameworkCore;
using NatilleraBE.Data;
using NatilleraBE.DTOs;
using NatilleraBE.Models;

namespace NatilleraBE.Services
{
    public class clsInteresPago
    {
        private readonly NatilleraDbContext dbNatillera;

        public clsInteresPago(NatilleraDbContext context)
        {
            dbNatillera = context;
        }

        public async Task<InteresPagoResponseDto> RegistrarInteresPagoAsync(InteresPagoDto dto)
        {
            var pago = await dbNatillera.Pagos.FindAsync(dto.IdPago);
            if (pago == null)
            {
                throw new KeyNotFoundException("El pago especificado no existe.");
            }

            var interesPago = new InteresPago
            {
                Dias = dto.Dias,
                ValorTotal = dto.ValorTotal,
                IdPago = dto.IdPago
            };

            dbNatillera.InteresPagos.Add(interesPago);
            await dbNatillera.SaveChangesAsync();

            return new InteresPagoResponseDto
            {
                Id = interesPago.Id,
                Dias = interesPago.Dias,
                ValorTotal = interesPago.ValorTotal,
                IdPago = interesPago.IdPago
            };
        }
        public async Task<List<InteresPagoResponseDto>> ObtenerPorIdPagoAsync(int idPago)
        {
            var intereses = await dbNatillera.InteresPagos
                .AsNoTracking()
                .Where(i => i.IdPago == idPago)
                .ToListAsync();

            return intereses.Select(i => new InteresPagoResponseDto
            {
                Id = i.Id,
                Dias = i.Dias,
                ValorTotal = i.ValorTotal,
                IdPago = i.IdPago
            }).ToList();
        }
    }
}

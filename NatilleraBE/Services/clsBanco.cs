using Microsoft.EntityFrameworkCore;
using NatilleraBE.Data;
using NatilleraBE.DTOs;
using NatilleraBE.Models;

namespace NatilleraBE.Services
{
    public class clsBanco
    {
        private readonly NatilleraDbContext _context;

        public clsBanco(NatilleraDbContext context)
        {
            _context = context;
        }
        public async Task<BancoResumenDto> ObtenerResumenGeneralAsync()
        {
            var totalAbonos = await _context.AbonosPrestamos.SumAsync(a => (decimal?)a.Valor) ?? 0;
            var totalInteresPagos = await _context.InteresPagos.SumAsync(i => (decimal?)i.ValorTotal) ?? 0;
            var totalInteresPrestamos = await _context.InteresPrestamos.SumAsync(i => (decimal?)i.Valor) ?? 0;
            var totalRifas = await _context.Pagos.SumAsync(p => (decimal?)p.Rifa) ?? 0;
            var totalPollas = await _context.Pagos.SumAsync(p => (decimal?)p.Polla) ?? 0;
            var totalAhorros = await _context.Pagos.SumAsync(p => (decimal?)p.Ahorro) ?? 0;
            var totalPrestamos = await _context.Prestamos.SumAsync(p => (decimal?)p.Valor) ?? 0;
            var totalSaldoRestante = totalPrestamos - totalAbonos;

            var granTotal = totalAbonos + totalSaldoRestante + totalInteresPagos +
                            totalInteresPrestamos + totalRifas + totalPollas +
                            totalAhorros;

            var totalGanancias = granTotal - totalAhorros;
            var totalHoy = granTotal - totalSaldoRestante;

            return new BancoResumenDto
            {
                TotalAbonosPrestamos = totalAbonos,
                TotalSaldoRestantePrestamos = totalSaldoRestante,
                TotalInteresPagos = totalInteresPagos,
                TotalInteresPrestamos = totalInteresPrestamos,
                TotalRifas = totalRifas,
                TotalPollas = totalPollas,
                TotalAhorros = totalAhorros,
                TotalPrestamos = totalPrestamos,
                GranTotal = granTotal,
                TotalGanancias = totalGanancias,
                TotalHoy = totalHoy
            };
        }

        public async Task<BancoResumenDto> ObtenerResumenPorMesAsync(int mes, int anio)
        {
            var totalAbonos = await _context.AbonosPrestamos
                .Where(a => a.Fecha.Month == mes && a.Fecha.Year == anio)
                .SumAsync(a => (decimal?)a.Valor) ?? 0;

            var totalInteresPagos = await _context.InteresPagos
                .Where(i => i.IdPagoNavigation.FechaPago.Month == mes && i.IdPagoNavigation.FechaPago.Year == anio)
                .SumAsync(i => (decimal?)i.ValorTotal) ?? 0;

            var totalInteresPrestamos = await _context.InteresPrestamos
                .Where(i => i.Fecha.Month == mes && i.Fecha.Year == anio)
                .SumAsync(i => (decimal?)i.Valor) ?? 0;

            var totalRifas = await _context.Pagos
                .Where(p => p.FechaPago.Month == mes && p.FechaPago.Year == anio)
                .SumAsync(p => (decimal?)p.Rifa) ?? 0;

            var totalPollas = await _context.Pagos
                .Where(p => p.FechaPago.Month == mes && p.FechaPago.Year == anio)
                .SumAsync(p => (decimal?)p.Polla) ?? 0;

            var totalAhorros = await _context.Pagos
                .Where(p => p.FechaPago.Month == mes && p.FechaPago.Year == anio)
                .SumAsync(p => (decimal?)p.Ahorro) ?? 0;

            var totalPrestamos = await _context.Prestamos
                .Where(p => p.Fecha.Month == mes && p.Fecha.Year == anio)
                .SumAsync(p => (decimal?)p.Valor) ?? 0;

            var totalSaldoRestante = totalPrestamos - totalAbonos;

            var granTotal = totalAbonos + totalSaldoRestante + totalInteresPagos +
                            totalInteresPrestamos + totalRifas + totalPollas +
                            totalAhorros;

            var totalHoy = granTotal - totalSaldoRestante;

            var totalGanancias = granTotal - totalAhorros;

            return new BancoResumenDto
            {
                TotalAbonosPrestamos = totalAbonos,
                TotalSaldoRestantePrestamos = totalSaldoRestante,
                TotalInteresPagos = totalInteresPagos,
                TotalInteresPrestamos = totalInteresPrestamos,
                TotalRifas = totalRifas,
                TotalPollas = totalPollas,
                TotalAhorros = totalAhorros,
                TotalPrestamos = totalPrestamos,
                GranTotal = granTotal,
                TotalGanancias = totalGanancias,
                TotalHoy = totalHoy
            };
        }

        public async Task<BancoDto?> ObtenerBancoAsync()
        {
            var banco = await _context.Bancos.FirstOrDefaultAsync();
            if (banco == null) return null;

            var total = (banco.Efectivo ?? 0) + (banco.Cuenta ?? 0) + (banco.Ahorro ?? 0);

            return new BancoDto
            {
                Id = banco.Id,
                Efectivo = banco.Efectivo,
                Cuenta = banco.Cuenta,
                Ahorro = banco.Ahorro,
                Total = total
            };
        }

        public async Task<BancoDto?> ActualizarBancoAsync(BancoUpdateDto dto)
        {
            var banco = await _context.Bancos.FirstOrDefaultAsync();
            if (banco == null) return null; 

            banco.Efectivo = dto.Efectivo ?? banco.Efectivo;
            banco.Cuenta = dto.Cuenta ?? banco.Cuenta;
            banco.Ahorro = dto.Ahorro ?? banco.Ahorro;

            await _context.SaveChangesAsync();

            var total = (banco.Efectivo ?? 0) + (banco.Cuenta ?? 0) + (banco.Ahorro ?? 0);

            return new BancoDto
            {
                Id = banco.Id,
                Efectivo = banco.Efectivo,
                Cuenta = banco.Cuenta,
                Ahorro = banco.Ahorro,
                Total = total
            };
        }

    }
}

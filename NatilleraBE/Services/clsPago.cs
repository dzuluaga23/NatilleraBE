using NatilleraBE.Data;
using NatilleraBE.DTOs;
using NatilleraBE.Models;

namespace NatilleraBE.Services
{
    public class clsPago
    {
        private NatilleraDbContext dbNatillera = new NatilleraDbContext();

        public string RegistrarPago(RegistrarPagoDto dto)
        {
            try
            {
                var socio = dbNatillera.Socios.FirstOrDefault(s => s.Id == dto.IdSocio && s.Estado);
                if (socio == null)
                    return "El socio no existe o está inactivo.";

                var yaPagoEsteMes = dbNatillera.Pagos.Any(p =>
                    p.IdSocio == dto.IdSocio &&
                    p.FechaPago.Month == dto.FechaPago.Month &&
                    p.FechaPago.Year == dto.FechaPago.Year &&
                    p.Estado == true);

                if (yaPagoEsteMes)
                    return "Este socio ya tiene un pago registrado para ese mes.";

                var pago = new Pago
                {
                    IdSocio = dto.IdSocio,
                    Rifa = dto.Rifa,
                    Polla = dto.Polla,
                    Ahorro = dto.Ahorro,
                    FechaPago = dto.FechaPago,
                    Estado = true 
                };

                dbNatillera.Pagos.Add(pago);
                dbNatillera.SaveChanges();
                return "Pago registrado correctamente.";
            }
            catch (Exception ex)
            {
                return $"Error al registrar el pago: {ex.Message}";
            }
        }
        public PagosSocioDto? ObtenerPagosPorDocumento(int documento)
        {
            var socio = dbNatillera.Socios
                .Where(s => s.Documento == documento && s.Estado)
                .Select(s => new
                {
                    s.Documento,
                    s.Nombre,
                    Pagos = s.Pagos.Where(p => p.Estado).ToList()
                })
                .FirstOrDefault();

            if (socio == null) return null;

            return new PagosSocioDto
            {
                Documento = socio.Documento ?? 0,
                Nombre = socio.Nombre,
                Pagos = socio.Pagos.Select(p => new PagoDetalleDto
                {
                    FechaPago = p.FechaPago,
                    Ahorro = p.Ahorro,
                    Polla = p.Polla,
                    Rifa = p.Rifa
                }).ToList(),
                TotalAhorro = socio.Pagos.Sum(p => p.Ahorro),
                TotalPolla = socio.Pagos.Sum(p => p.Polla),
                TotalRifa = socio.Pagos.Sum(p => p.Rifa)
            };
        }
        public ResumenPagosMesDto ObtenerPagosPorMes(int mes, int anio)
        {
            var socios = dbNatillera.Socios
                .Where(s => s.Estado == true)
                .Select(s => new ResumenPagoSocioDto
                {
                    IdPago = dbNatillera.Pagos
                        .Where(p => p.IdSocio == s.Id && p.FechaPago.Month == mes && p.FechaPago.Year == anio && p.Estado)
                        .Select(p => p.Id)
                        .FirstOrDefault(),

                    Documento = s.Documento ?? 0,
                    Nombre = s.Nombre,

                    Ahorro = dbNatillera.Pagos
                        .Where(p => p.IdSocio == s.Id && p.FechaPago.Month == mes && p.FechaPago.Year == anio && p.Estado)
                        .Select(p => (decimal?)p.Ahorro).FirstOrDefault() ?? 0,

                    Polla = dbNatillera.Pagos
                        .Where(p => p.IdSocio == s.Id && p.FechaPago.Month == mes && p.FechaPago.Year == anio && p.Estado)
                        .Select(p => (decimal?)p.Polla).FirstOrDefault() ?? 0,

                    Rifa = dbNatillera.Pagos
                        .Where(p => p.IdSocio == s.Id && p.FechaPago.Month == mes && p.FechaPago.Year == anio && p.Estado)
                        .Select(p => (decimal?)p.Rifa).FirstOrDefault() ?? 0,

                    FechaPago = dbNatillera.Pagos
                        .Where(p => p.IdSocio == s.Id && p.FechaPago.Month == mes && p.FechaPago.Year == anio && p.Estado)
                        .Select(p => (DateOnly?)p.FechaPago)
                        .FirstOrDefault()
                }).ToList();

            var totalAhorro = socios.Sum(s => s.Ahorro);
            var totalPolla = socios.Sum(s => s.Polla);
            var totalRifa = socios.Sum(s => s.Rifa);

            var totalInteres = (
                from i in dbNatillera.InteresPagos
                join p in dbNatillera.Pagos on i.IdPago equals p.Id
                where p.FechaPago.Month == mes && p.FechaPago.Year == anio && p.Estado
                select (decimal?)i.ValorTotal
            ).Sum() ?? 0;

            string MesEnTexto(int m) => m switch
            {
                1 => "Enero",
                2 => "Febrero",
                3 => "Marzo",
                4 => "Abril",
                5 => "Mayo",
                6 => "Junio",
                7 => "Julio",
                8 => "Agosto",
                9 => "Septiembre",
                10 => "Octubre",
                11 => "Noviembre",
                12 => "Diciembre",
                _ => m.ToString()
            };

            var mesTexto = MesEnTexto(mes);
            var mesNum = mes.ToString();
            var mesNum2 = mes.ToString("00");

            var pollaMes = dbNatillera.Pollas
                .Where(p => p.Mes != null &&
                            (p.Mes == mesTexto || p.Mes == mesNum || p.Mes == mesNum2))
                .OrderByDescending(p => p.Id)
                .FirstOrDefault();

            if (pollaMes?.Estado == true)
            {
                totalPolla /= 2;
            }

            return new ResumenPagosMesDto
            {
                Socios = socios,
                TotalAhorro = totalAhorro,
                TotalPolla = totalPolla,
                TotalRifa = totalRifa,
                TotalInteres = totalInteres 
            };
        }

        public string ActualizarPago(ActualizarPagoDto dto)
        {
            try
            {
                var pago = dbNatillera.Pagos.FirstOrDefault(p => p.Id == dto.IdPago && p.Estado);
                if (pago == null)
                    return "No se encontró el pago activo con ese ID.";

                var existeDuplicado = dbNatillera.Pagos.Any(p =>
                    p.IdSocio == pago.IdSocio &&
                    p.Id != dto.IdPago && 
                    p.FechaPago.Month == dto.FechaPago.Month &&
                    p.FechaPago.Year == dto.FechaPago.Year &&
                    p.Estado);

                if (existeDuplicado)
                    return "Ya existe otro pago de este socio para el mismo mes y año.";

                pago.Ahorro = dto.Ahorro;
                pago.Rifa = dto.Rifa;
                pago.Polla = dto.Polla;
                pago.FechaPago = dto.FechaPago;

                dbNatillera.SaveChanges();
                return "Pago actualizado correctamente.";
            }
            catch (Exception ex)
            {
                return $"Error al actualizar el pago: {ex.Message}";
            }
        }
        public List<PagoDetalladoDto> ObtenerPagosDetalladosPorMes(int mes, int anio)
        {
            var pagos = dbNatillera.Pagos
                .Where(p => p.FechaPago.Month == mes && p.FechaPago.Year == anio && p.Estado)
                .Select(p => new PagoDetalladoDto
                {
                    IdPago = p.Id,
                    Documento = p.IdSocioNavigation.Documento ?? 0,
                    Nombre = p.IdSocioNavigation.Nombre,
                    Ahorro = p.Ahorro,
                    Rifa = p.Rifa,
                    Polla = p.Polla,
                    FechaPago = p.FechaPago
                })
                .ToList();

            return pagos;
        }



    }
}

using Microsoft.EntityFrameworkCore;
using NatilleraBE.Data;
using NatilleraBE.DTOs;
using NatilleraBE.Models;
using NatilleraBE.Utils; 

namespace NatilleraBE.Services
{
    public class clsSocio
    {
        private NatilleraDbContext dbNatillera = new NatilleraDbContext();
        public Socio socio { get; set; }

        public string InsertarSocio(CrearSocioDto dto)
        {
            try
            {
                if (dto.Documento <= 0)
                    return "El documento es obligatorio y debe ser positivo.";

                if (string.IsNullOrWhiteSpace(dto.Nombre) || dto.Nombre.Length < 3)
                    return "El nombre debe tener al menos 3 caracteres.";

                if (string.IsNullOrWhiteSpace(dto.Numero) || dto.Numero.Length != 2 || !dto.Numero.All(char.IsDigit))
                    return "El número debe tener exactamente 2 dígitos numéricos.";

                if (!dbNatillera.Rols.Any(r => r.Id == dto.IdRol))
                    return "El rol asignado no existe.";

                if (dbNatillera.Socios.Any(s => s.Documento == dto.Documento))
                    return "Ya existe un socio con ese documento.";

                var (claveHash, salt) = PasswordHelper.HashPassword(dto.Clave);

                var socio = new Socio
                {
                    Documento = dto.Documento,
                    Nombre = dto.Nombre,
                    Numero = dto.Numero,
                    Clave = claveHash,
                    Salt = salt,
                    Estado = true,
                    IdRol = dto.IdRol
                };

                dbNatillera.Socios.Add(socio);
                dbNatillera.SaveChanges();
                return "Socio insertado correctamente.";
            }
            catch (Exception ex)
            {
                return $"Error al insertar socio: {ex.Message}";
            }
        }
        public List<BuscarSocioDto> ObtenerTodos()
        {
            return dbNatillera.Socios
                .Include(s => s.IdRolNavigation) 
                .Where(s => s.Estado == true)
                .Select(s => new BuscarSocioDto
                {
                    Id = s.Id,                            
                    Documento = s.Documento ?? 0,
                    Nombre = s.Nombre,
                    Numero = s.Numero,
                    Rol = s.IdRolNavigation != null ? s.IdRolNavigation.Nombre : "",
                }).ToList();
        }


        public BuscarSocioDto? ObtenerXDocumento(int documento)
        {
            var socio = dbNatillera.Socios
                .Include(s => s.IdRolNavigation)
                .FirstOrDefault(s => s.Documento == documento && s.Estado == true);
            if (socio == null) return null;

            return new BuscarSocioDto
            {
                Documento = socio.Documento ?? 0,
                Nombre = socio.Nombre,
                Numero = socio.Numero,
                Rol = socio.IdRolNavigation != null ? socio.IdRolNavigation.Nombre : "",
            };
        }
        public string EliminarPorDocumento(int documento)
        {
            var socio = dbNatillera.Socios.FirstOrDefault(s => s.Documento == documento && s.Estado == true);

            if (socio == null)
                return "Socio no encontrado o ya inactivo.";

            socio.Estado = false;
            try
            {
                dbNatillera.SaveChanges();
                return "Socio eliminado correctamente.";
            }
            catch (Exception ex)
            {
                return $"Error al eliminar socio: {ex.Message}";
            }
        }
        public string ActivarPorDocumento(int documento)
        {
            var socio = dbNatillera.Socios.FirstOrDefault(s => s.Documento == documento);

            if (socio == null)
                return "Socio no encontrado.";

            if (socio.Estado)
                return "El socio ya está activo.";

            socio.Estado = true;

            try
            {
                dbNatillera.SaveChanges();
                return "Socio activado correctamente.";
            }
            catch (Exception ex)
            {
                return $"Error al activar socio: {ex.Message}";
            }
        }
        public List<string> ObtenerNumerosDisponibles()
        {
            var usados = dbNatillera.Socios
                .Where(s => s.Estado == true && s.Numero != null)
                .Select(s => s.Numero)
                .ToHashSet();

            var disponibles = new List<string>();

            for (int i = 0; i < 100; i++)
            {
                string num = i.ToString("D2");
                if (!usados.Contains(num))
                {
                    disponibles.Add(num);
                }
            }

            return disponibles;
        }
        public async Task<List<SocioConNumeroDto>> ObtenerSociosConNumerosAsync()
        {
            return await dbNatillera.Socios
                .Select(s => new SocioConNumeroDto
                {
                    IdSocio = s.Id,
                    Nombre = s.Nombre,
                    Numero = s.Numero
                })
                .ToListAsync();
        }
        public List<BuscarSocioDto> ObtenerInactivos()
        {
            return dbNatillera.Socios
                .Where(s => s.Estado == false)
                .Select(s => new BuscarSocioDto
                {
                    Documento = s.Documento ?? 0,
                    Nombre = s.Nombre,
                    Numero = s.Numero,
                    Rol = s.IdRolNavigation != null ? s.IdRolNavigation.Nombre : ""
                }).ToList();
        }

    }
}

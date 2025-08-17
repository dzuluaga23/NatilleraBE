using NatilleraBE.Data;
using NatilleraBE.Models;
using Newtonsoft.Json;

namespace NatilleraBE.Services
{
    public class clsPolla
    {
        private readonly NatilleraDbContext _context;
        private readonly HttpClient _httpClient;

        public clsPolla(NatilleraDbContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        private class NumeroConSocio
        {
            public int IdSocio { get; set; }
            public string Nombre { get; set; } = string.Empty;
            public string Numero { get; set; } = string.Empty;
        }

        public string RegistrarPolla(Polla polla)
        {
            var existe = _context.Pollas.FirstOrDefault(p => p.Mes == polla.Mes);
            if (existe != null)
                return $"Ya existe una polla registrada para el mes {polla.Mes}";

            var response = _httpClient.GetAsync("http://localhost:5164/api/Socios/NumerosConSocios").Result;
            if (!response.IsSuccessStatusCode)
                return "Error al consultar NumerosConSocios";

            var json = response.Content.ReadAsStringAsync().Result;
            var lista = JsonConvert.DeserializeObject<List<NumeroConSocio>>(json);

            var ganador = lista.FirstOrDefault(x => x.Numero == polla.Numero.ToString());

            if (ganador != null)
            {
                polla.Estado = true; 
                _context.Pollas.Add(polla);
                _context.SaveChanges();
                return $"¡Hubo ganador! El socio es {ganador.Nombre} con el número {polla.Numero}";
            }
            else
            {
                polla.Estado = false; 
                _context.Pollas.Add(polla);
                _context.SaveChanges();
                return $"No hubo ganador para el número {polla.Numero}";
            }
        }

        public List<Polla> ConsultarPollas()
        {
            return _context.Pollas.ToList();
        }
    }
}

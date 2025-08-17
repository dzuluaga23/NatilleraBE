namespace NatilleraBE.DTOs
{
    public class ResumenPagosMesDto
    {
        public List<ResumenPagoSocioDto> Socios { get; set; } = new List<ResumenPagoSocioDto>();
        public decimal TotalAhorro { get; set; }
        public decimal TotalPolla { get; set; }
        public decimal TotalRifa { get; set; }
        public decimal TotalInteres { get; set; }
    }
}

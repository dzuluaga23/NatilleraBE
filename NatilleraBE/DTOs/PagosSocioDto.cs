namespace NatilleraBE.DTOs
{
    public class PagosSocioDto
    {
        public int Documento { get; set; }
        public string Nombre { get; set; } = null!;
        public List<PagoDetalleDto> Pagos { get; set; } = new();
        public decimal TotalAhorro { get; set; }
        public decimal TotalPolla { get; set; }
        public decimal TotalRifa { get; set; }
        public decimal TotalIntereses { get; set; }

    }

    public class PagoDetalleDto
    {
        public DateOnly FechaPago { get; set; }
        public decimal Ahorro { get; set; }
        public decimal Polla { get; set; }
        public decimal Rifa { get; set; }
    }
}

namespace NatilleraBE.DTOs
{
    public class PagoDetalladoDto
    {
        public int IdPago { get; set; }
        public int Documento { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Ahorro { get; set; }
        public decimal Rifa { get; set; }
        public decimal Polla { get; set; }
        public DateOnly FechaPago { get; set; }
    }
}

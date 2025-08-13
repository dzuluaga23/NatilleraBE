namespace NatilleraBE.DTOs
{
    public class ActualizarPagoDto
    {
        public int IdPago { get; set; }
        public decimal Ahorro { get; set; }
        public decimal Rifa { get; set; }
        public decimal Polla { get; set; }
        public DateOnly FechaPago { get; set; }
    }
}

namespace NatilleraBE.DTOs
{
    public class RegistrarPagoDto
    {
        public int IdSocio { get; set; }
        public decimal Rifa { get; set; }
        public decimal Polla { get; set; }
        public decimal Ahorro { get; set; }
        public DateOnly FechaPago { get; set; }
    }
}

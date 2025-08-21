namespace NatilleraBE.DTOs
{
    public class AbonoCreateDto
    {
        public decimal Valor { get; set; }
        public int IdPrestamo { get; set; }
        public DateOnly Fecha { get; set; }
    }

    public class AbonoConsultaDto
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public DateOnly Fecha { get; set; }
        public decimal ValorRestante { get; set; }
        public int IdPrestamo { get; set; }
    }
}

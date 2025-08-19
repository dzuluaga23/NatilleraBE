namespace NatilleraBE.DTOs
{
    public class InteresPrestamoDto
    {
        public decimal Valor { get; set; }
        public int IdPrestamo { get; set; }
    }

    public class InteresConsultaDto
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public DateOnly Fecha { get; set; }
        public int IdPrestamo { get; set; }
    }
}

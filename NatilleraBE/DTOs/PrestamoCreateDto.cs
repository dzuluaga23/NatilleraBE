namespace NatilleraBE.DTOs
{
    public class PrestamoCreateDto
    {
        public decimal Valor { get; set; }
        public int IdSocio { get; set; }
        public DateOnly Fecha { get; set; }  
    }

    public class PrestamoConsultaDto
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public DateOnly Fecha { get; set; }
        public int FechaCorte { get; set; }
        public int IdSocio { get; set; }
        public string NombreSocio { get; set; } = string.Empty;

        public decimal TotalIntereses { get; set; }
        public decimal TotalAbonos { get; set; }
        public decimal SaldoPendiente { get; set; }
    }
}

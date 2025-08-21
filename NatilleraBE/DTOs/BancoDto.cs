namespace NatilleraBE.DTOs
{
    public class BancoDto
    {
        public int Id { get; set; }
        public decimal? Efectivo { get; set; }
        public decimal? Cuenta { get; set; }
        public decimal? Ahorro { get; set; }
        public decimal Total { get; set; }
    }

    public class BancoUpdateDto
    {
        public decimal? Efectivo { get; set; }
        public decimal? Cuenta { get; set; }
        public decimal? Ahorro { get; set; }
    }
}

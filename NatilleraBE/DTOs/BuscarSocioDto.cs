namespace NatilleraBE.DTOs
{
    public class BuscarSocioDto
    {
        public int Id { get; set; }
        public int Documento { get; set; }
        public string Nombre { get; set; } = null!;
        public string Numero { get; set; } = null!;
        public string Rol { get; set; } = null!;
    }
}

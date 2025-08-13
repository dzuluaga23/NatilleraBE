using System;
using System.Collections.Generic;

namespace NatilleraBE.Models;

public partial class Socio
{
    public int Id { get; set; }

    public int? Documento { get; set; }

    public string Nombre { get; set; } = null!;

    public string Numero { get; set; } = null!;

    public bool Estado { get; set; }

    public string? Clave { get; set; }

    public string? Salt { get; set; }

    public int? IdRol { get; set; }

    public virtual Rol? IdRolNavigation { get; set; }

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}

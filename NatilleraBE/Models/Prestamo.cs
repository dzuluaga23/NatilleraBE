using System;
using System.Collections.Generic;

namespace NatilleraBE.Models;

public partial class Prestamo
{
    public int Id { get; set; }

    public decimal? Valor { get; set; }

    public DateOnly Fecha { get; set; }

    public int FechaCorte { get; set; }

    public int IdSocio { get; set; }

    public virtual ICollection<AbonosPrestamo> AbonosPrestamos { get; set; } = new List<AbonosPrestamo>();

    public virtual Socio IdSocioNavigation { get; set; } = null!;

    public virtual ICollection<InteresPrestamo> InteresPrestamos { get; set; } = new List<InteresPrestamo>();
}

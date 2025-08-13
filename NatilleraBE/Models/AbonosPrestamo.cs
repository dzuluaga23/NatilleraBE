using System;
using System.Collections.Generic;

namespace NatilleraBE.Models;

public partial class AbonosPrestamo
{
    public int Id { get; set; }

    public decimal? Valor { get; set; }

    public DateOnly Fecha { get; set; }

    public decimal ValorRestante { get; set; }

    public int IdPrestamo { get; set; }

    public virtual Prestamo IdPrestamoNavigation { get; set; } = null!;
}

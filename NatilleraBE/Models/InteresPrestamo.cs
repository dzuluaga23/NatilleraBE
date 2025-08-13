using System;
using System.Collections.Generic;

namespace NatilleraBE.Models;

public partial class InteresPrestamo
{
    public int Id { get; set; }

    public decimal Valor { get; set; }

    public DateOnly Fecha { get; set; }

    public int DiasMora { get; set; }

    public int IdPrestamo { get; set; }

    public virtual Prestamo IdPrestamoNavigation { get; set; } = null!;
}

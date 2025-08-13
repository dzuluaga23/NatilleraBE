using System;
using System.Collections.Generic;

namespace NatilleraBE.Models;

public partial class Pago
{
    public int Id { get; set; }

    public decimal Rifa { get; set; }

    public decimal Polla { get; set; }

    public decimal Ahorro { get; set; }

    public DateOnly FechaPago { get; set; }

    public bool Estado { get; set; }

    public int IdSocio { get; set; }

    public virtual Socio IdSocioNavigation { get; set; } = null!;

    public virtual ICollection<InteresPago> InteresPagos { get; set; } = new List<InteresPago>();
}

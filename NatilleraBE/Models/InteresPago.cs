using System;
using System.Collections.Generic;

namespace NatilleraBE.Models;

public partial class InteresPago
{
    public int Id { get; set; }

    public int Dias { get; set; }

    public decimal ValorTotal { get; set; }

    public int IdPago { get; set; }

    public virtual Pago IdPagoNavigation { get; set; } = null!;
}

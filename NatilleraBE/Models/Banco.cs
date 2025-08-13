using System;
using System.Collections.Generic;

namespace NatilleraBE.Models;

public partial class Banco
{
    public int Id { get; set; }

    public decimal? Efectivo { get; set; }

    public decimal? Cuenta { get; set; }

    public decimal? Ahorro { get; set; }
}

using System;
using System.Collections.Generic;

namespace NatilleraBE.Models;

public partial class Polla
{
    public int Id { get; set; }

    public string? Mes { get; set; }

    public byte? Numero { get; set; }

    public bool? Estado { get; set; }
}

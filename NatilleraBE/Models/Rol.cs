using System;
using System.Collections.Generic;

namespace NatilleraBE.Models;

public partial class Rol
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Socio> Socios { get; set; } = new List<Socio>();
}

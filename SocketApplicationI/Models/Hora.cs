using System;
using System.Collections.Generic;

namespace SocketApplicationI.Models;

public partial class Hora
{
    public int Iidhora { get; set; }

    public string? Hora1 { get; set; }

    public int? Bhabilitado { get; set; }

    public virtual ICollection<HoraDiaActividadPersona> HoraDiaActividadPersonas { get; set; } = new List<HoraDiaActividadPersona>();
}

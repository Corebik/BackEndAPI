using System;
using System.Collections.Generic;

namespace BackEndAPI.Models;

public partial class Casa
{
    public int IdCasa { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Registro> Registros { get; } = new List<Registro>();
}

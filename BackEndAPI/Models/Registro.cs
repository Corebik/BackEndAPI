using System;
using System.Collections.Generic;

namespace BackEndAPI.Models;

public partial class Registro
{
    public int IdRegistro { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public int? Identificacion { get; set; }

    public int? Edad { get; set; }

    public int? RefCasa { get; set; }

    public virtual Casa? RefCasaNavigation { get; set; }
}

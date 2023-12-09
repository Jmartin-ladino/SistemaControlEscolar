using System;
using System.Collections.Generic;

namespace SistemaControlEscolar.Models;

public partial class Profesor
{
    public int Id { get; set; }

    public string? Profesor1 { get; set; }

    public string? Correo { get; set; }

    public virtual ICollection<Alumno> Alumnos { get; } = new List<Alumno>();
}

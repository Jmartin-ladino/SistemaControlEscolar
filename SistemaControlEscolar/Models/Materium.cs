using System;
using System.Collections.Generic;

namespace SistemaControlEscolar.Models;

public partial class Materium
{
    public int Id { get; set; }

    public string? Materia { get; set; }

    public int? Calificacion { get; set; }

    public bool? Acreditada { get; set; }

    public int IdAlumno { get; set; }

    public virtual Alumno IdAlumnoNavigation { get; set; } = null!;
}

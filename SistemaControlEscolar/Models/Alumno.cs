using System;
using System.Collections.Generic;

namespace SistemaControlEscolar.Models;

public partial class Alumno
{
    public int Id { get; set; }

    public string? Alumno1 { get; set; }

    public string? Correo { get; set; }

    public int IdProfesor { get; set; }

    public virtual Profesor IdProfesorNavigation { get; set; } = null!;

    public virtual ICollection<Materium> Materia { get; } = new List<Materium>();
}

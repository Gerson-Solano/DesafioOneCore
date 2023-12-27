using System;
using System.Collections.Generic;

namespace CapaDatos
{
    public partial class Registro
    {
        public int IdRegistro { get; set; }
        public string? Tipo { get; set; }
        public string? Descripccion { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SystemIA.Entity
{
    public partial class Registro
    {
        [Key]
        public int IdRegistro { get; set; }
        public string? Tipo { get; set; }
        public string? Descripccion { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }
}

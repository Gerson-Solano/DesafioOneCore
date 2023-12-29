﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CapaDatos
{
    public partial class Registro
    {
        public int IdRegistro { get; set; }
        public string? Tipo { get; set; }
        public string? Descripccion { get; set; }
        [DisplayFormat(DataFormatString = "{dd/MM/yy}")]  // Formato para la presentación
        public DateTime? FechaRegistro { get; set; }
    }
}

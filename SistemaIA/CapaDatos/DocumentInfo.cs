using System;
using System.Collections.Generic;

namespace CapaDatos
{
    public partial class DocumentInfo
    {
        public int IdDocument { get; set; }
        public string? Descripccion { get; set; }
        public string? Resumen { get; set; }
        public string? Sentimiento { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SystemIA.Entity
{
    public partial class DocumentInfo
    {
        [Key]
        public int IdDocument { get; set; }
        public string? Descripccion { get; set; }
        public string? Resumen { get; set; }
        public string? Sentimiento { get; set; }
    }
}

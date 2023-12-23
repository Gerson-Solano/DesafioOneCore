using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SystemIA.Entity
{
    public partial class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public string? NombreCompleto { get; set; }

        //[DataType(DataType.EmailAddress)]
        public string? Correo { get; set; }
    }
}

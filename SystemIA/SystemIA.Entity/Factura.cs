using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SystemIA.Entity
{
    public partial class Factura
    {
        public Factura()
        {
            ProductosFacturas = new HashSet<ProductosFactura>();
        }
        [Key]
        public int NumFactura { get; set; }
        public string? NombreCliente { get; set; }
        public string? DireccionCliente { get; set; }
        public string? NombreProveedor { get; set; }
        public string? DireccionProveedor { get; set; }
        public DateTime? Fecha { get; set; }
        public decimal? Total { get; set; }

        public virtual ICollection<ProductosFactura> ProductosFacturas { get; set; }
    }
}

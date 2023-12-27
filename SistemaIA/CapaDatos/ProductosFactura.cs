using System;
using System.Collections.Generic;

namespace CapaDatos
{
    public partial class ProductosFactura
    {
        public int IdProducto { get; set; }
        public int? NumFactura { get; set; }
        public string? Nombre { get; set; }
        public decimal? PrecioUnitario { get; set; }
        public int? Cantidad { get; set; }
        public decimal? Total { get; set; }

        public virtual Factura? NumFacturaNavigation { get; set; }
    }
}

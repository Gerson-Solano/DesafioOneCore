using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Interfaces
{
    public interface IProductos
    {
        List<ProductosFactura> getProductosByIdFactura(int idFactura);
        string createProductos(ProductosFactura productos);

        int getLastFacturaBytheId();

    }
}

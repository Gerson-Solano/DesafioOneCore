using CapaDatos;
using CapaNegocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Clases
{
    public class lCProductos:IProductos
    {
        private DBIASYSTEMContext dbContext;

        public lCProductos(DBIASYSTEMContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<ProductosFactura> getProductosByIdFactura(int idFactura)
        {
            return dbContext.ProductosFactura.Where(x=>x.NumFactura==idFactura).ToList();
        }
        public int getLastFacturaBytheId()
        {
            return dbContext.Facturas.OrderByDescending(x=>x.NumFactura).FirstOrDefault().NumFactura;
        }
        public string createProductos(ProductosFactura productos)
        {
            try
            {
                if (productos!=null)
                {
                    dbContext.ProductosFactura.Add(productos);
                    dbContext.SaveChanges();
                    return "Se creo con exito";
                }
                else
                {
                    return "Viene vacio";
                }
            }
            catch (Exception ex)
            {
                return(ex.Message);
            }
            
        }
    }
}

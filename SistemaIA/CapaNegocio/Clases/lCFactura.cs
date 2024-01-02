using CapaDatos;
using CapaNegocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Clases
{
    public class lCFactura:IFactura
    {
        private DBIASYSTEMContext dbContext;

        public lCFactura(DBIASYSTEMContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Factura getLastFactura()
        {
            return dbContext.Facturas.OrderByDescending(x=>x.NumFactura).FirstOrDefault();
        }

        public string createFactura(Factura factura)
        {
            try
            {
                if (factura!=null)
                {
                    dbContext.Facturas.Add(factura);
                    dbContext.SaveChanges();
                    return ("La factura se guardo con exito");
                }
                else
                {
                    return("La factura se encuentra vacia, favor intentelo de nuevo");
                }
            }
            catch (Exception ex)
            {

                return(ex.Message);
            }
        }

    }
}

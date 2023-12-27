using CapaDatos;
using CapaNegocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Clases
{
    public class lResgistro: IRegistro
    {
        private DBIASYSTEMContext dbContext;

        public lResgistro(DBIASYSTEMContext dbContext)
        {
            this.dbContext = dbContext; 
        }

        public List<Registro> GetRegistros()
        {
            return dbContext.Registros.ToList();
        }
    }
}

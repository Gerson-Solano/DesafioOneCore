using CapaDatos;
using CapaNegocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Clases
{
    public class lCUsuario:IUsuario
    {
        private DBIASYSTEMContext dbContext;

        public lCUsuario(DBIASYSTEMContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public string createUser(Usuario user)
        {
            try
            {
                if (user!=null)
                {
                    dbContext.Usuarios.Add(user);
                    dbContext.SaveChanges();
                    return ("El usuario se creo con exito");
                }
                else
                {
                    return ("El usuario se encuentra vacio");
                }
            }
            catch (Exception ex)
            {

                return (ex.Message);
            }
        }

        public Usuario getUserByEmail(string email)
        {
            return dbContext.Usuarios.Where(x=>x.Correo==email).FirstOrDefault();
        }



    }
}

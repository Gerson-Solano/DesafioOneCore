using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Interfaces
{
    public interface IUsuario
    {
        string createUser(Usuario user);

        Usuario getUserByEmail(string email);
    }
}

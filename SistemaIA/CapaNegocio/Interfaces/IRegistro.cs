using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Interfaces
{
    public interface IRegistro
    {
        List<Registro> GetRegistros();

        Registro GetRegistroById(int id);

        string updateRegistro(Registro registro);
        string createRegistro(Registro registro);

        List<Registro> GetRegistrosByFecha(DateTime fecha);

        List<Registro> GetRegistrosByNombre(string nombre);

        List<Registro> GetRegistrosPorRangoDeFechas(DateTime fechaInicio, DateTime fechaFin);


    }
}

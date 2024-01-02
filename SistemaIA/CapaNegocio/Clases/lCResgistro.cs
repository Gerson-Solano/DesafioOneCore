using CapaDatos;
using CapaNegocio.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Clases
{
    public class lCResgistro: IRegistro
    {
        private DBIASYSTEMContext dbContext;

        public lCResgistro(DBIASYSTEMContext dbContext)
        {
            this.dbContext = dbContext; 
        }

        public List<Registro> GetRegistros()
        {
            return dbContext.Registros.ToList();
        }

        public Registro GetRegistroById(int id)
        {
            return dbContext.Registros.Find(id);
        }

        public string updateRegistro(Registro registro)
        {
            try
            {
                if (registro != null)
                {
                    dbContext.Registros.Update(registro);
                    dbContext.SaveChanges();
                    return "El registro se actualizo con exito";
                }
                else
                {
                    return "El registro se encuentra vacio, intentelo de nuevo";
                }
            }
            catch (Exception ex)
            {

                return (ex.Message);
            }
            
        }

        public string createRegistro(Registro registro)
        {
            try
            {
                if (registro != null)
                {
                    dbContext.Registros.Add(registro);
                    dbContext.SaveChanges();
                    return "El registro se creo con exito";
                }
                else
                {
                    return "El registro se encuentra vacio, intentelo de nuevo";
                }
            }
            catch (Exception ex)
            {

                return (ex.Message);
            }
        }
        public List<Registro> GetRegistrosByNombre(string nombre)
        {
            return dbContext.Registros.Where(r => r.Descripccion.Contains(nombre)).ToList();
        }
        public List<Registro> GetRegistrosByFecha(DateTime fecha)
        {
            fecha = fecha.ToUniversalTime().Date;

            return dbContext.Registros
                .Where(r => r.FechaRegistro.HasValue && EF.Functions.DateDiffDay(r.FechaRegistro.Value, fecha) == 0)
                .ToList();
        }

        public List<Registro> GetRegistrosPorRangoDeFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            fechaInicio = fechaInicio.ToUniversalTime().Date;
            fechaFin = fechaFin.ToUniversalTime().Date;

            return dbContext.Registros
                .Where(r => r.FechaRegistro.HasValue && r.FechaRegistro.Value.Date >= fechaInicio && r.FechaRegistro.Value.Date <= fechaFin)
                .ToList();
        }
    }
}

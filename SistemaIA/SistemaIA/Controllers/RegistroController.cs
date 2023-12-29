using CapaDatos;
using CapaNegocio.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SistemaIA.Controllers
{
   

    [Route("api/[controller]")]
    [ApiController]
    public class RegistroController : ControllerBase
    {
        IRegistro _registro;

        public RegistroController(IRegistro registro)
        {
            _registro = registro;
        }



        // GET: api/<RegistroController>
        [HttpGet]
        public IEnumerable<Registro> Get()
        {
            return _registro.GetRegistros();
        }

        // GET api/<RegistroController>/5
        [HttpGet("{id}")]
        public Registro Get(int id)
        {
            return _registro.GetRegistroById(id);
        }

        // POST api/<RegistroController>
        [HttpPost]
        public string Post(Registro registro)
        {
            return _registro.createRegistro(registro);
        }

        // PUT api/<RegistroController>/5
        [HttpPut("{id}")]
        public void Put(int id)
        {

        }

        // DELETE api/<RegistroController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


        [HttpGet("ByFecha")]
        public IEnumerable<Registro> GetByFecha(DateTime fecha)
        {
            return _registro.GetRegistrosByFecha(fecha);
        }

        [HttpGet("ByNombre")]
        public IEnumerable<Registro> GetByNombre(string txt)
        {
            return _registro.GetRegistrosByNombre(txt);
        }

        [HttpGet("ByRangoFechas")]
        public IEnumerable<Registro> GetByRangoFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            return _registro.GetRegistrosPorRangoDeFechas(fechaInicio, fechaFin);
        }
    }
}

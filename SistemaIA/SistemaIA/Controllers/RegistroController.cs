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
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RegistroController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RegistroController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RegistroController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

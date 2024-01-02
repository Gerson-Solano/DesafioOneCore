using CapaDatos;
using CapaNegocio.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SistemaIA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        IFactura _factura;

        public FacturaController(IFactura _factura)
        {
            this._factura = _factura;
        }
        // GET: api/<FacturaController>
        [HttpGet]
        public Factura Get()
        {
            return _factura.getLastFactura();
        }

        // GET api/<FacturaController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FacturaController>
        [HttpPost]
        public void Post(Factura factura)
        {
            _factura.createFactura(factura);
        }

        // PUT api/<FacturaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FacturaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

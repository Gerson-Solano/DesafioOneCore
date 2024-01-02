using CapaDatos;
using CapaNegocio.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SistemaIA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        IProductos _productos;
        public ProductosController(IProductos _productos)
        {
            this._productos = _productos;
        }

        // GET: api/<ProductosController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ProductosController>/5
        [HttpGet("{id}")]
        public IEnumerable<ProductosFactura> Get(int idFactura)
        {
            return _productos.getProductosByIdFactura(idFactura);
        }

        // POST api/<ProductosController>
        [HttpPost]
        public void Post(ProductosFactura productos)
        {
            var idFactura = _productos.getLastFacturaBytheId();
            productos.NumFactura = idFactura;   
            _productos.createProductos(productos);
        }

        // PUT api/<ProductosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

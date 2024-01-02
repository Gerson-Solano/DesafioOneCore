using CapaDatos;
using CapaNegocio.Clases;
using CapaNegocio.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SistemaIA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextAnalisisController : ControllerBase
    {
        ITextAnalisis _textAnalisis;
        public TextAnalisisController(ITextAnalisis _textAnalisis)
        {
            this._textAnalisis = _textAnalisis;
        }
        // GET: api/<TextAnalisisController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TextAnalisisController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        [HttpPost("subirArchivo")]
        public int SubirArchivo(ArchivoRequest archivoRequest)
        {
           return _textAnalisis.identificaryGestionarArchivo(archivoRequest);
        }
        // POST api/<TextAnalisisController>
        [HttpPost]
        public DocumentInfo Post(string value)
        {
            return _textAnalisis.getDocumentInfo(value);
        }

        // PUT api/<TextAnalisisController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TextAnalisisController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

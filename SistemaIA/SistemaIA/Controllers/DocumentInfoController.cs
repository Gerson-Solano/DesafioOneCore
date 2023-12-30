using CapaDatos;
using CapaNegocio.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SistemaIA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentInfoController : ControllerBase
    {
        IDocumentInfo Idocument;

        public DocumentInfoController(IDocumentInfo Idocument)
        {
            this.Idocument = Idocument;
        }
        // GET: api/<DocumentInfoController>
        [HttpGet]
        public DocumentInfo Get()
        {
            return Idocument.getLastDocumentInfo();  
        }

        // GET api/<DocumentInfoController>/5
        [HttpGet("{id}")]
        public DocumentInfo Get(int id)
        {
            return Idocument.getDocumentInfo(id);
        }

        // POST api/<DocumentInfoController>
        [HttpPost]
        public void Post(DocumentInfo document)
        {
            Idocument.createDocumntInfo(document);
        }

        // PUT api/<DocumentInfoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DocumentInfoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

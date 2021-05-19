using Doctrim.EF.Models;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Doctrim.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentTypesController : ControllerBase
    {
        private IDoctrimDBService _service;
        public DocumentTypesController(IDoctrimDBService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<DocumentType>>> GetTypes()
        {

            try
            {
                var x = await _service.GetAllDocumentTypes();
                if (x != null)
                {
                    return Ok(x);
                }

                //creates two agreements when starting up for the first time.
                if (x == null)
                {
                    DocumentType type1 = new DocumentType() { Type = "Terminal leasing" };
                    await _service.CreateDocumentType(type1);
                    DocumentType type2 = new DocumentType() { Type = "Acquiring Agreement" };
                    await _service.CreateDocumentType(type1);
                    var y = await _service.GetAllDocumentTypes();
                    return Ok(y);

                }
                else
                {
                    return StatusCode(404);
                }
            }
            catch
            {
                return StatusCode(500);
            }

        }
        
    }
}

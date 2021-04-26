using Doctrim.EF.Models;
using Microsoft.AspNetCore.Mvc;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
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

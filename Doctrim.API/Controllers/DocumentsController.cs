using Doctrim.EF.Data;
using Doctrim.EF.Models;
using Microsoft.AspNetCore.Mvc;
using Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Threading.Tasks;


namespace Doctrim.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private IDoctrimDBService _service;

        public DocumentsController(IDoctrimDBService service)
        {
            _service = service;
        }

        // GET: localhost:XXXXX/api/documents
        // Makes a call to the api and get all documents in the database as answer
        [HttpGet]
        public async Task<ActionResult<List<DocumentFile>>> GetAllDocuments()
        {
            try
            {
                var x = await _service.GetAllDocuments();
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

        // GET: localhost:XXXXX/api/documents/X
        // Makes a call to the api with an id of selected document, API returns the selected document as download.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocument(int id)
        {
            try
            {
                DocumentFile documentFile = await _service.GetDocument(id);
                if (documentFile != null)
                {
                    byte[] fileBytes = System.IO.File.ReadAllBytes(documentFile.DocumentPath);
                    string filename = documentFile.DocumentPath;
                    return Ok(File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filename));
                }
                else return StatusCode(404);
            }
            catch
            {
                return StatusCode(500);
            }

        }

        // Post: localhost:XXXXX/api/documents/documentFile
        // Makes a post to the API containing the the document file and its metadata
        [HttpPost]
        public async Task<IActionResult> UploadDocument([FromBody]DocumentFile documentFile)
        {
            try
            {
                if (await _service.CreateDocument(documentFile))
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(400, "Can't create document, check if all required parameters are filled in");
                }
            }
            catch
            {
                return StatusCode(500);
            }
           

        }

        // GET: localhost:XXXXX/api/documents/X
        // Makes a call to the api with an id of selected document, API returns the selected document as download.


      
           

           



    }
}

using AutoMapper;
using Doctrim.DTOs;
using Doctrim.EF.Data;
using Doctrim.EF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
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
        private IDoctrimDBService _dbService;
        private IDoctrimAPIService _apiService;
        private IMapper _mapper;


        public DocumentsController(IDoctrimDBService dbService,
            IDoctrimAPIService apiService,
            IMapper mapper)
        {
            _dbService = dbService;
            _apiService = apiService;
            _mapper = mapper;
            
        }
        #region GET

        // GET: localhost:XXXXX/api/documents
        // Makes a call to the api and get all documents in the database as answer
        [HttpGet]
        public async Task<ActionResult<List<DocumentFileDTO>>> GetAllDocuments()
        {
            try
            {
                var documentFiles = await _dbService.GetAllDocuments();
                List<DocumentFileDTO> documentFilesDTO = _mapper.Map<List<DocumentFileDTO>>(documentFiles);
                if (documentFilesDTO != null)
                {
                    return Ok(documentFilesDTO);
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

        // GET: localhost:XXXXX/api/documents/download/id
        // Makes a call to the api with an id of selected document, API returns the selected document as download.

        [HttpGet("Download/{UniqueId:Guid}")]
        public async Task<IActionResult> GetDocument(Guid UniqueId)
        {
            try
            {
                DocumentFile documentFile = await _dbService.GetDocument(UniqueId);
                if (documentFile != null)
                {

                    var provider = new FileExtensionContentTypeProvider();
                    if (!provider.TryGetContentType(documentFile.DocumentPath, out var contentType))
                    {
                        contentType = "application/octet-stream";
                    }

                    var fileBytes = System.IO.File.ReadAllBytes(documentFile.DocumentPath);
                    string filename = documentFile.DocumentPath;
                    return File(fileBytes, contentType, Path.GetFileName(filename));
                }
                else return StatusCode(404);
            }
            catch
            {
                return StatusCode(500);
            }

        }

        // GET: localhost:XXXXX/api/documents/bytype/type
        // Returns a list of documents that is of a specific type.
        [HttpGet("ByType/{type:Guid}")]
        public async Task<ActionResult<List<DocumentFileDTO>>> GetAllDocumentsByType(Guid type)
        {
            try
            {

                var documentFiles = await _dbService.GetDocumentsFromType(type);
                List<DocumentFileDTO> documentFilesDTO = _mapper.Map<List<DocumentFileDTO>>(documentFiles);
                if (documentFilesDTO != null)
                {
                    return Ok(documentFilesDTO);
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

        // GET: localhost:XXXXX/api/documents/betweendates/date&&date
        // Returns a list of documents that is of a specific type.
        [HttpGet("BetweenDates/{first:DateTime}&&{last:DateTime}")]
        public async Task<ActionResult<List<DocumentFileDTO>>> GetDocumentsBetweenDates(DateTime first, DateTime last)
        {
            try
            {

                var documentFiles = await _dbService.GetDocumentsBetweenDates(first, last);
                List<DocumentFileDTO> documentFilesDTO = _mapper.Map<List<DocumentFileDTO>>(documentFiles);
                if (documentFilesDTO != null)
                {
                    return Ok(documentFilesDTO);
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

        // GET: localhost:XXXXX/api/documents/fromtag/tag
        // Returns a list of documents that is of a specific type.
        [HttpGet("FromTag/{tag}")]
        public async Task<ActionResult<List<DocumentFileDTO>>> GetDocumentsFromTag(string tag)
        {
            try
            {                
                
                var documentFiles = await _dbService.GetDocumentsFromTag(tag);
                List<DocumentFileDTO> documentFilesDTO = _mapper.Map<List<DocumentFileDTO>>(documentFiles);
                if (documentFilesDTO != null)
                {
                    return Ok(documentFilesDTO);
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

        [HttpGet("Search")]
        public async Task<ActionResult<List<DocumentFileDTO>>> GetDocumentsFromTag([FromQuery]SearchDTO searchParameters)
        {
            try
            {

                var documentFiles = await _dbService.DocumentSearch(searchParameters);
                List<DocumentFileDTO> documentFilesDTO = _mapper.Map<List<DocumentFileDTO>>(documentFiles);
                if (documentFilesDTO != null)
                {
                    return Ok(documentFilesDTO);
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


        #endregion

        #region POST
        // Post: localhost:XXXXX/api/documents
        // Makes a post to the API containing the the document file and its metadata
        [HttpPost]
        public async Task<IActionResult> UploadDocument([FromBody] DocumentPostDTO postDTO)
        {
            try
            {
                DocumentFile documentFile = _mapper.Map<DocumentFile>(postDTO.DocumentFile);
                documentFile = await _apiService.DocumentUpload(documentFile, postDTO.FileByteArray);

                if (documentFile.DocumentPath != null)
                {
                    if (await _dbService.CreateDocument(documentFile))
                    {
                        return Ok();
                    }
                    else
                    {
                        return StatusCode(400, "Can't create document, check if all required parameters are filled in");
                    }
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

        #endregion
    }






}


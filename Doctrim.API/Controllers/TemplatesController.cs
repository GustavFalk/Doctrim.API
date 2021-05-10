using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Doctrim.DTOs;
using Doctrim.EF.Models;
using Microsoft.AspNetCore.StaticFiles;
using System.IO;

namespace Doctrim.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplatesController : ControllerBase
    {
        private IDoctrimDBService _dbService;
        private IDoctrimAPIService _apiService;
        private IMapper _mapper;

        public TemplatesController(IDoctrimDBService dbService,
         IDoctrimAPIService apiService,
         IMapper mapper)
        {
            _dbService = dbService;
            _apiService = apiService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<DocumentTemplateDTO>>> GetTemplates()
        {
            

            try
            {
                var templates = await _dbService.GetTemplates();
                List<DocumentTemplateDTO> templatesDTO = _mapper.Map<List<DocumentTemplateDTO>>(templates);
                if (templatesDTO != null)
                {
                    return Ok(templatesDTO);
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

        //TODO: Add selected datasource here.
        [HttpGet("GenerateDocument/{selectedTemplate:Guid}")]
        public async Task<IActionResult> GenerateDocument(Guid selectedTemplate)
        {
            try
            {
                DocumentTemplate documentTemplate = await _dbService.GetTemplate(selectedTemplate);
                if (documentTemplate != null)
                {
                    string filledTemplatePath = await _apiService.FillTemplate(documentTemplate);
                    if (filledTemplatePath != null)
                    {
                        var provider = new FileExtensionContentTypeProvider();
                        if (!provider.TryGetContentType(filledTemplatePath, out var contentType))
                        {
                            contentType = "application/octet-stream";
                        }

                        var fileBytes = System.IO.File.ReadAllBytes(filledTemplatePath);
                        string filename = documentTemplate.TemplateName + "Describing text.docx";
                        return File(fileBytes, contentType, Path.GetFileName(filename));
                       
                    }
                    else return StatusCode(500);
                }
                else return StatusCode(404);
            }
            catch
            {
                return StatusCode(500);
            }

        }

      
        [HttpPost]
        public async Task<IActionResult> UploadTemplate([FromBody] TemplatePostDTO postDTO)
        {
            try
            {
                DocumentTemplate documentTemplate = _mapper.Map<DocumentTemplate>(postDTO.TemplateDTO);
                documentTemplate = await _apiService.UploadTemplate(documentTemplate, postDTO.FileByteArray);

                if (documentTemplate.FilePath != null)
                {
                    if (await _dbService.CreateTemplate(documentTemplate))
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
            catch(Exception ex)
            {
                return StatusCode(500);
            }


        }

    }

}
      
             


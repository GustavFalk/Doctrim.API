using Doctrim.DTOs;
using Doctrim.EF.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DoctrimAPIService : IDoctrimAPIService
    {
        private readonly IHostingEnvironment _env;

        public DoctrimAPIService(IHostingEnvironment env)
        {
            _env = env;
        }

        /// <summary>
        /// Converts a Bytearray into document and adds it to the "UploadedDocuments" folder and returns the DocumentFileDTO with the filepath.  
        /// it adds a DateTime to the string to make it unique in the database
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<DocumentFileDTO> DocumentUpload(DocumentPostDTO fileDTO)
        {
            fileDTO.DocumentFile.DocumentName = fileDTO.DocumentFile.DocumentName;
            var path = $"{_env.ContentRootPath}\\UploadedDocuments\\{fileDTO.DocumentFile.DocumentName}{DateTime.Now.ToString("yyMMddhhmmssffff")}{fileDTO.DocumentFile.FileExtension}";
            var fileStream = System.IO.File.Create(path);
            fileStream.Write(fileDTO.FileByteArray, 0, fileDTO.FileByteArray.Length);
            fileStream.Close();

            fileDTO.DocumentFile.DocumentPath = path;

            return fileDTO.DocumentFile;
        }

     
    }
}

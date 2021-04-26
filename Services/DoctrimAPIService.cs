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
        /// Uploads the document to the "UploadedDocuments" folder and returns the filepath to the document.  
        /// it adds a DateTime to the string to make it unique in the database
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<DocumentFile> DocumentUpload(DocumentFile file)
        {


            file.DocumentName = file.DocumentName + DateTime.Now.ToString("yyMMddhhmmssffff");
            var path = $"{_env.ContentRootPath}\\UploadedDocuments\\{file.DocumentName}";
            var fileStream = System.IO.File.Create(path);
            fileStream.Write(file.FileByteArray, 0, file.FileByteArray.Length);
            fileStream.Close();

            file.DocumentPath = "~/UploadedDocuments/" + file.DocumentName;

            return file;
            //string serverPath = _env.ContentRootPath;
            //string fileName = Path.GetFileNameWithoutExtension(file.FileName);
            //fileName = fileName + DateTime.Now.ToString("yyMMddhhmmssffff") + fileExtension;

            //string path = Path.Combine(serverPath + "/UploadedDocuments", fileName);
            //using(var fileStream = new FileStream(path, FileMode.Create))
            //{
            //    await file.CopyToAsync(fileStream);
            //}
            //int skippableURL = serverPath.Length;
            //string newURL = path.Substring(skippableURL);
            //return "~" + newURL;



        }

     
    }
}

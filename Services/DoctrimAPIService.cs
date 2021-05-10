using Doctrim.DTOs;
using Doctrim.EF.Models;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Wordprocessing;

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
        public async Task<DocumentFile> DocumentUpload(DocumentFile documentFile, byte[] fileByteArray)
        {
            
            var path = $"{_env.ContentRootPath}\\UploadedDocuments\\{documentFile.DocumentName}{DateTime.Now.ToString("yyMMddhhmmssffff")}{documentFile.FileExtension}";
            var fileStream = System.IO.File.Create(path);
            fileStream.Write(fileByteArray, 0, fileByteArray.Length);
            fileStream.Close();

            documentFile.DocumentPath = path;

            return documentFile;
        }

        /// <summary>
        /// Fills selected template with selected datasource.
        /// </summary>
        /// <param name="selectedTemplate"></param>
        /// <returns></returns>
        //Todo: Add data-source as indata aswell.
      public async Task<string> FillTemplate(DocumentTemplate selectedTemplate)
        {
            string sourceFile = selectedTemplate.FilePath ;
            string destinationFile = $@"C:\Users\Gustav\source\repos\Doctrim\Doctrim.API\FilledTemplates\DatasourceName_{selectedTemplate.TemplateName}_{DateTime.Now.ToString("yyMMddhhmmssffff")}.docx";



            try
            {
                //Makes a copy of template, that gets filled.
                File.Copy(sourceFile, destinationFile, true);
                using (WordprocessingDocument doc = WordprocessingDocument.Open(destinationFile, true))
                {
                    var body = doc.MainDocumentPart.Document.Body;

                    foreach (var text in body.Descendants<Text>())
                    {
                        if (text.Text.Contains("<#name>"))
                        {
                            text.Text = text.Text.Replace("<#name>", "Gustav");
                        }

                        if (text.Text.Contains("<#lastname>"))
                        {
                            text.Text = text.Text.Replace("<#lastname>", "Falk");
                        }

                        if (text.Text.Contains("<#food>"))
                        {
                            text.Text = text.Text.Replace("<#food>", "Tacos");
                        }

                        if (text.Text.Contains("<#animal>"))
                        {
                            text.Text = text.Text.Replace("<#animal>", "Dog");
                        }
                    }
                }
                return destinationFile;
            }

            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<DocumentTemplate> UploadTemplate(DocumentTemplate template, byte[] fileByteArray)
        {
            var path = $"{_env.ContentRootPath}\\DocumentTemplates\\{template.TemplateName}{DateTime.Now.ToString("yyMMddhhmmssffff")}.docx";
            var fileStream = System.IO.File.Create(path);
            fileStream.Write(fileByteArray, 0, fileByteArray.Length);
            fileStream.Close();

            template.FilePath = path;
            template.UniqueId = Guid.NewGuid();
            return template;
        }


    }
}

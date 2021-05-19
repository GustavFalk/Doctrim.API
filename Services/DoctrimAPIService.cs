using Doctrim.DTOs;
using Doctrim.EF.Models;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Wordprocessing;
using Adobe.DocumentServices.PDFTools.auth;
using Adobe.DocumentServices.PDFTools;
using Adobe.DocumentServices.PDFTools.pdfops;
using Adobe.DocumentServices.PDFTools.io;

namespace Services
{
    public class DoctrimAPIService : IDoctrimAPIService
    {
        private readonly IHostEnvironment _env;

        public DoctrimAPIService(IHostEnvironment env)
        {
            _env = env;
        }

        /// <summary>
        /// Converts a Bytearray into document and adds it to the "UploadedDocuments" folder and returns the DocumentFileDTO with the filepath.  
        /// it adds a DateTime to the string to make it unique in the database
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public DocumentFile DocumentUpload(DocumentFile documentFile, byte[] fileByteArray)
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
      public string FillTemplate(DocumentTemplate selectedTemplate)
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
               
                Credentials credentials = Credentials.ServiceAccountCredentialsBuilder()
                .FromFile("Insert path to pdf-apitools-credentials.json inside of CreatePdfFromDocx") //TODO: Fill in credential path here
                .Build();

                //Create an ExecutionContext using credentials and create a new operation instance.
                ExecutionContext executionContext = ExecutionContext.Create(credentials);
                CreatePDFOperation createPdfOperation = CreatePDFOperation.CreateNew();

                // Set operation input from a source file.
                FileRef source = FileRef.CreateFromLocalFile(destinationFile);
                createPdfOperation.SetInput(source);

                // Execute the operation.
                FileRef result = createPdfOperation.Execute(executionContext);

                string savedPdf = $"{_env.ContentRootPath}\\FilledTemplates\\{Path.GetFileNameWithoutExtension(destinationFile)}.pdf";
                // Save the result to the specified location.
                result.SaveAs(savedPdf);
                

                return savedPdf;
            }

            catch
            {
                return null;
            }
        }

      public DocumentTemplate UploadTemplate(DocumentTemplate template, byte[] fileByteArray)
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

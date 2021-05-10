using Doctrim.DTOs;
using Doctrim.EF.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IDoctrimAPIService
    {
        public Task<DocumentFile> DocumentUpload(DocumentFile documentFile, byte[] fileByteArray);

        public Task<string> FillTemplate(DocumentTemplate selectedTemplate);

        public Task<DocumentTemplate> UploadTemplate(DocumentTemplate template, byte[] fileByteArray);
    }
}

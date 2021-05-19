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
        public DocumentFile DocumentUpload(DocumentFile documentFile, byte[] fileByteArray);

        public string FillTemplate(DocumentTemplate selectedTemplate);

        public DocumentTemplate UploadTemplate(DocumentTemplate template, byte[] fileByteArray);
    }
}

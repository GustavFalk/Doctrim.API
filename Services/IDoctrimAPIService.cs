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
        public Task<DocumentFileDTO> DocumentUpload(DocumentPostDTO fileDTO);
    }
}

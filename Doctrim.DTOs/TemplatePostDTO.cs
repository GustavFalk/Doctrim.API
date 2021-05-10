using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctrim.DTOs
{
    public class TemplatePostDTO
    {
        public DocumentTemplateDTO TemplateDTO { get; set; }
        public byte[] FileByteArray { get; set; }
    }
}

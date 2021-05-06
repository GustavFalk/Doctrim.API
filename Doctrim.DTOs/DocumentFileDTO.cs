using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctrim.DTOs
{
    public class DocumentFileDTO
    {
        

        public Guid UniqueId { get; set; }

        [Required]
        public DateTime UploadDate { get; set; }

        public string DocumentPath { get; set; }
        public string DocumentName { get; set; }
        [Required]
        public Guid LegalEntity { get; set; }
        [Required]
        public Guid TypeGuid { get; set; }
        public string FileExtension { get; set; }
        public DocumentTypeDTO Type { get; set; }

        public List<MetadataTagDTO> Tags { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Doctrim.EF.Models
{
    public class DocumentFile
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Unique ID is required")]
        public Guid UniqueId { get; set; } 

        [Required (ErrorMessage = "Document path is required")]
        public string DocumentPath { get; set; }
        [Required(ErrorMessage = "Date is required")]
        public DateTime UploadDate { get; set; }
        public List<MetadataTag> Tags { get; set; }
        public string DocumentName { get; set; }
        public Guid LegalEntity { get; set; }
        [NotMapped]
        public string FileExtension { get; set; }
        public Guid TypeGuid { get; set; } 
        [Required(ErrorMessage = "Type is required")]
        public DocumentType Type { get; set; }

        
    }
}

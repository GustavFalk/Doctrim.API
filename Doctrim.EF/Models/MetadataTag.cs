using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Doctrim.EF.Models
{
    public class MetadataTag
    {
        public int Id { get; set; }
        public string Tag { get; set; }
        [Required(ErrorMessage = "Document id is required")]
        public Guid DocumentGuid { get; set; }
       
        public DocumentFile Document { get; set; }
    }
}


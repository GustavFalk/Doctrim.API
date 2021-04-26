using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Doctrim.EF.Models
{
    public class DocumentType
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "unique id is required")]
        public Guid UniqueId { get; set; }

        [Required(ErrorMessage = "Type name is required")]
        public string Type { get; set; }
           
        public List<DocumentFile> Documents { get; set; }

    }
}

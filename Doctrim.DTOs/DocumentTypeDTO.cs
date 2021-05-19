using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctrim.DTOs
{
    public class DocumentTypeDTO
    {     

        public Guid UniqueId { get; set; }
        [Required]
        public string Type { get; set; }
    }
}

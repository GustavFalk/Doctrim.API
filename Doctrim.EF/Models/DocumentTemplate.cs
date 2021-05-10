using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctrim.EF.Models
{
    public class DocumentTemplate
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Guid UniqueId { get; set; }
        [Required]
        public string TemplateName { get; set; }
        [Required]
        public string FilePath { get; set; }
    }
}

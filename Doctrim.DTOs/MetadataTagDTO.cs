using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctrim.DTOs
{
    public class MetadataTagDTO
    {
        public int Id { get; set; }
        public string Tag { get; set; }
        public Guid DocumentGuid { get; set; }

    }
}

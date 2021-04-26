using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class DocumentFileModel
    {
        public int Id { get; set; }        
        
        public DateTime UploadDate { get; set; }       
       
        public DocumentTypeModel Type { get; set; }

        public List<MetadataTagModel> Tags { get; set; }
    }
}

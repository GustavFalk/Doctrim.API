using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Doctrim.EF.Models;

namespace Doctrim.DTOs
{
    public class MappingProfile : Profile
    {
     
        public MappingProfile()
        {
            CreateMap<DocumentFileDTO, DocumentFile>();
            CreateMap<DocumentFile, DocumentFileDTO>();
            CreateMap<DocumentTypeDTO, DocumentType>();
            CreateMap<DocumentType, DocumentTypeDTO>();
            CreateMap<MetadataTagDTO, MetadataTag>();
            CreateMap<MetadataTag, MetadataTagDTO>();
        }
    }
}

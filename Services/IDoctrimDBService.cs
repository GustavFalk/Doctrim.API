using Doctrim.DTOs;
using Doctrim.EF.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface IDoctrimDBService
    {
        #region Create

        #region Documents
        public Task<bool> CreateDocument(DocumentFile file);
        #endregion

        #region DocumentTypes
        public Task CreateDocumentType(DocumentType type);
        #endregion

        #region DocumentTemplates
        public Task<bool> CreateTemplate(DocumentTemplate template);
        #endregion

        #endregion

        #region Read

        #region Documents
        public Task<List<DocumentFile>> GetAllDocuments();
        public Task<DocumentFile> GetDocument(Guid UniqueId);

        public Task<List<DocumentFile>> DocumentSearch(SearchQuery search);

        #region DocumentTypes
        public Task<List<DocumentType>> GetAllDocumentTypes();       
        

        #endregion
        #endregion

        #region DocumentTemplates
        public Task<List<DocumentTemplate>> GetTemplates();
        public Task<DocumentTemplate> GetTemplate(Guid selectedTemplate);
        #endregion
        #endregion

        #region Update
        #endregion

        #region Delete
        #endregion

    }
}

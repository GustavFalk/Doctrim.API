using Doctrim.DTOs;
using Doctrim.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public void CreateDocumentType(DocumentType type);
        #endregion

        #endregion

        #region Read

        #region Documents
        public Task<List<DocumentFile>> GetAllDocuments();
        public Task<DocumentFile> GetDocument(Guid UniqueId);
        public Task<List<DocumentFile>> GetDocumentsFromType(Guid type);

        #region DocumentTypes
        public Task<List<DocumentType>> GetAllDocumentTypes();

        public Task<List<DocumentFile>> GetDocumentsBetweenDates(DateTime first, DateTime last);

        public Task<List<DocumentFile>> GetDocumentsFromTag(string search);
        public Task<List<DocumentFile>> DocumentSearch(SearchDTO search);

        #endregion
        #endregion

        #endregion

        #region Update
        #endregion

        #region Delete
        #endregion

    }
}

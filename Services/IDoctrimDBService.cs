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
        public Task<DocumentFile> GetDocument(int id);
        public Task<List<DocumentFile>> GetDocumentsFromType(Guid type);

        #region DocumentTypes
        public Task<List<DocumentType>> GetAllDocumentTypes();
        #endregion
        #endregion

        #endregion

        #region Update
        #endregion

        #region Delete
        #endregion

    }
}

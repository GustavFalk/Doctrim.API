using Doctrim.EF.Data;
using Doctrim.EF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services
{
    public class DoctrimDBService : IDoctrimDBService
    {
        private readonly DoctrimContext _context;
        private readonly IDoctrimAPIService _apiService;
        public DoctrimDBService(DoctrimContext context, IDoctrimAPIService apiService)
        {
            _context = context;
            _apiService = apiService;

        }
        #region Create

        #region Documents

        /// <summary>
        /// Uploads a document file to the server and saves document metadata in database.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<bool> CreateDocument(DocumentFile file)
        {
            
            if (file.DocumentPath != null)
            {
                await _context.Documents.AddAsync(file);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }

        }

        #endregion

        #region DocumentTypes

        /// <summary>
        /// Adds a new DocumentType in the dababase from a DocumentType object.
        /// </summary>
        /// <param name="type"></param>
        public async void CreateDocumentType(DocumentType type)
        {
            
                type.UniqueId = Guid.NewGuid();
                await _context.DocumentTypes.AddAsync(type);
                await _context.SaveChangesAsync();
           
        }
            
        #endregion

        #endregion

        #region Read

        #region Documents
        /// <summary>
        /// Returns a list of all documents in the database.
        /// </summary>
        /// <returns></returns>
        public async Task<List<DocumentFile>> GetAllDocuments()
        {
            return await _context.Documents
                .Include(x => x.Tags)
                .Include(x => x.Type)
                .ToListAsync();
        }

        /// <summary>
        /// Returns a object of DocumentType using an id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DocumentFile> GetDocument(int id)
        {
            return await _context.Documents
                .Include(x => x.Tags)
                .Include(x => x.Type)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

        }

        /// <summary>
        /// Returns a list of document that has one specific types identifyer.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<List<DocumentFile>> GetDocumentsFromType(Guid type)
        {
            return await _context.Documents
                .Where(x => x.TypeGuid == type)
                .ToListAsync();
        }

      
        #endregion

        #region DocumentTypes

        /// <summary>
        /// Returns a list of all document types
        /// </summary>
        /// <returns></returns>
        public async Task<List<DocumentType>> GetAllDocumentTypes()
        {
           return await _context.DocumentTypes
                .ToListAsync();
        }

        #endregion

        #endregion

        #region Update
        #endregion

        #region Delete
        #endregion
    }
}

using Doctrim.DTOs;
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
        public async Task CreateDocumentType(DocumentType type)
        {
            
                type.UniqueId = Guid.NewGuid();
                await _context.DocumentTypes.AddAsync(type);
                await _context.SaveChangesAsync();
           
        }

        #endregion

        #region DocumentTemplates

        public async Task<bool> CreateTemplate(DocumentTemplate template)
        {

            if (template.FilePath != null)
            {
                await _context.Templates.AddAsync(template);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }

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
        public async Task<DocumentFile> GetDocument(Guid UniqueId)
        {
            return await _context.Documents
                .Include(x => x.Tags)
                .Include(x => x.Type)
                .Where(x => x.UniqueId == UniqueId)
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

        /// <summary>
        /// Returns a list of document that har an uploaddate between two dates.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>

        public async Task<List<DocumentFile>> GetDocumentsBetweenDates(DateTime first, DateTime last)
        {
            return await _context.Documents
                .Where(x => x.UploadDate >= first && x.UploadDate <= last)
                .ToListAsync();
        }

        public async Task<List<DocumentFile>> GetDocumentsFromTag(string search)
        {
            return await _context.Documents
                .Include(x => x.Tags)
                .Where(x => x.Tags.Any(y => y.Tag.Contains(search)))                
                .ToListAsync();         


        }

        public async Task<List<DocumentFile>> DocumentSearch(SearchDTO search)
        {
            var files = from x in _context.Documents
                        select x;                

            //sorts the list of files with search 
            if (search.TypeGuid != Guid.Empty)
                files = files.Where(x => x.TypeGuid == search.TypeGuid);

            if (search.From != DateTime.MinValue && search.Until != DateTime.MinValue)
                files = files.Where(x => x.UploadDate >= search.From && x.UploadDate <= search.Until);

            if (search.TagName != null)
                files = files.Where(x => x.Tags.Any(y => y.Tag.Contains(search.TagName)));

            if (search.LegalEntityGuid != Guid.Empty)
                files = files.Where(x => x.LegalEntity == search.LegalEntityGuid);

            return await files
                .Include(x => x.Tags)
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

        #region DocumentTemplates
        public async Task<DocumentTemplate> GetTemplate(Guid selectedTemplate)
        {
            return await _context.Templates                
                .Where(x => x.UniqueId == selectedTemplate)
                .FirstOrDefaultAsync();

        } 
        public async Task<List<DocumentTemplate>> GetTemplates()
        {
            return await _context.Templates
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

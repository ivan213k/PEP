using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PerformanceEvaluationPlatform.DAL.DatabaseContext;
using PerformanceEvaluationPlatform.DAL.Models.Documents.Dao;
using PerformanceEvaluationPlatform.DAL.Models.Documents.Dto;
using PerformanceEvaluationPlatform.DAL.Models.Documents.Mappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.DAL.Repositories.Document
{
    public class DocumentRepository : BaseRepository, IDocumentReposotory
    {
        public DocumentRepository(IOptions<DatabaseOptions> databaseOptions, PepDbContext dbContext)
            : base(databaseOptions, dbContext)
        {
        }
        public async Task Update(int id)
        {
            var  docum = await Get(id);
            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteDocument(int id)
        {
                var removeDocument = await Get(id);
            if (removeDocument != null)
            {
                var result = DbContext.Set<Models.Documents.Dao.Document>().Remove(removeDocument);
                await DbContext.SaveChangesAsync();
            }  
        }

        public async Task<DocumentDetailDto> GetDocument(int id)
        {
            var documentDetail = await DbContext.Set<Models.Documents.Dao.Document>()
                .Include(x => x.DocumentType)
                .Include(x => x.User)
                .Include(x => x.CreatedBy)
                .Include(x => x.UpdatedBy)
                .SingleOrDefaultAsync(x => x.Id == id);
            if (documentDetail == null) {
                return null;
            }
            var documentDetailDto = documentDetail.AsDocumentDetailDto();
            return documentDetailDto;
        }

        public Task<IList<DocumentListItemDto>> GetDocuments(DocumentListFilterDto filter)
        {
            var parameters = new {
               Search= filter.Search,
               Skip = filter.Skip,
               TypeSortOrder = filter.TypeSortOrder,
               NameSortOrder = filter.NameSortOrder,
               Take = filter.Take,
               TypeListIds = filter.TypeIds,
               UserListIds = filter.UserIds,
               ValidUpToDate = filter.ValidTo
            };
            return ExecuteSp<DocumentListItemDto>("[dbo].[sp_GetDocumentListItem]", parameters);
        }

        public async Task<DocumentTypeDto> GetTypeModel(int id)
        {
            var documentType = await DbContext.Set<DocumentType>().SingleOrDefaultAsync(x => x.Id == id);
            if (documentType == null) {
                return null;
            }
            var documentTypeDto = documentType.AsDocumentTypeDto();
            return documentTypeDto;
        }

        public async Task<IList<DocumentTypeDto>> GetTypes()
        {
            var documentTypesList = await DbContext.Set<DocumentType>().ToListAsync();
            var docTypesDtos = Mapper.ConvertToIEnumerableDocumetTypeDtoFromIEnumerableDocumentType(documentTypesList);
            return (IList<DocumentTypeDto>)docTypesDtos;
            
        }

        public  Task<Models.Documents.Dao.Document> Get(int id) {
            return Get<Models.Documents.Dao.Document>(id);
        }

        public Task Create(Models.Documents.Dao.Document example)
        {
            return Create<Models.Documents.Dao.Document>(example);
        }
    }
}

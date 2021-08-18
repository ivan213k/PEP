using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PerformanceEvaluationPlatform.Application.Interfaces.Documents;
using PerformanceEvaluationPlatform.Application.Model.Documents;
using PerformanceEvaluationPlatform.Domain.Documents;
using PerformanceEvaluationPlatform.Persistence.DatabaseContext;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Persistence.Repositories.Documents
{
    public class DocumentRepository : BaseRepository, IDocumentReposotory
    {
        //TODO: Uncomment User after adding in project
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
                var result = DbContext.Set<Document>().Remove(removeDocument);
                await DbContext.SaveChangesAsync();
            }  
        }

        public async Task<DocumentDetailDto> GetDocument(int id)
        {
            var documentDetail = await DbContext.Set<Document>()
                .Include(x => x.DocumentType)
                //.Include(x => x.User)
                //.Include(x => x.CreatedBy)
                //.Include(x => x.UpdatedBy)
                .SingleOrDefaultAsync(x => x.Id == id);
            if (documentDetail == null) {
                return null;
            }
            var documentDetailDto = documentDetail.AsDto();
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
            var documentTypeDto = documentType.AsDto();
            return documentTypeDto;
        }

        public async Task<IList<DocumentTypeDto>> GetTypes()
        {
            var documentTypesList = await DbContext.Set<DocumentType>().ToListAsync();
            var docTypesDtos = documentTypesList.AsIEnumerableDtos(); 
            return (IList<DocumentTypeDto>)docTypesDtos;
            
        }

        public  Task<Document> Get(int id) {
            return Get<Document>(id);
        }

        public Task Create(Document example)
        {
            return Create<Document>(example);
        }
    }
}

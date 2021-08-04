using PerformanceEvaluationPlatform.DAL.Models.Documents.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using Documentdoc = PerformanceEvaluationPlatform.DAL.Models.Documents.Dao.Document;


namespace PerformanceEvaluationPlatform.DAL.Repositories.Document
{
    public interface IDocumentReposotory : IBaseRepository
    {
        Task<IList<DocumentListItemDto>> GetDocuments(DocumentListFilterDto filter);

        Task<DocumentDetailDto> GetDocument(int id);

        Task<IList<DocumentTypeDto>> GetTypes();

        Task<DocumentTypeDto> GetTypeModel(int id);

        Task Create(Documentdoc example);

        Task DeleteDocument(int id);

        Task<Models.Documents.Dao.Document> Get(int id);



    }
}

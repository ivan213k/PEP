using PerformanceEvaluationPlatform.Application.Model.Documents;
using PerformanceEvaluationPlatform.Domain.Documents;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Application.Interfaces.Documents
{
    public interface IDocumentReposotory : IBaseRepository
    {
        Task<IList<DocumentListItemDto>> GetDocuments(DocumentListFilterDto filter);

        Task<DocumentDetailDto> GetDocument(int id);

        Task<IList<DocumentTypeDto>> GetTypes();

        Task<DocumentTypeDto> GetTypeModel(int id);

        Task Create(Document example);

        Task DeleteDocument(int id);

        Task<Document> Get(int id);



    }
}

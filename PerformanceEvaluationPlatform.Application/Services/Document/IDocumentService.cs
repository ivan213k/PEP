using PerformanceEvaluationPlatform.Application.Model.Documents;
using PerformanceEvaluationPlatform.Application.Packages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Application.Services.Document
{
    public interface IDocumentService
    {
        Task<ServiceResponse<IList<DocumentListItemDto>>> GetDocuments(DocumentListFilterDto filter);
        Task<ServiceResponse<IList<DocumentTypeDto>>> GetTypes();
        Task<ServiceResponse<DocumentDetailDto>> GetDocumentDetails(int id);
        Task<ServiceResponse<DocumentTypeDto>> GetDocumentType(int id);
        Task<ServiceResponse> Update(int id, UpdateDocumentDto model);
        Task<ServiceResponse<int>> Create(CreateDocumentDto model);
        Task<ServiceResponse> Delete(int id);
    }
}

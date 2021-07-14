using PerformanceEvaluationPlatform.Models.Document.BaseModels;
using PerformanceEvaluationPlatform.Models.Document.RequestModels;
using PerformanceEvaluationPlatform.Models.Document.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Repositories.Document
{
    public interface IDocumentReposotory
    {
        IEnumerable<DocumentModel> GetDocuments();

        public DocumentModel GetDocument(int id);

        IEnumerable<TypeModel> GetTypes();

        public TypeModel GetTypeModel(int id);

        bool SaveDocument(DocumentModel model);

        bool UpdateDocument(DocumentModel model);

        bool UpdateType(TypeModel type);

        bool DeleteType(TypeModel type);

        bool DeleteDocument(DocumentModel document);

        IEnumerable<DocumentModel> GetFiltredDocumentsLits(DocumentRequestModel filter);

    }
}

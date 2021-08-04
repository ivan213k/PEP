using PerformanceEvaluationPlatform.Models.Document.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.Document.Validator
{
    public interface IDocumentValidator
    {
        ValidationError TryValidateRequestAddDocumentModel(RequestAddDocumentModel model);
        Task<ValidationError> TryValidateRequestAddDocumentModel(RequestUpdateDocumentModel model);
    }
}

using PerformanceEvaluationPlatform.DAL.Repositories.Document;
using PerformanceEvaluationPlatform.Models.Document.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.Document.Validator
{
    public class DocumentRequestModelsValidator:IDocumentValidator
    {
       private readonly IDocumentReposotory _documentsRepository;
        public DocumentRequestModelsValidator(IDocumentReposotory documentReposotory)
        {
            _documentsRepository = documentReposotory;
        }
       public  ValidationError TryValidateRequestAddDocumentModel(RequestAddDocumentModel model) {
            ValidationError error = new ValidationError();
            error.IsValid = false;
            if (model.ValidToDate < DateTime.Now)
            {
                error.ValidationErrors.Add(nameof(model.ValidToDate), " Validation time is not correct. it must be bigger than current time.");
                return error;
            }
            if (string.IsNullOrWhiteSpace(model.FileName))
            {
                error.ValidationErrors.Add(nameof(model.FileName), " Set File Name");
                return error;
            }
            error.IsValid = true;
            return error;
        }
        public async Task<ValidationError> TryValidateRequestAddDocumentModel(RequestUpdateDocumentModel model)
        {
            ValidationError error = new ValidationError();
            error.IsValid = false;
            var modelForUpdating = await _documentsRepository.Get(model.Id);
            if (modelForUpdating == null)
            {
                error.ValidationErrors.Add(nameof(model.Id), " Not Found");
                return error;


            }
            if (model.ValidToDate < DateTime.Now)
            {
                error.ValidationErrors.Add(nameof(model.ValidToDate), " Validation time is not correct. it must be bigger than current time.");
                return error; 
            }
            if (string.IsNullOrWhiteSpace(model.FileName))
            {
                error.ValidationErrors.Add(nameof(model.FileName), " Set File Name");
                return error; 
            }
            error.IsValid = true;
            return error;
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using PerformanceEvaluationPlatform.Models.Document.BaseModels;
using PerformanceEvaluationPlatform.Models.Document.Mappers;
using PerformanceEvaluationPlatform.Models.Document.RequestModels;
using PerformanceEvaluationPlatform.Models.Document.ViewModels;
using PerformanceEvaluationPlatform.Repositories.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Controllers
{
    //TODO: Refactor after adding DTO
    [ApiController]
    public class DocumentController : ControllerBase
    {
        readonly IDocumentReposotory _documentsRepository;
        public DocumentController(IDocumentReposotory documentRepository) {
            _documentsRepository = documentRepository;
        }
        [HttpGet("documents")]
        public IActionResult GetDocuments([FromQuery] DocumentRequestModel filter)
        {
            
            return Ok(MyCustomDocumentMapper.GetListOfMappedDocumentsItemViewModels(_documentsRepository.GetFiltredDocumentsLits(filter),_documentsRepository.GetTypes()));
        }

        [HttpGet("documents/{id}")]
        public IActionResult GetDocument(int id)
        {
            var docum = _documentsRepository.GetDocument(id);
            if (docum == null) {
                return NotFound();
            }
            return Ok(MyCustomDocumentMapper.MappDetailDocument(docum,_documentsRepository.GetTypeModel(docum.TypeId)));
        }

        [HttpGet("documents/types")]
        public IActionResult GetDocumentTypes()
        {
            return Ok(MyCustomDocumentMapper.GetTypeViewModelsfromTypes(_documentsRepository.GetTypes()));
        }

        [HttpGet("documents/types/{id}")]
        public IActionResult GetDocumentType(int id)
        {
            var result = MyCustomDocumentMapper.TypeViewModelFromType(_documentsRepository.GetTypeModel(id));
            if (result == null) {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost("document")]
        public IActionResult CreateDocument([FromBody] RequestAddDocumentModel model) {
           var docum = RequestDocumentModelValidation(model);
            if (docum.GetType() == typeof(OkResult))
            {
                _documentsRepository.SaveDocument(MyCustomDocumentMapper.ConvertRequestAddDocumentModelToBaseModel(model));
                return Ok(model);
            }
            return NotFound();
        }

        [HttpPut("document/{id}")]
        public IActionResult EditDocument([FromBody] DocumentDetailViewModel model) {
            //TODO: Create UpdateModel
            //var document = _documentsRepository.GetDocument(model.Id);
            //var result=ValidationUpdateDocument(model, document);
            //if (result.GetType() == typeof(OkResult)) {
            //    document = MyCustomDocumentMapper.MappDocumentModel(model, _documentsRepository);
            //    if (document == null) {
            //        return BadRequest("Can`t find edited type name in Database");
            //    }
            //    _documentsRepository.UpdateDocument(document);
            //    return Ok(document);
            //}
            //return result;
            return NotFound();
        }

        private IActionResult RequestDocumentModelValidation(RequestAddDocumentModel model) {
            //TODO: Check by UserId if user exist
            //cheking if user with such id existing in Database
            var type = _documentsRepository.GetTypeModel(model.TypeId);
            if (type == null) {
                return BadRequest("No Such type in DataBase");
            }
            if (model.ValidToDate < DateTime.Now) {
                return BadRequest("documents Validation Time expired");
            }
            if ((string.IsNullOrEmpty(model.FileName) || string.IsNullOrWhiteSpace(model.FileName))||_documentsRepository.GetDocuments().Select(x=>x.FileName).Contains(model.FileName)) {
                return BadRequest("File name empty or olready exist");
            }
            if (string.IsNullOrEmpty(model.MetaDate) || string.IsNullOrWhiteSpace(model.MetaDate)) {
                return BadRequest("MetaData is Empty");
            }
            model.CreatedByAt = DateTime.Now;
            return Ok();
        }

        private IActionResult ValidationUpdateDocument(DocumentDetailViewModel model, DocumentModel document) {
            // not Allowed to change UserName, CreatedBy and CreatedAt and Metadata fields
            if (model.CreatedAt != document.CreatedAt) {
                return BadRequest("You can`t change date of creating");
            }
            if (string.IsNullOrEmpty(model.LastUpdatesBy) || string.IsNullOrWhiteSpace(model.LastUpdatesBy)) {
                return BadRequest("Insert Your Name");
            }
            if (string.IsNullOrWhiteSpace(model.FileName)||string.IsNullOrEmpty(model.FileName)) {
                return BadRequest("Set name for file");
            }
            if (model.ValidTo == null || model.ValidTo == default(DateTime)||model.ValidTo<DateTime.Now) {
                return BadRequest("Wrong Date of Validation");
            }
            model.LastUpdatesAt = DateTime.Now;
            
            //TODO: creat a check for prevant changing User Id
            return Ok();

        }
    }
}
